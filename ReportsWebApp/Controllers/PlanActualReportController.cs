using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportsWebApp.Models;

namespace ReportsWebApp.Controllers
{
    [PlanReportAuthorizeFilter]
    public class PlanActualReportController : Controller
    {
        private ReportsWebAppDBEntities db = new ReportsWebAppDBEntities();

        // GET: 
        public ActionResult Index(string departureCity, string arrivalCity)
        {
            PlanReportModel model = new PlanReportModel();
            List<PlanActualReport> report = new List<PlanActualReport>();

            try
            {
                model.DepartureCities = db.Cities.Select(ct => ct.CityName).ToList();
                model.ArrivalCities = db.Cities.Select(ct => ct.CityName).ToList();
                var query = from plan in db.Plans
                            join ctFrom in db.Cities on plan.CityFromId equals ctFrom.Id
                            join ctTo in db.Cities on plan.CityToId equals ctTo.Id
                            join transp in db.Transportations on plan.Id equals transp.PlanId into transportations
                            select new PlanActualReport
                            {
                                PlanId = plan.Id,
                                DepartureCity = ctFrom.CityName,
                                ArrivalCity = ctTo.CityName,
                                PlannedTransportations = plan.PlannedTransportations,
                                ActualTransportations = transportations.Count()
                            };

                if (!string.IsNullOrEmpty(departureCity))
                {
                    query = from rep in query
                            where rep.DepartureCity == departureCity
                            select rep;
                }

                if (!string.IsNullOrEmpty(arrivalCity))
                {
                    query = from rep in query
                            where rep.ArrivalCity == arrivalCity
                            select rep;
                }

                report = query.ToList();

                foreach (var row in report)
                {
                    int[] days = new int[31];

                    for (int i = 0; i < 31; i++)
                    {
                        days[i] = 0;
                    }

                    var transportations = from trnsp in db.Transportations
                                          where trnsp.PlanId == row.PlanId
                                          select trnsp.DateTimeDeparture;
                    foreach (var trnsp in transportations)
                    {
                        days[trnsp.Day - 1]++;
                    }
                    row.ActualPerDay = days;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
            ViewBag.Message = "Plan-actual report page.";
            model.report = report;

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
