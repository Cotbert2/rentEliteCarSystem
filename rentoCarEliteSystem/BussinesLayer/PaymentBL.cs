

using EntityLayer;

namespace BussinesLayer
{
    public class PaymentBL
    {
        public EntityLayer.systemEntities.ResponseEL createPayment(PaymentEL payment)
        {
            DataLayer.PaymentDL paymentDL = new DataLayer.PaymentDL();
            return paymentDL.createPayment(payment);
        }
    }
}
