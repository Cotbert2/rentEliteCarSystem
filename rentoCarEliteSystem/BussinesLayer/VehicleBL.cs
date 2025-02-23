using DataLayer;
using EntityLayer;

namespace BussinesLayer
{
    public class VehicleBL
    {
        public EntityLayer.systemEntities.ResponseEL createVehicle(VehicleEL vehicleToCreate)
        {
            VehicleDL vehicleDL = new VehicleDL();
            return vehicleDL.createVehicle(vehicleToCreate);
        }


        public List<VehicleEL> getAllVehicles()
        {
            VehicleDL vehicleDL = new VehicleDL();
            return vehicleDL.getAllVehicles();
        }
    }
}
