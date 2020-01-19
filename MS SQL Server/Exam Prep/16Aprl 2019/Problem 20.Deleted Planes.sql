CREATE TABLE DeletedPlanes (
	Id INT,
	[Name] VARCHAR(30),
	Seats INT,
	[Range] INT
)

GO 

CREATE TRIGGER tr_InsertDeletedPlanes
ON Planes
FOR DELETE
AS
BEGIN
	INSERT INTO DeletedPlanes
	SELECT * 
	FROM deleted
END

GO

DELETE Tickets
WHERE FlightId IN (SELECT Id FROM Flights WHERE PlaneId = 13)

DELETE FROM Flights
WHERE PlaneId = 13

DELETE FROM Planes
WHERE Id = 13
