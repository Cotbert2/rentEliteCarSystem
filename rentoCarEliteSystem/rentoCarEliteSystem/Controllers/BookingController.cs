using BussinesLayer;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public EntityLayer.systemEntities.ResponseEL createBooking([FromBody] EntityLayer.BookingEL bookingToCreate)
        {
            BoookingBL myBooking = new BoookingBL();
            return myBooking.createBooking(bookingToCreate);
        }
    }
}
