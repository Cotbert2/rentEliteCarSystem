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


        public EntityLayer.systemEntities.ResponseEL updateVehicle(VehicleEL vehicle)
        {
            VehicleDL vehicleDL = new VehicleDL();
            return vehicleDL.updateVehicle(vehicle);
        }

        public EntityLayer.systemEntities.ResponseEL deleteVehicle(int vehicleId) {
            VehicleDL vehicleDL = new VehicleDL();
            return vehicleDL.deleteVehicle(vehicleId);
        }
    }
}
