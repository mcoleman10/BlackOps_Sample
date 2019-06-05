using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class AllVehicles
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Vehicle> results { get; set; }

        public bool isNext
        {
            get
            {
                return !String.IsNullOrEmpty(next);
            }
        }

        public bool isPrev
        {
            get
            {
                return !String.IsNullOrEmpty(previous);
            }
        }
    }
}