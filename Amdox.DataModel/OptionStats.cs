using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amdox.IDataModel;

namespace Amdox.DataModel
{
    public class OptionStats : IOptionStats
    {
        public int simpleCount { get; set; }
        public decimal simplePercentage { get; set; }

        public int weightedCount { get; set; }
        public decimal weightedPercentage { get; set; }
    }
}
