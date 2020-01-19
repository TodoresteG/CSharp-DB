CREATE PROC usp_ExcludeFromSchool @StudentId INT
AS
BEGIN TRANSACTION
	IF(NOT EXISTS(SELECT Id FROM Students WHERE Id = @StudentId))
	BEGIN
		RAISERROR('This school has no student with the provided id!', 16, 1)
		ROLLBACK
		RETURN
	END

	DELETE FROM StudentsTeachers
	WHERE StudentId = @StudentId

	DELETE FROM StudentsExams
	WHERE StudentId = @StudentId

	DELETE FROM StudentsSubjects
	WHERE StudentId = @StudentId

	DELETE FROM Students
	WHERE Id = @StudentId
COMMIT

GO

EXEC usp_ExcludeFromSchool 1
SELECT COUNT(*) FROM Students
