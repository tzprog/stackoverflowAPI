using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackoverflowTags.Models
{
    public class wwwViewModel
    {
        public int Id { get; set; }

        public string TagName { get; set; }
        public string TagQuestionsCounter { get; set; }

        public double PercentPopularity { get; set; }
    }
}
