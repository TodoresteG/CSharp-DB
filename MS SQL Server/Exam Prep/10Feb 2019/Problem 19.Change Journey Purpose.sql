CREATE PROC usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(30))
AS
BEGIN TRANSACTION
	DECLARE @ValidId INT
	SET @ValidId = (
		SELECT j.Id
		FROM Journeys j
		WHERE j.Id = @JourneyId
	)

	IF (@ValidId IS NULL) 
	BEGIN
		RAISERROR('The journey does not exist!', 16, 1)
		ROLLBACK
		RETURN
	END

	DECLARE @CurrentPurpose VARCHAR(30)
	SET @CurrentPurpose = (
		SELECT j.Purpose
		FROM Journeys j
		WHERE j.Id = @JourneyId
	)

	IF (@CurrentPurpose = @NewPurpose)
	BEGIN
		RAISERROR('You cannot change the purpose!', 16, 2)
		ROLLBACK
		RETURN
	END

	UPDATE Journeys
	SET Purpose = @NewPurpose
	WHERE Id = @JourneyId
COMMIT