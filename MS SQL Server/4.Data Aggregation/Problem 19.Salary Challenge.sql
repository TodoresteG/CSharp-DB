SELECT TOP(10) E.FirstName,
		E.LastName,
		E.DepartmentID
FROM Employees E
WHERE E.Salary > (SELECT AVG(EMP.Salary) FROM Employees EMP WHERE E.DepartmentID = EMP.DepartmentID)
ORDER BY E.DepartmentID