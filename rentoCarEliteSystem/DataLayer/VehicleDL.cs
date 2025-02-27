
using EntityLayer;
using EntityLayer.systemEntities;
using System.Data.SqlClient;

namespace DataLayer
{
    public class VehicleDL : DatabaseConnection
    {

        public EntityLayer.systemEntities.ResponseEL createVehicle(VehicleEL vehicle)
        {
            EntityLayer.systemEntities.ResponseEL response = new EntityLayer.systemEntities.ResponseEL();
            response.code = -1;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertVehicle", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue ("@Brand", vehicle.brand);
                    cmd.Parameters.AddWithValue("@Model", vehicle.model);
                    cmd.Parameters.AddWithValue("@VehicleYear", vehicle.vehicleYear);
                    cmd.Parameters.AddWithValue("@Price", vehicle.price);
                    cmd.Parameters.AddWithValue("@Photo", vehicle.photo);
                    cmd.Parameters.AddWithValue("@CurrentStatus", "activo");
                    object result = cmd.ExecuteScalar();
                    int newCustomerId = result != null ? Convert.ToInt32(result) : 0;
                    response.code = newCustomerId;
                }

            }
            catch (Exception ex)
            {
                response.code = -1;
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
                            vehicle.photo = (reader.GetString(6) == null) ? "" : reader.GetString(6);
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

        public ResponseEL updateVehicle(VehicleEL myVehicle)
        {
            ResponseEL response = new ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateVehicle", getConnection()))

                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", myVehicle.VehicleId);
                    cmd.Parameters.AddWithValue("@Brand", myVehicle.brand);
                    cmd.Parameters.AddWithValue("@Model", myVehicle.model);
                    cmd.Parameters.AddWithValue("@VehicleYear", myVehicle.vehicleYear);
                    cmd.Parameters.AddWithValue("@Price", myVehicle.price);
                    cmd.Parameters.AddWithValue("@CurrentStatus", myVehicle.status);
                    cmd.Parameters.AddWithValue("@Photo", myVehicle.photo);
                    response.code = cmd.ExecuteNonQuery();
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

        public ResponseEL deleteVehicle(int VehicleId)
        {
            ResponseEL response = new ResponseEL();
            response.code = 500;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteVehicle", getConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                    response.code = cmd.ExecuteNonQuery();
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

        

    }
}
