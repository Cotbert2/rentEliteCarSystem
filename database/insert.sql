CREATE PROCEDURE InsertVehicle
    @Brand NVARCHAR(50),
    @Model NVARCHAR(50),
    @VehicleYear INT,
    @Price DECIMAL(10,2),
    @CurrentStatus NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Vehicles (Brand, Model, VehicleYear, Price, CurrentStatus)
    VALUES (@Brand, @Model, @VehicleYear, @Price, @CurrentStatus);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewVehicleId;
END;


/*Insert customer*/

/*  CREATE TABLE Customers (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  Phone NVARCHAR(15) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL
  );*/

CREATE PROCEDURE sp_InsertCustomer
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Phone NVARCHAR(15),
    @Email NVARCHAR(100)

AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Customers (FirstName, LastName, Phone, Email)
    VALUES (@FirstName, @LastName, @Phone, @Email);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewCustomerId;
END;
sp_GetAllVehicles


/*  CREATE TABLE Employees (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  Position NVARCHAR(50) NOT NULL,
  Phone NVARCHAR(15) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL
  );*/

CREATE PROCEDURE sp_InsertEmployee
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Position NVARCHAR(50),
    @Phone NVARCHAR(15),
    @Email NVARCHAR(100),
    @Password NVARCHAR(32)

AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Employees (FirstName, LastName, Position, Phone, Email, Password)
    VALUES (@FirstName, @LastName, @Position, @Phone, @Email, @Password);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewEmployeeId;
END;



--   CREATE TABLE Bookings (
--   Id INT IDENTITY(1,1) PRIMARY KEY,
--   CustomerId INT FOREIGN KEY REFERENCES Customers(Id) ON DELETE CASCADE,
--   VehicleId INT FOREIGN KEY REFERENCES Vehicles(Id) ON DELETE CASCADE,
--   StartDate DATE NOT NULL,
--   EndDate DATE NOT NULL,
--   BookingStatus NVARCHAR(20) NOT NULL
--   );

CREATE PROCEDURE sp_InsertBooking
    @CustomerId INT,
    @VehicleId INT,
    @StartDate DATE,
    @EndDate DATE,
    @BookingStatus NVARCHAR(20)

AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Bookings (CustomerId, VehicleId, StartDate, EndDate, BookingStatus)
    VALUES (@CustomerId, @VehicleId, @StartDate, @EndDate, @BookingStatus);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewBookingId;
END;

/*  CREATE TABLE Payments (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  BookingId INT FOREIGN KEY REFERENCES Bookings(Id) ON DELETE CASCADE,
  Amount DECIMAL(10,2) NOT NULL,
  PaymentMethod NVARCHAR(50) NOT NULL,
  PaymentDate DATE NOT NULL
  );*/

CREATE PROCEDURE sp_InsertPayment
    @BookingId INT,
    @Amount DECIMAL(10,2),
    @PaymentMethod NVARCHAR(50),
    @PaymentDate DATE

AS

BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Payments (BookingId, Amount, PaymentMethod, PaymentDate)
    VALUES (@BookingId, @Amount, @PaymentMethod, @PaymentDate);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewPaymentId;
END;

/*  CREATE TABLE Insurance (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  BookingId INT FOREIGN KEY REFERENCES Bookings(Id) ON DELETE CASCADE,
  SecureType NVARCHAR(50) NOT NULL,
  Amount DECIMAL(10,2) NOT NULL
 );*/


CREATE PROCEDURE sp_InsertInsurance
    @BookingId INT,
    @SecureType NVARCHAR(50),
    @Amount DECIMAL(10,2)

AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Insurance (BookingId, SecureType, Amount)
    VALUES (@BookingId, @SecureType, @Amount);
    
    -- Devolver el ID del nuevo registro insertado
    SELECT SCOPE_IDENTITY() AS NewInsuranceId;
END;


--Manual insertions
INSERT INTO Vehicles (Brand, Model, VehicleYear, Price, CurrentStatus)
VALUES ('Toyota', 'Corolla', 2019, 20000, 'Available');

INSERT INTO Bookings (CustomerId, VehicleId, StartDate, EndDate, BookingStatus)
VALUES (1, 1, '2020-01-01', '2020-01-10', 'Pending');

