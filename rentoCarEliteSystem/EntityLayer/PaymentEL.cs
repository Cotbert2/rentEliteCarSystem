
namespace EntityLayer
{
    public class PaymentEL
    {
        public int paymentID { get; set; }
        public BookingEL booking { get; set; }
        public float amount { get; set; }
        public string paymentMethod { get; set; }
        public DateTime paymentDate { get; set; }
    }
}
