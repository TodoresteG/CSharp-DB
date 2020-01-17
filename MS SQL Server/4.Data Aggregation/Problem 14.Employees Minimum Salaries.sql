SELECT E.DepartmentID, MIN(E.Salary) AS [MinimumSalary]
FROM Employees E
WHERE E.DepartmentID IN (2, 5, 7) AND E.HireDate > '01/01/2000'
GROUP BY E.DepartmentID