

using EntityLayer;

namespace BussinesLayer
{
    public class CustomerBL
    {
        public EntityLayer.systemEntities.ResponseEL createCustomer(CustomerEL customer)
        {
            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.createCustomer(customer);
        }
    }
}
