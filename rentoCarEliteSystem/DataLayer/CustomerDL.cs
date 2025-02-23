
using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class CustomerDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createCustomer(CustomerEL myCustomer)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = 500;
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
                response.code = 500;
                response.message = ex.Message;
                return response;
            }
            return response;
        }


    }
}
