CREATE TABLE Deleted_Employees (
	EmployeeId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50),
	LastName VARCHAR(50),
	MiddleName CHAR(1),
	JobTitle VARCHAR(50),
	DepartmentId INT,
	Salary DECIMAL(18, 4)
)

GO

CREATE TRIGGER tr_InsertIntoDeletedTable ON Employees
INSTEAD OF DELETE
AS
	INSERT INTO Deleted_Employees
	SELECT e.FirstName,
			e.LastName,
			e.MiddleName,
			e.JobTitle,
			e.DepartmentID,
			e.Salary
	FROM Employees e

DROP TABLE Employees