using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ILoginVM
{
    string UserName { get; set; }
    string Password { get; set; }
}

