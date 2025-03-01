

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




        public List< EntityLayer.systemEntities.DashBoardEL> getDashBoardData()
        {
            List<EntityLayer.systemEntities.DashBoardEL> dashBoardList = new List<EntityLayer.systemEntities.DashBoardEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDashBoardData", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EntityLayer.systemEntities.DashBoardEL dashBoard = new EntityLayer.systemEntities.DashBoardEL();
                            dashBoard.booking = new BookingEL();
                            dashBoard.booking.customer = new CustomerEL();
                            dashBoard.booking.vehicle = new VehicleEL();
                            dashBoard.payment = new PaymentEL();
                            dashBoard.insurance = new InsuranceEL();

                            dashBoard.booking.bookingID = reader["BookingId"] != DBNull.Value ? Convert.ToInt32(reader["BookingId"]) : 0;
                            dashBoard.booking.startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : DateTime.MinValue;
                            dashBoard.booking.endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : DateTime.MinValue;
                            dashBoard.booking.bookingStatus = reader["BookingStatus"] != DBNull.Value ? Convert.ToString(reader["BookingStatus"]) : string.Empty;
                            dashBoard.booking.customer.firstName = reader["CustomerName"] != DBNull.Value ? Convert.ToString(reader["CustomerName"]) : string.Empty;
                            dashBoard.booking.customer.phone = reader["CustomerPhone"] != DBNull.Value ? Convert.ToString(reader["CustomerPhone"]) : string.Empty;
                            dashBoard.booking.customer.email = reader["CustomerEmail"] != DBNull.Value ? Convert.ToString(reader["CustomerEmail"]) : string.Empty;
                            dashBoard.booking.vehicle.brand = reader["Vehicle"] != DBNull.Value ? Convert.ToString(reader["Vehicle"]) : string.Empty;
                            dashBoard.booking.vehicle.vehicleYear = reader["VehicleYear"] != DBNull.Value ? Convert.ToInt32(reader["VehicleYear"]) : 0;
                            dashBoard.booking.vehicle.price = reader["VehiclePrice"] != DBNull.Value ? Convert.ToSingle(reader["VehiclePrice"]) : 0f;
                            dashBoard.payment.amount = reader["PaymentAmount"] != DBNull.Value ? Convert.ToSingle(reader["PaymentAmount"]) : 0f;
                            dashBoard.payment.paymentMethod = reader["PaymentMethod"] != DBNull.Value ? Convert.ToString(reader["PaymentMethod"]) : string.Empty;
                            dashBoard.payment.paymentDate = reader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaymentDate"]) : DateTime.MinValue;
                            dashBoard.insurance.insuranceType = reader["InsuranceType"] != DBNull.Value ? Convert.ToString(reader["InsuranceType"]) : string.Empty;
                            dashBoard.insurance.amount = reader["InsuranceAmount"] != DBNull.Value ? Convert.ToSingle(reader["InsuranceAmount"]) : 0f;

                            dashBoardList.Add(dashBoard);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return dashBoardList;

        }


    }

}
