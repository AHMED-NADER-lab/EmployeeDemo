using EmployeeData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData.Entities
{
    public class Employee : BaseEntity<int>
    {
        public Employee()
        {
        }
        public string fullName { get; set; }
        public string userName  { get; set; }
        public byte[] Password { get; set; }
        public string hashPassword { get; set; }

        public double salary { get; set; }
        public Gender gender { get; set; }

        public int departmentId { get; set; }

        public Department department { get; set; }


    }
}
