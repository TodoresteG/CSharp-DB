CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
	DECLARE @EmployeeDepartment VARCHAR(50)
	SET @EmployeeDepartment = (
		SELECT d.[Name]
		FROM Employees e
		JOIN Departments d
		ON e.DepartmentId = d.Id
		WHERE e.Id = @EmployeeId
	)

	DECLARE @ReportDepartment VARCHAR(50)
	SET @ReportDepartment = (
		SELECT d.[Name]
		FROM Reports r
		JOIN Categories c
		ON r.CategoryId = c.Id
		JOIN Departments d
		ON c.DepartmentId = d.Id
		WHERE r.Id = @ReportId
	)

	IF (@EmployeeDepartment <> @ReportDepartment)
	BEGIN
		RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1)
		RETURN
	END

	UPDATE Reports
	SET EmployeeId = @EmployeeId
	WHERE Id = @ReportId
END

GO

EXEC usp_AssignEmployeeToReport 17, 2