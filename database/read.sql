/*getAll Vehicles*/

CREATE PROCEDURE sp_GetAllVehicles
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM Vehicles;
END;

/**/