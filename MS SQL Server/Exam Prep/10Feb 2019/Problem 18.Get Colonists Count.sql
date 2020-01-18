CREATE FUNCTION udf_GetColonistsCount(@PlanetName VARCHAR (30))
RETURNS INT
AS
BEGIN
	DECLARE @Count INT
	SET @Count = (
		SELECT COUNT(c.Id)
		FROM Planets p
		JOIN Spaceports s
		ON p.Id = s.PlanetId
		JOIN Journeys j
		ON s.Id = j.DestinationSpaceportId
		JOIN TravelCards tc
		ON j.Id = tc.JourneyId
		JOIN Colonists c
		ON tc.ColonistId = c.Id
		WHERE p.[Name] = @PlanetName
	)

	RETURN @Count
END