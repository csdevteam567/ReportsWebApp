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
    public class GroupReportController : Controller
    {
        private ReportsWebAppDBEntities db = new ReportsWebAppDBEntities();

        // GET: 
        [GroupReportAuthorizeFilter]
        public ActionResult Index()
        {
            List<GroupReport> report = new List<GroupReport>();

            try
            {
                List<int> fromCities = (from plan in db.Plans
                                           join fromCt in db.Cities on plan.CityFromId equals fromCt.Id
                                           select fromCt.Id).Distinct().ToList();

                foreach(var city in fromCities)
                {
                    GroupReport reportRow = new GroupReport();
                    string cityName = db.Cities.Where(c => c.Id == city ).First().CityName;
                    reportRow.DepartureCity = cityName;

                    var plans = from plan in db.Plans
                              where plan.CityFromId == city
                              select plan;
                    int totalActualTransportations = 0;
                    int totalPlannedTransportations = 0;
                    List<SubReport> subReport = new List<SubReport>();
                    foreach(var pln in plans)
                    {
                        SubReport subRow = new SubReport();
                        string arrivalCity = db.Cities.Where(c => c.Id == pln.CityToId).First().CityName;
                        subRow.ArrivalCity = arrivalCity;

                        int actualTransportations = (from tr in db.Transportations
                                                  where tr.PlanId == pln.Id
                                                  select tr).Count();

                        subRow.ActualTransportations = actualTransportations;
                        totalActualTransportations += actualTransportations;

                        subRow.PlannedTransportations = pln.PlannedTransportations;
                        totalPlannedTransportations += pln.PlannedTransportations;

                        subReport.Add(subRow);
                    }
                    reportRow.PlannedTransportations = totalPlannedTransportations;
                    reportRow.ActualTransportations = totalActualTransportations;
                    reportRow.Row = subReport;
                    report.Add(reportRow);
                }
                         
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
            ViewBag.Message = "Group report page.";
            return View(report);
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
