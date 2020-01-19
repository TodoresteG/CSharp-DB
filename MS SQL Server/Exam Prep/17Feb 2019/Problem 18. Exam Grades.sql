CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(18, 2))
RETURNS VARCHAR(MAX)
AS
BEGIN
	IF (@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!'
	END
	
	DECLARE @existingId INT
	SET @existingId = (
		SELECT s.Id
		FROM Students s
		WHERE s.Id = @studentId
	)

	IF (@existingId IS NULL)
	BEGIN
		RETURN 'The student with provided id does not exist in the school!'
	END

	DECLARE @count INT
	SET @count = (
		SELECT COUNT(*)
		FROM Students s
		JOIN StudentsSubjects ss
		ON s.Id = ss.StudentId
		WHERE s.Id = @studentId AND ss.Grade > @grade AND ss.Grade <= @grade + 0.50
	)

	DECLARE @studentName VARCHAR(30)
	SET @studentName = (
		SELECT s.FirstName
		FROM Students s
		WHERE s.Id = @studentId
	)

	RETURN CONCAT('You have to update ', @count, ' grades for the student ', @studentName)
END

GO

SELECT dbo.udf_ExamGradesToUpdate(12, 6.20)
SELECT dbo.udf_ExamGradesToUpdate(12, 5.50)
SELECT dbo.udf_ExamGradesToUpdate(121, 5.50)