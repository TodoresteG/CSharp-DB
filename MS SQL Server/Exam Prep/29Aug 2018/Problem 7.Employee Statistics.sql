SELECT e.FirstName,
		e.LastName,
		COUNT(o.Id) AS [Count]
FROM Employees e
JOIN Orders o
ON e.Id = o.EmployeeId
GROUP BY e.FirstName, e.LastName
ORDER BY [Count] DESC, e.FirstName ASC