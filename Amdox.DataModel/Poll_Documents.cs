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
    using System.ComponentModel.DataAnnotations;

    public partial class Poll_Documents : IPoll_Documents
    {
        [Key]
        public int DocumentID { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentURL { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> PollID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public bool IsEnabled { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }


    }
}