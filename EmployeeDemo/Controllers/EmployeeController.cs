using EmployeeData.Entities;
using EmployeeData.ViewModel;
using EmployeeRepository;
using EmployeeServices.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost("Resgister")]
        public async Task< ActionResult> Resgister([FromBody]EmployeeVM model)
        {
            return Ok( await _employeeService.Resigter(model));
        }
        [HttpPost("UpdateEmployee")]
        [Authorize]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeVM model)
        {
            return Ok(await _employeeService.UpdateEmployee(model));
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _employeeService.DeleteEmployeeAsync(id));
        }



        [HttpPost("Login")]
        public ActionResult Login(loginVM model)
        {
            return Ok(_employeeService.Login(model));
        }


        // GET: api/<EmployeeController>
     
        [HttpGet("GetAllEmployee"), Authorize]
        public ActionResult GetAllEmployee()
        {
            return Ok(_employeeService.GetAllEmployee());
        }

        // GET api/<EmployeeController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_employeeService.GetEmployee(id));
        }

     

        [HttpGet("GetAllDepartment")]
        public ActionResult GetAllDepartment()
        {
            return Ok(_employeeService.GetAllDepartment());
        }

 
     
    }
}
