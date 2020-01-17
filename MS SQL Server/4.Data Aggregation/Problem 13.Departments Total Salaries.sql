SELECT E.DepartmentID, SUM(E.Salary) AS [TotalSalary]
FROM Employees E
GROUP BY E.DepartmentID
ORDER BY E.DepartmentID