using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ReportsWebApp.Models
{
    public class SubReport
    {
        [Display(Name = "Arrival city")]
        public string ArrivalCity { get; set; }

        [Display(Name = "Planned transportations")]
        public int PlannedTransportations { get; set; }

        [Display(Name = "Actual transportations")]
        public int ActualTransportations { get; set; }
    }

    public class GroupReport
    {
        [Display(Name = "Departure city")]
        public string DepartureCity { get; set; }

        [Display(Name = "Planned transportations")]
        public int PlannedTransportations { get; set; }

        [Display(Name = "Actual transportations")]
        public int ActualTransportations { get; set; }

        public List<SubReport> Row { get; set; }
    }
}