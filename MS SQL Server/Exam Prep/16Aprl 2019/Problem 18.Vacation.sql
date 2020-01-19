CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(30), @destination VARCHAR(30), @peopleCount INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	IF(@peopleCount <= 0)
	BEGIN
		RETURN 'Invalid people count!'
	END

	IF(NOT EXISTS(SELECT Origin FROM Flights WHERE Origin = @origin) OR NOT EXISTS(SELECT Destination FROM Flights WHERE Destination = @destination))
	BEGIN
		RETURN 'Invalid flight!' 
	END

	DECLARE @flightId INT
	SET @flightId = (
		SELECT f.Id
		FROM Flights f
		JOIN Tickets t
		ON f.Id = t.FlightId
		WHERE f.Origin = @origin AND f.Destination = @destination
	)

	DECLARE @priceForFlight DECIMAL(18, 2)
	SET @priceForFlight = (
		SELECT t.Price
		FROM Tickets t
		WHERE t.FlightId = @flightId
	)

	DECLARE @totalSum DECIMAL(18, 2)
	SET @totalSum = @priceForFlight * @peopleCount

	RETURN CONCAT('Total price', ' ', @totalSum)
END

GO

SELECT dbo.udf_CalculateTickets('Invalid','Rancabolang', 33)