CREATE PROCEDURE usp_GetEmployeesFromTown @Town VARCHAR(30)
AS
SELECT e.FirstName,
		e.LastName
FROM Employees e
LEFT OUTER JOIN Addresses a
ON e.AddressID = a.AddressID
LEFT OUTER JOIN Towns t
ON a.TownID = t.TownID
WHERE t.[Name] = @Town

EXEC usp_GetEmployeesFromTown @Town = 'Sofia'
