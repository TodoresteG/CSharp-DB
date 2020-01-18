CREATE TABLE Logs (
	LogId INT PRIMARY KEY IDENTITY,
	AccountId INT FOREIGN KEY REFERENCES Accounts(Id),
	OldSum DECIMAL(18, 2),
	NewSum DECIMAL(18, 2)
)

GO

CREATE TRIGGER tr_CreateTableLogs ON Accounts
AFTER UPDATE
AS
INSERT INTO Logs
SELECT i.Id,
		d.Balance,
		i.Balance
FROM inserted i
INNER JOIN deleted d
ON i.AccountHolderId = d.AccountHolderId

UPDATE Accounts
SET Balance += 10
WHERE Id = 1