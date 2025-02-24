

using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class BookingDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createBooking(BookingEL myBooking)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertBooking", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", myBooking.customer.id);
                    cmd.Parameters.AddWithValue("@VehicleId", myBooking.vehicle.VehicleId);
                    cmd.Parameters.AddWithValue("@StartDate", myBooking.startDate);
                    cmd.Parameters.AddWithValue("@EndDate", myBooking.endDate);
                    cmd.Parameters.AddWithValue("@BookingStatus", myBooking.bookingStatus);

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
