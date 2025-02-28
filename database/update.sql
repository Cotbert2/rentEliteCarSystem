CREATE PROCEDURE UpdateCustomer
    @Id INT,
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL,
    @Email NVARCHAR(255) = NULL,
    @Phone NVARCHAR(20) = NULL,
AS
BEGIN

    UPDATE Customers
    SET 
        FirstName = COALESCE(@FirstName, FirstName),
        LastName = COALESCE(@LastName, LastName),
        Email = COALESCE(@Email, Email),
        Phone = COALESCE(@Phone, Phone),
    WHERE Id = @Id;
END;

/*  Id INT IDENTITY(1,1) PRIMARY KEY,
  Brand NVARCHAR(50) NOT NULL,
  Model NVARCHAR(50) NOT NULL,
  VehicleYear INT NOT NULL,
  Price DECIMAL(10,2) NOT NULL,
  CurrentStatus NVARCHAR(20) NOT NULL,
  Photo NVARCHAR(100) NOT NULL*/

CREATE PROCEDURE sp_UpdateVehicle
    @Id INT,
    @Brand NVARCHAR(50) = NULL,
    @Model NVARCHAR(50) = NULL,
    @VehicleYear INT = NULL,
    @Price DECIMAL(10,2) = NULL,
    @CurrentStatus NVARCHAR(20) = NULL
    @Photo NVARCHAR(100) = NULL
AS
BEGIN

    UPDATE Vehicles
    SET 
        Brand = COALESCE(@Brand, Brand),
        Model = COALESCE(@Model, Model),
        VehicleYear = COALESCE(@VehicleYear, VehicleYear),
        Price = COALESCE(@Price, Price),
        CurrentStatus = COALESCE(@CurrentStatus, CurrentStatus),
        Photo = COALESCE(@Photo, Photo)
    WHERE Id = @Id;
END;



/*  Id INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  Position NVARCHAR(50) NOT NULL,
  Phone NVARCHAR(15) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL*/


CREATE PROCEDURE sp_UpdateEmployee
    @Id INT,
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL,
    @Position NVARCHAR(50) = NULL,
    @Phone NVARCHAR(15) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Password NVARCHAR(255) = NULL

AS
BEGIN

    UPDATE Employees
    SET 
        FirstName = COALESCE(@FirstName, FirstName),
        LastName = COALESCE(@LastName, LastName),
        Position = COALESCE(@Position, Position),
        Phone = COALESCE(@Phone, Phone),
        Email = COALESCE(@Email, Email),
        Password = COALESCE(@Password, Password)
    WHERE Id = @Id;
END;
