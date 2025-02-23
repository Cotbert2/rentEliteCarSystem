using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace rentoCarEliteSystem.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public EntityLayer.systemEntities.ResponseEL createVehicle(VehicleEL vehicle)
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.createVehicle(vehicle);
        }




        public List<VehicleEL> getAllVehicles()
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.getAllVehicles();
        }
    }
}
