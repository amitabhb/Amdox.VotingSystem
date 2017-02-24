

namespace Amdox.IDataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOptionStats
    {
        int simpleCount {get;set;}
        decimal simplePercentage { get; set; }

        int weightedCount { get; set; }
        decimal weightedPercentage { get; set; }
    }
}
