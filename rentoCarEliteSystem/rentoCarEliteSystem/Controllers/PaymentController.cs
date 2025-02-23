using BussinesLayer;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public EntityLayer.systemEntities.ResponseEL createPayment([FromBody ]PaymentEL paymentToCreate)
        {
            PaymentBL myPayment = new PaymentBL();
            return myPayment.createPayment(paymentToCreate);
        }
    }
}
