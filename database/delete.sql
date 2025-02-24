/*Store procedure to delete a customer*/

CREATE PROCEDURE sp_DeleteCustomer
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM Customers
    WHERE Id = @CustomerId;
END;