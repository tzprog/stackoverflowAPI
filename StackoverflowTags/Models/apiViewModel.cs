using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackoverflowTags.Models
{
    public class apiViewModel
    {
        public int id { get; set; }
        public int lp { get; set; }
        public double PercentPopularity { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }
}
