using BussinesLayer;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public EntityLayer.systemEntities.ResponseEL createCustomer(CustomerEL customerToCreate)
        {
            CustomerBL myCustomer = new CustomerBL();
            return myCustomer.createCustomer(customerToCreate);
            
        }
    }
}
