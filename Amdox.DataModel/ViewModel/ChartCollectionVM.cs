using Amdox.IDataModel.IViewModel;
using DotNet.Highcharts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class ChartCollectionVM : IChartCollectionVM
    {
        public List<Highcharts> ChartCollection { get; set; }

        public ChartCollectionVM(List<Highcharts> _highChartLst)
        {
            ChartCollection = _highChartLst;
        }
    }
}
