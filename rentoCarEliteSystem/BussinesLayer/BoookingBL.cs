using EntityLayer;

namespace BussinesLayer
{
    public class BoookingBL
    {

        public EntityLayer.systemEntities.ResponseEL createBooking(BookingEL booking)
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.createBooking(booking);
        }

        public List<BookingEL> getBookingsByCustomerId(int customerId)
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.getBookingsByCustomerId(customerId);
        }



        public List<BookingEL> getBookingsByVechileId(int vehicleId)
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.getBookingsByVehicleId(vehicleId);
        }

    }
}
