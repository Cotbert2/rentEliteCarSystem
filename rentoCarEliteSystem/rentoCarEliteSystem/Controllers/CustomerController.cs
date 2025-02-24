using BussinesLayer;
using EntityLayer;
using EntityLayer.systemEntities;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ModalCreateCustomer()
        {
            return PartialView("ModalCreateCustomer");
        }


        [HttpPost]
        public  ResponseEL createCustomer([FromBody] CustomerEL customerToCreate)
        {
            CustomerBL myCustomer = new CustomerBL();
            return myCustomer.createCustomer(customerToCreate);
            
        }

        [HttpGet]
        public List<CustomerEL> getAllCustomer()
        {
            CustomerBL myCustomer = new CustomerBL();
            return myCustomer.getAllCustomer();
        }

        [HttpDelete]
        public  ResponseEL deleteCustomer(int id)
        {
            CustomerBL myCustomer = new CustomerBL();
            return myCustomer.deleteCustomer(id);
        }


        [HttpPut]
        public  ResponseEL updateCustomer([FromBody] CustomerEL customerToUpdate)
        {
            CustomerBL myCustomer = new CustomerBL();
            return myCustomer.updateCustomer(customerToUpdate);
        }
    }
}
