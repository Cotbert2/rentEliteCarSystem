using EntityLayer;
using EntityLayer.systemEntities;
namespace BussinesLayer
{
    public class CustomerBL
    {
        public  ResponseEL createCustomer(CustomerEL customer)
        {
            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.createCustomer(customer);
        }

        public List<CustomerEL> getAllCustomer()
        {
            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.getAllCustomer();
        }


        public  ResponseEL deleteCustomer(int id)
        {
            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.deleteCustomer(id);
        }


        public  ResponseEL updateCustomer(CustomerEL customer)
        {
            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.updateCustomer(customer);
        }
    }
}
