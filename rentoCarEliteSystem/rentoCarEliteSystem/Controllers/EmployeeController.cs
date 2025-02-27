using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login() { 
            return View(); 
        }

        [HttpPost]
        public EntityLayer.systemEntities.ResponseEL createEmployee([FromBody] EntityLayer.EmployeeEL employeeToCreate)
        {
            BussinesLayer.EmployeeBL myEmployee = new BussinesLayer.EmployeeBL();
            return myEmployee.createEmployee(employeeToCreate);

        }


        [HttpPost]
        public EmployeeEL login([FromBody] EmployeeEL employee)
        {
            BussinesLayer.EmployeeBL myEmployee = new BussinesLayer.EmployeeBL();
            return myEmployee.login(employee);
        }
    }
}
