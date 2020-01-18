CREATE FUNCTION ufn_IsWordComprised (@SetOfLetters VARCHAR(50), @Word VARCHAR(50))
RETURNS BIT
AS
BEGIN
	DECLARE @Index INT = 1
		WHILE (@Index <= LEN(@Word))
		BEGIN
			DECLARE @Symbol VARCHAR(1) = SUBSTRING(@Word, @Index, 1)
				IF(CHARINDEX(@Symbol, @SetOfLetters) = 0)
				BEGIN
					RETURN 0
				END
			SET @Index += 1
		END
		RETURN 1
END

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
FROM Employees e