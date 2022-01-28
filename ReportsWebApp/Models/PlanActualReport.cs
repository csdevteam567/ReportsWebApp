using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsWebApp.Models
{
    public class PlanReportModel
    {
        public List<string> DepartureCities { get; set; }

        public List<string> ArrivalCities { get; set; }

        public List<PlanActualReport> report { get; set; }
    }

    public class PlanActualReport
    {
        [Display(AutoGenerateField = false)]
        public int PlanId { get; set; }

        [Display(Name = "Departure city")]
        public string DepartureCity { get; set; }

        [Display(Name = "Arrival city")]
        public string ArrivalCity { get; set; }

        [Display(Name = "Planned transportations")]
        public int PlannedTransportations { get; set; }

        public int[] ActualPerDay { get; set; }

        [Display(Name = "Actual transportations")]
        public int ActualTransportations { get; set; }
    }
}