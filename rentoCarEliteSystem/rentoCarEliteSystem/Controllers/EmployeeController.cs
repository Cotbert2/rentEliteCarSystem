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

        [HttpGet]
        public List<EmployeeEL> getAllEmployees() {
            BussinesLayer.EmployeeBL myEmployee = new BussinesLayer.EmployeeBL();
            return myEmployee.getAllEmployees();
        }

        [HttpPut]
        public EntityLayer.systemEntities.ResponseEL updateEmployee([FromBody] EmployeeEL employeeToUpdate)
        {
            BussinesLayer.EmployeeBL myEmployee = new BussinesLayer.EmployeeBL();
            return myEmployee.updateEmployee(employeeToUpdate);
        }


        [HttpDelete]
        public EntityLayer.systemEntities.ResponseEL deleteEmployee( int id)
        {
            BussinesLayer.EmployeeBL myEmployee = new BussinesLayer.EmployeeBL();
            return myEmployee.deleteEmployee(id);
        }

        
    }
}
