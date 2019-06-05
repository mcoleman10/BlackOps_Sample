using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PlanetDetailsViewModel : IComparable<PlanetDetailsViewModel>
    {
        public string Name { get; set; }

        public string Population { get; set; }

        public string Diameter { get; set; }

        public string Terrain { get; set; }

        public string Orbital_Period { get; set; }

        public List<string> residents { get; set; }

        public string FormattedPopulation => Population == "unknown" ? "unknown" : long.Parse(Population).ToString("N0");

        public int CompareTo(PlanetDetailsViewModel that)
        {
            int currentDiameter = 0;
            int compareDiameter = 0;

            if (that == null) return 1;

            int.TryParse(this.Diameter, out currentDiameter);
            int.TryParse(that.Diameter, out compareDiameter);
            if (that.Diameter == "unknown") compareDiameter = -1;
            if (this.Diameter == "unknown") currentDiameter = -1;
            if (that.Diameter == "0") compareDiameter = 0;
            if (this.Diameter == "0") currentDiameter = 0;

            if (currentDiameter < compareDiameter) return 1;
            if (currentDiameter > compareDiameter) return -1;

            return 0;
        }
    }
}