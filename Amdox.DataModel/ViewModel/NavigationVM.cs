
using System.ComponentModel.DataAnnotations;
namespace Amdox.DataModel
{
    public partial class NavigationVM : INavigationVM
    {
        [Key]
        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public int UserType
        {
            get;
            set;
        }
        public int PartnerType
        {
            get;
            set;
        }
        public string ImageUrl { get; set; }
    }
}
