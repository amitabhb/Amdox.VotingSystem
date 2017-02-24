//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Amdox.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public interface IUserDetail
    {
        int UserDetailsID { get; set; }
        Nullable<int> UserID { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        Nullable<int> UserTypeID { get; set; }
        Nullable<System.DateTime> CreatedDate { get; set; }
        Nullable<bool> IsDeleted { get; set; }
        Nullable<int> CreatedBy { get; set; }
        Nullable<int> PartnerTypeID { get; set; }
        Nullable<bool> IsInternationalPartner { get; set; }
        string OfficeLocation { get; set; }
        Nullable<System.DateTime> ResignationDate { get; set; }
        Nullable<bool> isResigned { get; set; }
        Nullable<System.DateTime> DateLeft { get; set; }
        string EmailAddress { get; set; }
        string Title { get; set; }
        Nullable<bool> IsEmailNotificationRequired { get; set; }
    }
}
