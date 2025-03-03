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
            EntityLayer.systemEntities.ResponseEL response =  vehicleDL.deleteVehicle(vehicleId);

            //check for bookings
            if (response.code == 1)
            {
                BoookingBL bookingBL = new BoookingBL();
                List<BookingEL> bookings = bookingBL.getBookingsByVechileId(vehicleId);

                if (bookings.Count > 0)
                {
                    foreach (BookingEL booking in bookings)
                    {
                        bookingBL.deleteBooking(booking.bookingID);
                    }
                }
            }

            return response;
        }
    }
}
