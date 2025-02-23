

namespace EntityLayer
{
    public class InsuranceEL
    {
        public int insuranceID { get; set; }
        public BookingEL booking { get; set; }

        public string insuranceType { get; set; }

        public float amount { get; set; }
    }
}
