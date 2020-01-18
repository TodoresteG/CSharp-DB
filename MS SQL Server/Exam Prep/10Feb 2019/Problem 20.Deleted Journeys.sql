CREATE TRIGGER tr_InsertDeletedJourneys
ON Journeys
FOR DELETE
AS
BEGIN
	INSERT INTO DeletedJourneys
	SELECT *
	FROM deleted d
END

GO

DELETE FROM TravelCards
WHERE JourneyId =  2


DELETE FROM Journeys
WHERE Id =  2