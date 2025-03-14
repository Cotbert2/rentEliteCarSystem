﻿

using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class EmployeeDL : DatabaseConnection
    {


        public EntityLayer.systemEntities.ResponseEL createEmployee(EmployeeEL myEmployee)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = -1;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertEmployee", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", myEmployee.firstName);
                    cmd.Parameters.AddWithValue("@LastName", myEmployee.lastName);
                    cmd.Parameters.AddWithValue("@Position", myEmployee.position);
                    cmd.Parameters.AddWithValue("@Phone", myEmployee.phone);
                    cmd.Parameters.AddWithValue("@Email", myEmployee.email);
                    cmd.Parameters.AddWithValue("@Password", myEmployee.password);
                    object result = cmd.ExecuteScalar();
                    int newCustomerId = result != null ? Convert.ToInt32(result) : 0;
                    response.code = newCustomerId;
                }

            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
                return response;
            }
            return response;
        }

        public EmployeeEL login(EmployeeEL myEmployee)
        {

            EmployeeEL currentEmployee = new EmployeeEL();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_LoginEmployee", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", myEmployee.email);
                    cmd.Parameters.AddWithValue("@Password", myEmployee.password);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentEmployee.employeeID = reader.GetInt32(0);
                            currentEmployee.firstName = reader.GetString(1);
                            currentEmployee.lastName = reader.GetString(2);
                            currentEmployee.position = reader.GetString(3);
                            currentEmployee.phone = reader.GetString(4);
                            currentEmployee.email = reader.GetString(5);
                        }
                        return currentEmployee;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return currentEmployee;
            }
        }





        public List<EmployeeEL> getAllEmployees()
        {
            List<EmployeeEL> employees = new List<EmployeeEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllEmployees", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeEL employee = new EmployeeEL();
                            /*    SELECT Id, FirstName, LastName, Position, Phone, Email FROM Employees;
*/

                            employee.employeeID = reader.GetInt32(0);
                            employee.firstName = reader.GetString(1);
                            employee.lastName = reader.GetString(2);
                            employee.position = reader.GetString(3);
                            employee.phone = reader.GetString(4);
                            employee.email = reader.GetString(5);
                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return employees;
            }
            return employees;
        }


        public EntityLayer.systemEntities.ResponseEL updateEmployee(EmployeeEL myEmployee)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = -1;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", myEmployee.employeeID);
                    cmd.Parameters.AddWithValue("@FirstName", myEmployee.firstName);
                    cmd.Parameters.AddWithValue("@LastName", myEmployee.lastName);
                    cmd.Parameters.AddWithValue("@Position", myEmployee.position);
                    cmd.Parameters.AddWithValue("@Phone", myEmployee.phone);
                    cmd.Parameters.AddWithValue("@Email", myEmployee.email);
                    cmd.Parameters.AddWithValue("@Password", myEmployee.password);
                    response.code = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
                return response;
            }
            return response;
        }


        public EntityLayer.systemEntities.ResponseEL deleteEmployee(int employeeId)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = -1;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    response.code = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
                return response;
            }
            return response;
        }



    }
}
