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


        [HttpGet]
        public List<EntityLayer.BookingEL> getBookingsByCustomerId(int customerId)
        {
            BoookingBL myBooking = new BoookingBL();
            return myBooking.getBookingsByCustomerId(customerId);
        }

        public List<EntityLayer.BookingEL> getBookingsByVechileId(int vehicleId)
        {
            BoookingBL myBooking = new BoookingBL();
            return myBooking.getBookingsByVechileId(vehicleId);
        }


        public List<EntityLayer.systemEntities.DashBoardEL> getDashBoardData()
        {
            BoookingBL myBooking = new BoookingBL();
            return myBooking.getDashBoardData();
        }

        [HttpDelete]
        public EntityLayer.systemEntities.ResponseEL deleteBooking(int bookingId)
        {
            BoookingBL myBooking = new BoookingBL();
            return myBooking.deleteBooking(bookingId);
        }
    }
}
