using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class InsuranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public EntityLayer.systemEntities.ResponseEL createInsurance([FromBody] EntityLayer.InsuranceEL insuranceToCreate)
        {
            BussinesLayer.InsuranceBL myInsurance = new BussinesLayer.InsuranceBL();
            return myInsurance.createInsurance(insuranceToCreate);
        }
    }
}
