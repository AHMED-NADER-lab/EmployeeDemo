using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData.ViewModel
{
    public class LoginResponseVM
    {
        public string token { get; set; }
        public bool islogin { get; set; }
        public DateTime? loginDatetime { get; set; }
    }
}
