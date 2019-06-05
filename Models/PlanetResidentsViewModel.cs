using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PlanetResidentsViewModel
    {
        public PlanetResidentsViewModel()
        {
            Residents = new List<ResidentSummary>();
        }

        public List<ResidentSummary> Residents { get; set; }
    }
}