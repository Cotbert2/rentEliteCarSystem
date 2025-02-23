
using EntityLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class VehicleDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createVehicle(VehicleEL vehicle)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertVehicle", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue ("@Brand", vehicle.brand);
                    cmd.Parameters.AddWithValue("@Model", vehicle.model);
                    cmd.Parameters.AddWithValue("@VehicleYear", vehicle.vehicleYear);
                    cmd.Parameters.AddWithValue("@Price", vehicle.price);
                    cmd.Parameters.AddWithValue("@CurrentStatus", vehicle.status);
                    response.code = cmd.ExecuteNonQuery() == 1 ? 200 : 500;
                }

            }
            catch (Exception ex)
            {
                response.code = 500;
                response.message = ex.Message;
                return response;
            }
            return response;
        }
        
        public List<VehicleEL> getAllVehicles()
        {
            List<VehicleEL> vehicles = new List<VehicleEL>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllVehicles", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VehicleEL vehicle = new VehicleEL();
                            vehicle.VehicleId = reader.GetInt32(0);
                            vehicle.brand = reader.GetString(1);
                            vehicle.model = reader.GetString(2);
                            vehicle.vehicleYear = reader.GetInt32(3);
                            vehicle.price = (float) reader.GetDecimal(4);
                            vehicle.status = reader.GetString(5);
                            vehicles.Add(vehicle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return vehicles;
            }
            return vehicles;
        }

    }
}
