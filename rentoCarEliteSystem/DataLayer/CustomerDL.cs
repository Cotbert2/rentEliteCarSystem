
using EntityLayer;
using EntityLayer.systemEntities;
using System.Data.SqlClient;

namespace DataLayer
{
    public class CustomerDL : DatabaseConnection
    {

        public  ResponseEL createCustomer(CustomerEL myCustomer)
        {
             ResponseEL response = new  ResponseEL();
            response.code = -1;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertCustomer", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", myCustomer.firstName);
                    cmd.Parameters.AddWithValue("@LastName", myCustomer.lastName);
                    cmd.Parameters.AddWithValue("@Phone", myCustomer.phone);
                    cmd.Parameters.AddWithValue("@Email", myCustomer.email);
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


        public List<CustomerEL> getAllCustomer()
        {
            List<CustomerEL> customers = new List<CustomerEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerEL currentCustomer = new CustomerEL();
                            currentCustomer.id = reader.GetInt32(0);
                            currentCustomer.firstName = reader.GetString(1);
                            currentCustomer.lastName = reader.GetString(2);
                            currentCustomer.phone = reader.GetString(3);
                            currentCustomer.email = reader.GetString(4);
                            customers.Add(currentCustomer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return customers;
            }
            return customers;
        }


        public  ResponseEL deleteCustomer(int customerId)
        {
             ResponseEL response = new  ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    response.code = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = ex.Message;
                return response;
            }
            return response;
        }

        public  ResponseEL updateCustomer(CustomerEL myCustomer)
        {
             ResponseEL response = new  ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", myCustomer.id);
                    cmd.Parameters.AddWithValue("@FirstName", myCustomer.firstName);
                    cmd.Parameters.AddWithValue("@LastName", myCustomer.lastName);
                    cmd.Parameters.AddWithValue("@Phone", myCustomer.phone);
                    cmd.Parameters.AddWithValue("@Email", myCustomer.email);
                    response.code = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = ex.Message;
                return response;
            }
            return response;
        }

        public CustomerEL getCustomerByBookingId(int bookingId)
        {
            CustomerEL customer = new CustomerEL();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetCustomerByBookingId", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.id = reader.GetInt32(0);
                            customer.firstName = reader.GetString(1);
                            customer.lastName = reader.GetString(2);
                            customer.phone = reader.GetString(3);
                            customer.email = reader.GetString(4);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return customer;
        }
    }
}
