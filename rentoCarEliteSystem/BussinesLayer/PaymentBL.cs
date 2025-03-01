

using EntityLayer;

namespace BussinesLayer
{
    public class PaymentBL
    {
        public EntityLayer.systemEntities.ResponseEL createPayment(PaymentEL payment)
        {
            DataLayer.PaymentDL paymentDL = new DataLayer.PaymentDL();
            //TODO: compute payment amount
            payment.amount = 10;
            payment.paymentDate = System.DateTime.Now;

            return paymentDL.createPayment(payment);
        }
    }
}
