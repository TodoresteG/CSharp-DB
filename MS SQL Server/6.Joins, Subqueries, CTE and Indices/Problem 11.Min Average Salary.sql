SELECT MIN(AvgOfAllDepartments.[Avg]) AS [MinAverageSalary]
FROM(
	SELECT AVG(e.Salary) AS [Avg],
		d.[Name]
	FROM Employees e
	INNER JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
	GROUP BY d.[Name]
) AS AvgOfAllDepartments