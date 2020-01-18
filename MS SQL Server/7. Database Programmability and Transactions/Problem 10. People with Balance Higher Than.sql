CREATE OR ALTER PROC usp_GetHoldersWithBalanceHigherThan @Balance DECIMAL(18, 2)
AS
SELECT ah.FirstName,
		ah.LastName
FROM AccountHolders ah
LEFT OUTER JOIN Accounts a
ON ah.Id = a.AccountHolderId
GROUP BY ah.FirstName, ah.LastName
HAVING SUM(a.Balance) > @Balance
ORDER BY ah.FirstName ASC

EXEC usp_GetHoldersWithBalanceHigherThan @Balance = 20000