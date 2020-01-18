CREATE FUNCTION ufn_CalculateFutureValue (@Sum DECIMAL(18, 4), @Rate FLOAT, @Years INT)
RETURNS DECIMAL(18, 4)
AS
BEGIN
	RETURN @Sum * POWER(1 + @Rate, @Years)
END

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)
FROM AccountHolders