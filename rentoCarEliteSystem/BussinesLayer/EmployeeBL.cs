

using DataLayer;
using EntityLayer;

namespace BussinesLayer
{
   public class EmployeeBL
    {
        public EntityLayer.systemEntities.ResponseEL createEmployee(EmployeeEL employee)
        {
            DataLayer.EmployeeDL employeeDL = new DataLayer.EmployeeDL();
            return employeeDL.createEmployee(employee);
        }


        public EmployeeEL login(EmployeeEL employee)
        {
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.login(employee);
        }

        public List<EmployeeEL> getAllEmployees()
        {
            EmployeeDL employeeDL = new EmployeeDL();

            return employeeDL.getAllEmployees();
        }

        public EntityLayer.systemEntities.ResponseEL updateEmployee(EmployeeEL employee)
        {
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.updateEmployee(employee);
        }


        public EntityLayer.systemEntities.ResponseEL deleteEmployee(int employeeId)
        {
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.deleteEmployee(employeeId);
        }
    }
}
