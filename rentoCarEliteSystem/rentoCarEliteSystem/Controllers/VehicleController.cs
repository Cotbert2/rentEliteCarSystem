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

        [HttpPost]
        public EntityLayer.systemEntities.ResponseEL createVehicle([FromBody] VehicleEL vehicle)
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.createVehicle(vehicle);
        }




        
        public List<VehicleEL> getAllVehicles()
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.getAllVehicles();
        }

        [HttpPut]
        public EntityLayer.systemEntities.ResponseEL updateVehicle([FromBody] VehicleEL myVehicle)
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.updateVehicle(myVehicle);
        }


        [HttpDelete]
        public EntityLayer.systemEntities.ResponseEL deleteVehicle(int vehicleId)
        {
            BussinesLayer.VehicleBL vehicleBL = new BussinesLayer.VehicleBL();
            return vehicleBL.deleteVehicle(vehicleId);
        }
    }
}
