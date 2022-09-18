using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData.Interface
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        bool IsDeleted { get; set; }


    }
}
