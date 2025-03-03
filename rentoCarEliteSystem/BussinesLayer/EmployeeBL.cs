using Azure;
using DataLayer;
using EntityLayer;
using  EntityLayer.systemEntities;

namespace BussinesLayer
{
   public class EmployeeBL
    {
        public ResponseEL createEmployee(EmployeeEL employee)
        {
            if ( employee == null || string.IsNullOrEmpty(employee.firstName) || string.IsNullOrEmpty(employee.lastName) || string.IsNullOrEmpty(employee.position) || string.IsNullOrEmpty(employee.email) || string.IsNullOrEmpty(employee.phone) || string.IsNullOrEmpty(employee.password))
            {
                return new ResponseEL()
                {
                    code = -1,
                    message = "All fields are required",
                };
            }


            if (!Utils.ValidateEmail(employee.email)
            || !Utils.ValidatePhone(employee.phone)
            || !Utils.ValidateName(employee.firstName)
            || !Utils.ValidateName(employee.lastName)
            || !Utils.validatePosition(employee.position))
            {
                return new ResponseEL()
                {
                    code = -1,
                    message = "Invalid data",
                };
            }
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.createEmployee(employee);
        }


        public EmployeeEL login(EmployeeEL employee)
        {
            if (employee.email == null || employee.password == null) return null;
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.login(employee);
        }

        public List<EmployeeEL> getAllEmployees()
        {
            EmployeeDL employeeDL = new EmployeeDL();

            return employeeDL.getAllEmployees();
        }

        public ResponseEL updateEmployee(EmployeeEL employee)
        {

            if (!Utils.ValidateEmail(employee.email)
            || !Utils.ValidatePhone(employee.phone)
            || !Utils.ValidateName(employee.firstName)
            || !Utils.ValidateName(employee.lastName)
            || !Utils.validatePosition(employee.position))
            {
                return new ResponseEL()
                {
                    code = -1,
                    message = "Invalid data",
                };
            }
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.updateEmployee(employee);
        }


        public ResponseEL deleteEmployee(int employeeId)
        {
            if (employeeId <= 0)
            {
                return new EntityLayer.systemEntities.ResponseEL()
                {
                    code = -1,
                    message = "Invalid employee id",
                };
            }
            EmployeeDL employeeDL = new EmployeeDL();
            return employeeDL.deleteEmployee(employeeId);
        }
    }
}
