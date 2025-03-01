

using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class BookingDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createBooking(BookingEL myBooking)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = -1;
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
                response.code = -1;
                response.message = ex.Message;
                return response;
            }
            return response;
        }

        public List<BookingEL> getBookingsByCustomerId(int customerId){
            List<BookingEL> bookings = new List<BookingEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_getBookingsByCustomerId", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingEL booking = new BookingEL();
                            booking.bookingID = Convert.ToInt32(reader["Id"]);
                            booking.startDate = Convert.ToDateTime(reader["StartDate"]);
                            booking.endDate = Convert.ToDateTime(reader["EndDate"]);
                            booking.bookingStatus = Convert.ToString(reader["BookingStatus"]);
                            booking.customer = new CustomerEL();
                            booking.customer.id = Convert.ToInt32(reader["CustomerId"]);
                            booking.vehicle = new VehicleEL();
                            booking.vehicle.VehicleId = Convert.ToInt32(reader["VehicleId"]);
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return bookings;
        }

        public List<BookingEL> getBookingsByVehicleId(int vehicleId)
        {
            List<BookingEL> bookings = new List<BookingEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_getBookingsByVehicleId", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingEL booking = new BookingEL();
                            booking.bookingID = Convert.ToInt32(reader["Id"]);
                            booking.startDate = Convert.ToDateTime(reader["StartDate"]);
                            booking.endDate = Convert.ToDateTime(reader["EndDate"]);
                            booking.bookingStatus = Convert.ToString(reader["BookingStatus"]);
                            booking.customer = new CustomerEL();
                            booking.customer.id = Convert.ToInt32(reader["CustomerId"]);
                            booking.vehicle = new VehicleEL();
                            booking.vehicle.VehicleId = Convert.ToInt32(reader["VehicleId"]);
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return bookings;
        }


    }
}
