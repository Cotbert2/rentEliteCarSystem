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

    }
}
