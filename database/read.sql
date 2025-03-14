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



CREATE PROCEDURE sp_getBookingsByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT VehicleId, StartDate, EndDate, BookingStatus  FROM Bookings
    WHERE CustomerId = @CustomerId;
END;

EXEC sp_getBookingsByCustomerId 1;


CREATE PROCEDURE sp_getBookingsByVehicleId
    @VehicleId INT

AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT CustomerId, StartDate, EndDate, BookingStatus  FROM Bookings
    WHERE VehicleId = @VehicleId and BookingStatus = 'ACTIVE'
    --all fields should be not null
    and CustomerId is not null and StartDate is not null and EndDate is not null and BookingStatus is not null;

END;


	CREATE PROCEDURE sp_getBookingsByVehicleId
    @VehicleId INT
AS
BEGIN
    SET NOCOUNT ON;
--JOIN
    SELECT B.Id as Id, CustomerId, StartDate, EndDate, BookingStatus, VehicleId FROM Payments P
    JOIN Bookings B ON P.BookingId = B.Id
    WHERE B.VehicleId =  @VehicleId and BookingStatus = 'ACTIVE'
END;




CREATE PROCEDURE sp_GetDashboardData
AS
BEGIN
SELECT
    B.Id AS BookingId,
    C.FirstName + ' ' + C.LastName AS CustomerName,
    C.Phone AS CustomerPhone,
    C.Email AS CustomerEmail,
    V.Brand + ' ' + V.Model AS Vehicle,
    V.VehicleYear,
    V.Price AS VehiclePrice,
    B.StartDate,
    B.EndDate,
    B.BookingStatus,
    P.Amount AS PaymentAmount,
    P.PaymentMethod,
    P.PaymentDate,
    I.SecureType AS InsuranceType,
    I.Amount AS InsuranceAmount
FROM Bookings B
JOIN Customers C ON B.CustomerId = C.Id
JOIN Vehicles V ON B.VehicleId = V.Id
LEFT JOIN Payments P ON B.Id = P.BookingId
LEFT JOIN Insurance I ON B.Id = I.BookingId
WHERE B.BookingStatus = 'Active'
ORDER BY B.Id;
END;


/*Store procedure for populated bookings*/



CREATE PROCEDURE sp_GetInsuranceByBookingId
    @BookingId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM Insurance
    WHERE BookingId = @BookingId;
END;


CREATE PROCEDURE sp_getCustomerByBookingId
    @BookingId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT C.Id, C.FirstName, C.LastName, C.Phone, C.Email FROM Customers C
    JOIN Bookings B ON C.Id = B.CustomerId
    WHERE B.Id = @BookingId;
END;