CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(8)
AS BEGIN
	DECLARE @output VARCHAR(8)
	 IF @salary < 30000
	 BEGIN
		SET @output = 'Low'
	 END
	 ELSE IF @salary BETWEEN 30000 AND 50000
	 BEGIN
		SET @output = 'Average'
	 END
	 ELSE IF @salary > 50000
	 BEGIN
		SET @output = 'High'
	 END
	RETURN(@output)
END

SELECT dbo.ufn_GetSalaryLevel(25000)
FROM Employees e