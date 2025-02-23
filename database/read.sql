/*getAll Vehicles*/

CREATE PROCEDURE sp_GetAllVehicles
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM Vehicles;
END;

/*getAll Customers*/

CREATE PROCEDURE sp_GetAllCustomers
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM Customers;
END;