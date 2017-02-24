//-----------------------------------------------------------------------
// <copyright file="IExportDataManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Signature for Export Data manager
/// </summary>
namespace Amdox.IBusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Amdox.IDataModel;
    using Amdox.IDataModel.IViewModel;

    /// <summary>
    /// Signature Export data business
    /// </summary>
    public interface IExportDataManager
    {
        /// <summary>
        /// Gets personnel list.
        /// </summary>
        /// <returns>return data table of partners</returns>
        DataTable GetManagePersonnelList();

        /// <summary>
        /// Get confidential vote details
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>Data table of vote details</returns>
        DataTable GetConfidentialVoteDetailsList(int pollID);

        /// <summary>
        /// Get partner grade.
        /// </summary>
        /// <param name="partner">partner id</param>
        /// <returns>return partner grade</returns>
        string GetGrade(int? partner);

        /// <summary>
        /// Get option answer (as Yes/No)
        /// </summary>
        /// <param name="isInt">question id</param>
        /// <returns>return result</returns>
        string GetYesNoValue(bool? isInt);

        /// <summary>
        /// Check admin right
        /// </summary>
        /// <param name="utype">user id</param>
        /// <returns>get user type</returns>
        string GetAdminRights(int? utype);

        /// <summary>
        /// Export to excel
        /// </summary>
        /// <param name="exportData">data to export</param>
        void ToExcel(DataTable exportData);

        /// <summary>
        /// Export vote outcome
        /// </summary>
        /// <param name="pollID">poll id</param>
        /// <returns>export data in table</returns>
        DataTable ExportVoteOutcome(int pollID);

        /// <summary>
        /// Get vote resolution
        /// </summary>
        /// <param name="pollid">poll id</param>
        /// <returns>list containing question stats</returns>
        List<IQuestionStatisticsVM> GetVoteResolutions(int pollid);
    }
}
