using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData.Entities
{
    public class Department: BaseEntity<int>
    {
        public Department()
        {
        }

        public string Name { get; set; }
    }
}
