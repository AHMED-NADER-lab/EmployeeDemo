using EmployeeData.Entities;
using EmployeeData.ViewModel;
using EmployeeRepository;
using EmployeeServices.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeServices.Interface
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee, int> _employee;
        private readonly IRepository<Department, int> _depatment;
        private readonly IConfiguration config;
        public EmployeeService(IRepository<Employee,int> employee, IConfiguration configuration, IRepository<Department, int> depatment)
        {
            _employee = employee;
            config = configuration;
            _depatment = depatment;
        }

   
        public LoginResponseVM Login(loginVM model)
        {
            if (model==null)
            {
                throw new Exception("login fail ");
            }
            var user = _employee.GetAll().Where(x => x.userName == model.UserName && !x.IsDeleted).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("user not found ");
            }
            else
            {
                var password = comparePassword( user.Password,model.password,user.hashPassword);
                    if(!password)
                {
                    throw new Exception("login fail ");
                }

            }
           
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.userName),
          new Claim("fullName", user.fullName),
        new Claim(ClaimTypes.Role, "employee")
    };
                var tokeOptions = new JwtSecurityToken(
                      issuer: "http://localhost:5276",
                     audience: "http://localhost:5276",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                 return new LoginResponseVM()
            {
                islogin = true,
                loginDatetime = DateTime.Now,
                token = tokenString
                 }; 
            
        }

        public async Task< EmployeeVM> Resigter(EmployeeVM model)
        {
            var user = _employee.GetAll().Where(x =>( x.userName == model.userName ||x.fullName==model.fullName)&& !x.IsDeleted).FirstOrDefault();
            if(user != null)
            {
                throw new Exception("user can not Resigter");
            }

            var emp = new Employee();
            emp.fullName = model.fullName;
            emp.userName = model.userName;
            emp.salary = model.salary;
            emp.gender = model.gender;
            emp.departmentId = model.departmentId;
            emp.hashPassword = config.GetValue<string>("saltKey");
            emp.Password = HashPassword(model.Password, config.GetValue<string>("saltKey"));
            _employee.Add(emp);
           if(  await _employee.SaveChanges()==1)
            {
                return model;
            }
            else
            {
                throw new Exception("error happen");
            }

           
        }



        private byte[] HashPassword(string password, string salt, string hashingAlgorithm = "HMACSHA256")
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            var saltyPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, saltyPasswordBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltyPasswordBytes, saltBytes.Length, passwordBytes.Length);
            var bytepass = new HMACSHA256(saltBytes).ComputeHash(saltyPasswordBytes);
             return bytepass;



        }


        private bool comparePassword(byte[] userPassword, string password, string salt, string hashingAlgorithm = "HMACSHA256")
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            var saltyPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, saltyPasswordBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltyPasswordBytes, saltBytes.Length, passwordBytes.Length);
            var bytepass = new HMACSHA256(saltBytes).ComputeHash(saltyPasswordBytes);
            var passuser = Convert.ToBase64String(userPassword);
            var modeluser = Convert.ToBase64String(bytepass);
            return passuser.Equals(modeluser);
        }

        public List<EmployeeVM> GetAllEmployee()
        {
            List<EmployeeVM> models = new List<EmployeeVM>();
            var allemp = _employee.GetAll().Include(x=>x.department);
            foreach (var item in allemp)
            {
                models.Add(new EmployeeVM()
                {
                    Id=item.Id,
                    departmentId=item.departmentId,
                    fullName=item.fullName,
                    gender=item.gender,
                    salary=item.salary,
                    userName=item.userName,
                     departmentName=item.department.Name,
                    isActive=!item.IsDeleted

                });
            }

            return models;
        }

        public async Task<EmployeeVM> UpdateEmployee(EmployeeVM model)
        {
            var emp = _employee.Get((int)model.Id);
            if (emp == null)
            {
                throw new Exception("Not Found");
            }
            emp.fullName = model.fullName;
            emp.salary = model.salary;
            emp.gender = model.gender;
            emp.departmentId = model.departmentId;
            await _employee.SaveChanges();
            return model;

        }

        public async Task<bool> DeleteEmployeeAsync(int Id)
        {
            var emp = _employee.Get(Id);
            if (emp == null)
            {
                throw new Exception("Not Found");
            }
            emp.IsDeleted = true;
            await _employee.SaveChanges();
            return true;
        }

        public EmployeeVM GetEmployee(int Id)
        {
            var emp = _employee.Get(Id);
            if (emp == null)
            {
                throw new Exception("Not Found");
            }

            return new EmployeeVM()
                {
                    departmentId = emp.departmentId,
                    fullName = emp.fullName,
                    gender = emp.gender,
                    salary = emp.salary,
                    userName = emp.userName
                };
            

        }

        public List<DepartmentVM> GetAllDepartment()
        {
            List<DepartmentVM> models = new List<DepartmentVM>();
            var department=_depatment.GetAll(true);
            foreach (var item in department)
            {
                models.Add(new DepartmentVM()
                {
                    id = item.Id,
                    name = item.Name
                });
            }
            return models;
        }
    }
}
