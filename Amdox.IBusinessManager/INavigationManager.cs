//-----------------------------------------------------------------------
// <copyright file="INavigationManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Signature for NavigationManager
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
    /// Navigation manager
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// Get User Role.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>returns navigation view model</returns>
        INavigationVM GetUserRole(string userName);
    }
}
