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


/*  Id INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  Position NVARCHAR(50) NOT NULL,
  Phone NVARCHAR(15) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL*/

CREATE PROCEDURE sp_getAllEmployees
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, FirstName, LastName, Position, Phone, Email FROM Employees;
END;