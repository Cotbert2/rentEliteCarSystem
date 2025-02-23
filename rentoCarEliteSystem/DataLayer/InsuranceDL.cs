
using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class InsuranceDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createInsurance(InsuranceEL myInsurance)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertInsurance", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookingId", myInsurance.booking.bookingID);
                    cmd.Parameters.AddWithValue("@SecureType", myInsurance.insuranceType);
                    cmd.Parameters.AddWithValue("@Amount", myInsurance.amount);
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
