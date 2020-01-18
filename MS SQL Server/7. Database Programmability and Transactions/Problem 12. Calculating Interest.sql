CREATE OR ALTER PROC usp_CalculateFutureValueForAccount @AccountID INT, @InterestRate FLOAT
AS
SELECT a.Id,
		ah.FirstName,
		ah.LastName,
		a.Balance,
		dbo.ufn_CalculateFutureValue(a.Balance, @InterestRate, 5) AS [Balance in 5 years]
FROM AccountHolders ah
INNER JOIN Accounts a
ON ah.Id = a.AccountHolderId
WHERE a.Id = @AccountID

EXEC usp_CalculateFutureValueForAccount @AccountID = 1, @InterestRate = 0.1