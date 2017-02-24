using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.DataModel.ViewModel
{
    public class LoginVM : ILoginVM
    {
        [Required(ErrorMessage ="Please enter a user name.")]
        [MaxLength(100, ErrorMessage = "Username max length 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MaxLength(length:100, ErrorMessage ="Password max length 100 characters.")]
        public string Password { get; set; }
    }
}
