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

/*Login Employees*/

CREATE PROCEDURE sp_LoginEmployee
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM Employees
    WHERE Email = @Email AND Password = @Password;
END;