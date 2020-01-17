SELECT e.EmployeeID,
		e.FirstName,
		CASE
			WHEN DATEPART(YEAR, p.StartDate) > 2004 THEN NULL
			ELSE p.[Name]
		END AS [ProjectName]
FROM Employees e
LEFT OUTER JOIN EmployeesProjects ep
ON e.EmployeeID = ep.EmployeeID
LEFT OUTER JOIN Projects p
ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24