
namespace EntityLayer
{
    public class BookingEL
    {
        public int bookingID { get; set; }
        public CustomerEL customer { get; set; }
        public VehicleEL vehicle { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string bookingStatus { get; set; }

    }
}
