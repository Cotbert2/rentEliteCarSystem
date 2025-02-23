

using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PaymentDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createPayment(PaymentEL myPayment)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertPayment", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookingId", myPayment.booking.bookingID);
                    cmd.Parameters.AddWithValue("@Amount", myPayment.amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", myPayment.paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentDate", myPayment.paymentDate);

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
