CREATE PROCEDURE usp_GetTownsStartingWith @FirstLetter VARCHAR(30)
AS
SELECT t.[Name] AS [Town]
FROM Towns t
WHERE LEFT(t.[Name], LEN(@FirstLetter)) = @FirstLetter

EXEC usp_GetTownsStartingWith @FirstLetter = 'b'