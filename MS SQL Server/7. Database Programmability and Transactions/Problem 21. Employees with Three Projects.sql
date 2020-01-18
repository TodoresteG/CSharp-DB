CREATE PROC usp_AssignProject @EmployeeId INT, @ProjectId INT
AS
BEGIN TRANSACTION
	DECLARE @ProjectCount INT = (SELECT COUNT(EmployeeID) FROM EmployeesProjects WHERE EmployeeID = @EmployeeId)

	IF(@ProjectCount > 3)
	BEGIN
		RAISERROR('The employee has too many projects!', 16, 1)
		ROLLBACK
		RETURN
	END

	INSERT INTO EmployeesProjects (EmployeeID, ProjectID)
	VALUES (@EmployeeId, @ProjectId)
COMMIT

EXEC usp_AssignProject 1, 4