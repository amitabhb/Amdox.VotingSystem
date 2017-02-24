//-----------------------------------------------------------------------
// <copyright file="IHomeIndexManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Signature for HomeIndexManager
/// </summary>
namespace Amdox.IBusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Amdox.IBusinessManager;

    /// <summary>
    /// Signature of Index manager
    /// </summary>
    public interface IHomeIndexManager
    {
        /// <summary>
        /// GEt user info
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>returns home index data</returns>
        IHomeIndexVM GetUserInfo(string userName);
    }
}
