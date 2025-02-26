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
