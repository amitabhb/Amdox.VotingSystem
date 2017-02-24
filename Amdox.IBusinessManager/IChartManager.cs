//-----------------------------------------------------------------------
// <copyright file="IChartManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Signature for Business Manager.
/// </summary>
namespace Amdox.IBusinessManager
{
    using System.Collections.Generic;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;

    /// <summary>
    /// Signature of Chart Manager
    /// </summary>
    public interface IChartManager
    {
        /// <summary>
        /// Get chart details
        /// </summary>
        /// <param name="pollid">pass poll id</param>
        /// <returns>returns data for display of chart</returns>
        IChartCollectionVM GetCharts(int pollid);

        /// <summary>
        /// Get vote results by id
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>return IResolutionStatisticsVM</returns>
        List<IResolutionStatisticsVM> GetVoteResolutionStatistics(int pollID);
    }
}
