using EntityLayer;
using EntityLayer.systemEntities;
namespace BussinesLayer
{
    public class CustomerBL
    {
        public  ResponseEL createCustomer(CustomerEL customer)
        {
            //validate for empty strings
            if (customer.firstName == "" || customer.lastName == ""
            || customer.email == "" || customer.phone == "")
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "All fields are required",
                };
            }

            //validate for null fileds
            if (customer.firstName == null || customer.lastName == null
                || customer.email == null || customer.phone == null)
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "All fields are required",
                };
            }

            //validate for regular expresion

            if (!(Utils.ValidateEmail(customer.email) &&
                Utils.ValidateName(customer.firstName) && Utils.ValidateName(customer.lastName)
                && Utils.ValidatePhone(customer.phone)) 
                )
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "fields error",
                };
            }

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

            //validate for empty strings
            if (customer.firstName == "" || customer.lastName == ""
            || customer.email == "" || customer.phone == "")
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "All fields are required",
                };
            }

            //validate for null fileds
            if (customer.firstName == null || customer.lastName == null
                || customer.email == null || customer.phone == null)
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "All fields are required",
                };
            }

            //validate for regular expresion

            if (!(Utils.ValidateEmail(customer.email) &&
                Utils.ValidateName(customer.firstName) && Utils.ValidateName(customer.lastName)
                && Utils.ValidatePhone(customer.phone))
                )
            {
                return new ResponseEL
                {
                    code = -1,
                    message = "fields error",
                };
            }

            DataLayer.CustomerDL customerDL = new DataLayer.CustomerDL();
            return customerDL.updateCustomer(customer);
        }

    }
}
