using EmployeeData.ViewModel;
using EmployeeServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Implementation
{
    public interface IEmployeeService
    {
        public Task<EmployeeVM> Resigter(EmployeeVM model);

        public LoginResponseVM Login(loginVM model);

        public List<EmployeeVM> GetAllEmployee();

        public EmployeeVM GetEmployee(int Id);

        public Task<UpdateEmployeeVM> UpdateEmployee(UpdateEmployeeVM model);

        public Task<bool> DeleteEmployeeAsync(int Id);

        public List<DepartmentVM> GetAllDepartment();
    }
}
