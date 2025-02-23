

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
    }
}
