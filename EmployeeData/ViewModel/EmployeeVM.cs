using EmployeeData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData.ViewModel
{
    public class EmployeeVM
    {
        public int? Id { get; set; }
        public string fullName { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }

        public double salary { get; set; }
        public Gender gender { get; set; }

        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public bool isActive { get; set; }

    }
}
