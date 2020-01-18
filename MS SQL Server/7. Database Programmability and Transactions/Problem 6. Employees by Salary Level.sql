CREATE PROCEDURE usp_EmployeesBySalaryLevel @Level VARCHAR(10)
AS
SELECT e.FirstName,
		e.LastName
FROM Employees e
WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @Level

EXEC usp_EmployeesBySalaryLevel @Level = 'High'