CREATE PROC usp_DepositMoney @AccountId INT, @MoneyAmount DECIMAL(18, 4)
AS
BEGIN TRANSACTION
	IF (NOT EXISTS(SELECT * FROM Accounts WHERE Id = @AccountId))
	BEGIN
		RAISERROR('Account does not exists', 16, 1)
		ROLLBACK
		RETURN
	END

	DECLARE @balance DECIMAL(18, 4) = (SELECT Balance FROM Accounts WHERE Id = @AccountId)

	IF(@balance < 0)
	BEGIN
		RAISERROR('Non positive balance', 16, 2)
		ROLLBACK
		RETURN
	END

	UPDATE Accounts
	SET Balance += @MoneyAmount
	WHERE Id = @AccountId
COMMIT


EXEC usp_DepositMoney 1, 10