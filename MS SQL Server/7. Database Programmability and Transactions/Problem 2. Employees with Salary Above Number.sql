CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @Salary DECIMAL(18, 4)
AS
SELECT e.FirstName,
		e.LastName
FROM Employees e
WHERE e.Salary >= @Salary

EXEC usp_GetEmployeesSalaryAboveNumber @Salary = 48100