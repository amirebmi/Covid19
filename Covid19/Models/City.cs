using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework3.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }

        public long Population { get; set; }

        public virtual List<Covid19> Covids { get; set; }

        public override string ToString()
        {
            return $"City of {CityName} (Population: {Population})";
        }
    }
}
