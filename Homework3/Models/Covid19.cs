using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework3.Models
{
    public class Covid19
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public long Cases { get; set; }

        public long Deaths { get; set; }

        public long Tested { get; set; }

        public override string ToString()
        {
            return $"{Date}\t{Cases}\t{Deaths}\t{Tested}";
        }
    }
}
