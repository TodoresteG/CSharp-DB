CREATE PROC usp_TransferMoney @SenderId INT, @ReceiverId INT, @Amount DECIMAL(18, 4)
AS
BEGIN TRANSACTION
	IF(NOT EXISTS(SELECT * FROM Accounts WHERE Id = @SenderId) AND NOT EXISTS(SELECT * FROM Accounts WHERE Id = @ReceiverId))
	BEGIN
		RAISERROR('Account not valiable', 16, 1)
		ROLLBACK
		RETURN
	END

	DECLARE @Balance DECIMAL(18, 4) = (SELECT Balance FROM Accounts WHERE Id = @SenderId)

	IF(@Balance < @Amount)
	BEGIN
		RAISERROR('Inciffitient amount', 16, 2)
		ROLLBACK
		RETURN
	END

	UPDATE Accounts
	SET Balance -= @Amount
	WHERE Id = @SenderId

	UPDATE Accounts
	SET Balance += @Amount
	WHERE Id = @ReceiverId
COMMIT

EXEC usp_TransferMoney 5, 1, 5000