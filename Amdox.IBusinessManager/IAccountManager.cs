//-----------------------------------------------------------------------
// <copyright file="IAccountManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Business manager
/// </summary>
namespace Amdox.IBusinessManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Account Manager
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="loginVM">login in user</param>
        /// <returns>send user id</returns>
        int UserAuthenticate(ILoginVM loginVM);
    }
}
