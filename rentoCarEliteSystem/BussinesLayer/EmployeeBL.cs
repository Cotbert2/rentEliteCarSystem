

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
    }
}
