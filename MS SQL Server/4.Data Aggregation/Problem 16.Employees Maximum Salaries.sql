SELECT E.DepartmentID, MAX(E.Salary) AS [MaxSalary]
FROM Employees E
GROUP BY E.DepartmentID
HAVING MAX(E.Salary) NOT BETWEEN 30000 AND 70000