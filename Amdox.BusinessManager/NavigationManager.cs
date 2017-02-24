//-----------------------------------------------------------------------
// <copyright file="NavigationManager.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Navigation Manager
/// </summary>
namespace Amdox.BusinessManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Amdox.DataModel;
    using Amdox.IBusinessManager;
    using Ninject;

    /// <summary>
    /// Navigation Manager class
    /// </summary>
    public class NavigationManager : INavigationManager
    {
        /// <summary>
        /// Navigation Details view model.
        /// </summary>
        [Inject]
        private INavigationVM userNavDetails;

        /// <summary>
        /// data context.
        /// </summary>
        [Inject]
        private VoteContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationManager" /> class.
        /// </summary>
        /// <param name="navVM">Navigation view model</param>
        /// <param name="dataContext">data context</param>
        public NavigationManager(INavigationVM navVM, VoteContext dataContext)
        {
            this.userNavDetails = navVM;
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Get User Role.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>returns navigation view model</returns>
        public INavigationVM GetUserRole(string userName)
        {
            ////TODO : Write the logic to get the NavigationVM.

            ////userDetails are picked up from userdetails and users table. 
            ////checks for isenabled,isdeleted and username-windows logged inuser ex ad\salim mandrekar
            string strUserName = userName;
            var objUserDetails = (from UD in this.dataContext.UserDetails
                                join U in this.dataContext.Users on UD.UserID equals U.UserID 
                                where UD.IsDeleted == false 
                                && U.IsEnabled == true
                                && U.UserName.Equals(strUserName)
                                select new
                                {
                                    UD.FirstName,
                                    UD.MiddleName,
                                    UD.LastName,
                                    UD.UserTypeID,
                                    U.UserName,
                                    UD.UserID,
                                    UD.UserDetailsID,
                                    UD.PartnerTypeID,                                   
                                }).ToList();

            if (objUserDetails.Count > 0)
            {
                foreach (var s in objUserDetails)
                {
                    this.userNavDetails.UserId = s.UserID.GetValueOrDefault();
                    this.userNavDetails.UserName = s.UserName;
                    this.userNavDetails.FirstName = s.FirstName;
                    this.userNavDetails.LastName = s.LastName;
                    this.userNavDetails.FullName = s.FirstName + ' ' + s.MiddleName + ' ' + s.LastName;
                    this.userNavDetails.PartnerType = s.PartnerTypeID.GetValueOrDefault();
                    this.userNavDetails.UserType = s.UserTypeID.GetValueOrDefault();
                    this.userNavDetails.ImageUrl = string.Empty;
                }
            }
            
             return this.userNavDetails;
        }
    }
}
