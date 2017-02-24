//-----------------------------------------------------------------------
// <copyright file="ChartManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Amdox.BusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Amdox.DataModel;
    using Amdox.DataModel.ViewModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;
    using DotNet.Highcharts;
    using DotNet.Highcharts.Enums;
    using DotNet.Highcharts.Helpers;
    using DotNet.Highcharts.Options;
    using Ninject;
    using Ninject.Parameters;

    /// <summary>
    /// Chart Manager
    /// </summary>
    public class ChartManager : IChartManager
    {
        /// <summary>
        /// VM for Chart
        /// </summary>
        [Inject]
        private IVoteResultChartVM voteResultChartVM;

        /// <summary>
        /// data context
        /// </summary>
        [Inject]
        private VoteContext context;

        /// <summary>
        /// Poll manager business
        /// </summary>
        [Inject]
        private IPollManager ipollManager;

        /// <summary>
        /// DI kernel
        /// </summary>
        private IKernel krnel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartManager" /> class.
        /// </summary>
        /// <param name="voteResultChartVM">voteResultChart view model</param>
        /// <param name="dataContext">data context</param>
        /// <param name="ipollManager">poll manager</param>
        /// <param name="kernel">DI kernel</param>
        public ChartManager(IVoteResultChartVM voteResultChartVM, VoteContext dataContext, IPollManager ipollManager, IKernel kernel)
        {
            this.voteResultChartVM = voteResultChartVM;
            this.context = dataContext;
            this.ipollManager = ipollManager;
            this.krnel = kernel;
        }

        /// <summary>
        /// Get vote results by id
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>return IResolutionStatisticsVM</returns>
        public List<IResolutionStatisticsVM> GetVoteResolutionStatistics(int pollID)
        {
            var objPollQuestions = from pq in this.context.Poll_Questions
                                   join ps in this.context.Poll_Statistics on pq.QuestionID equals ps.QuestionID
                                   where pq.PollID == pollID && pq.IsEnabled == true
                                   select new
                                   {
                                       ps.PollStatisticsID,
                                       ps.PollID,
                                       pq.QuestionDescription,
                                       ps.OptionID,
                                       ps.QuestionID,
                                       ps.TotalVotes,
                                       ps.TotalWeighedVotesPercentage,
                                       ps.TotalVotesPercentage
                                   };

            List<IResolutionStatisticsVM> lstQuestions = new List<IResolutionStatisticsVM>();
            if (objPollQuestions != null && objPollQuestions.Count() > 0)
            {
                foreach (var a in objPollQuestions)
                {
                    var data = this.krnel.Get<IResolutionStatisticsVM>();

                    data.PollStatisticsID = a.PollStatisticsID;
                    data.PollId = pollID;
                    data.QuestionDescription = a.QuestionDescription;
                    data.OptionID = a.OptionID;
                    data.QuestionID = a.QuestionID;
                    data.TotalVotes = a.TotalVotes;
                    data.TotalWeighedVotesPercentage = a.TotalWeighedVotesPercentage;
                    data.TotalVotesPercentage = a.TotalVotesPercentage;
                    lstQuestions.Add(data);
                }
            }

            return lstQuestions;
        }

        /// <summary>
        /// Get chart details
        /// </summary>
        /// <param name="pollID">pass poll id</param>
        /// <returns>returns data for display of chart</returns>
        public IChartCollectionVM GetCharts(int pollID)
        {
            List<Highcharts> lstChart = new List<Highcharts>();
            IChartCollectionVM chartColl = this.krnel.Get<IChartCollectionVM>(new ConstructorArgument("_highChartLst", lstChart));
            var questionLst = this.context.Poll_Questions.Where(x => x.PollID == pollID);

            foreach (var resol in questionLst)
            {
                var chartData = this.GetResolution(resol.QuestionID, pollID);
                Highcharts chart = new Highcharts("chart" + resol.QuestionID);
                chart = new Highcharts("chart" + resol.QuestionID)
                               .InitChart(new Chart { PlotShadow = false, PlotBackgroundColor = null, PlotBorderWidth = null })
                               .SetTitle(new Title { Text = chartData.ResolutionDescription })
                               .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                               .SetPlotOptions(new PlotOptions
                               {
                                   Pie = new PlotOptionsPie
                                   {
                                       AllowPointSelect = true,
                                       Cursor = Cursors.Pointer,
                                       DataLabels = new PlotOptionsPieDataLabels { Enabled = false },
                                       ShowInLegend = true
                                   }
                               })

                               .SetSeries(new Series
                               {
                                   Type = ChartTypes.Pie,
                                   Name = chartData.ResolutionDescription,
                                   Data = new Data(new object[]
                    {
                        new object[] { "In Favour", chartData.Favour },
                         new object[] { "Against", chartData.Against },
                          new object[] { "Abstain", chartData.Abstain },

                        new Point
                        {
                            Name = "Did Not Vote",
                            Y = chartData.NotVoted,
                            Sliced = true,
                            Selected = true
                        },
                    })
                               });

                chartColl.ChartCollection.Add(chart);
            }

            return chartColl;
        }

        /// <summary>
        /// Get all the resolution for the poll
        /// </summary>
        /// <param name="questionId">question id</param>
        /// <param name="pollId">poll id</param>
        /// <returns>returns poll result view model</returns>
        private IVoteResultChartVM GetResolution(int questionId, int pollId)
        {
            var pollStat = this.context.Poll_Statistics.Where(x => x.QuestionID == questionId);
            IVoteResultChartVM chartVM = this.krnel.Get<IVoteResultChartVM>();

            ////1.  Get the description
            chartVM.ResolutionDescription = this.context.Poll_Questions.FirstOrDefault(x => x.QuestionID == questionId).QuestionDescription;

            ////2.  Get the Favour
            var dataFavour = (from a in this.context.Poll_Statistics
                            join b in this.context.Poll_Options on a.OptionID equals b.OptionID
                            where b.OptionName == "In Favour" && a.QuestionID == questionId
                            select a.TotalVotes).ToList();
            chartVM.Favour = dataFavour != null && dataFavour.Count() > 0 ? Convert.ToInt32(dataFavour[0]) : 0;

            ////3.  Get the Against
            chartVM.Against = (from a in this.context.Poll_Statistics
                               join b in this.context.Poll_Options on a.OptionID equals b.OptionID
                               where b.OptionName == "Against" && a.QuestionID == questionId
                               select a.TotalVotes).ToList().FirstOrDefault() ?? 0;

            ////4.  Get the Abstain
            chartVM.Abstain = (from a in this.context.Poll_Statistics
                               join b in this.context.Poll_Options on a.OptionID equals b.OptionID
                               where b.OptionName == "Abstain" && a.QuestionID == questionId
                               select a.TotalVotes).ToList().FirstOrDefault() ?? 0;

            ////5.  Get not voted yet
            chartVM.NotVoted = Convert.ToInt32(this.context.Poll_Maintenance.FirstOrDefault(x => x.PollId == pollId).TotalVotesExpected) -
                (chartVM.Favour + chartVM.Against + chartVM.Abstain);

            return chartVM;
        }
    }
}
