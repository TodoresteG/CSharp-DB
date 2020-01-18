CREATE TABLE NotificationEmails (
	Id INT PRIMARY KEY IDENTITY,
	Recipient INT FOREIGN KEY REFERENCES Accounts(Id),
	[Subject] VARCHAR(MAX),
	Body VARCHAR(MAX)
)

GO

CREATE TRIGGER tr_MakeEmailNotification 
ON Logs
AFTER INSERT
AS
INSERT INTO NotificationEmails
SELECT i.AccountId,
		CONCAT('Balance change for account:' , ' ', i.AccountId),
		CONCAT('On', ' ', GETDATE(), ' ', 'your balance was changed from', ' ', i.OldSum, ' ', 'to', ' ', i.NewSum, '.')
FROM inserted i

UPDATE Accounts
SET Balance += 100
WHERE Id = 11