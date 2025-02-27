/*Store procedure to delete a customer*/

CREATE PROCEDURE sp_DeleteCustomer
    @CustomerId INT
AS
BEGIN
    
    DELETE FROM Customers
    WHERE Id = @CustomerId;
END;


CREATE PROCEDURE sp_DeleteVehicle
    @VehicleId INT
AS
BEGIN
--Change current status to "Deleted" in the vehicle table
    UPDATE Vehicles
    SET CurrentStatus = 'eliminado'
    WHERE Id = @VehicleId;
END;