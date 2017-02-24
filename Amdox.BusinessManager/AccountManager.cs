//-----------------------------------------------------------------------
// <copyright file="AccountManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// This is Business Layer
/// </summary>
namespace Amdox.BusinessManager
{
    using System.Linq;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;

    /// <summary>
    /// Account manager.
    /// </summary>
    public class AccountManager : IAccountManager
    {
        /// <summary>
        /// DB Context.
        /// </summary>
        private VoteContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager" /> class.
        /// </summary>
        /// <param name="dataContext">data context</param>
        public AccountManager(VoteContext dataContext)
        {
            this.context = dataContext;
        }

        /// <summary>
        /// User authenticates
        /// </summary>
        /// <param name="loginVM">login view model</param>
        /// <returns>Organization Id</returns>
        public int UserAuthenticate(ILoginVM loginVM)
        {
            int success = 0;
            var user = this.context.Users.FirstOrDefault(x => x.UserName == loginVM.UserName && x.Password == loginVM.Password);
            if (user != null)
            {
                success = user.OrganisationId ?? 0;
            }

            return success;
        }
    }
}
