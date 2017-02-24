using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.IDataModel.IViewModel
{
    public interface IChartCollectionVM
    {
        List<Highcharts> ChartCollection { get; set; }
    }
}
