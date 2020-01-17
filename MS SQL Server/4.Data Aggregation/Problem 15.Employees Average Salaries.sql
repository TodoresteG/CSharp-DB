SELECT * 
INTO EmployeesNewTable
FROM Employees E
WHERE E.Salary > 30000

DELETE FROM EmployeesNewTable 
WHERE ManagerID = 42

UPDATE EmployeesNewTable
SET Salary += 5000
WHERE DepartmentID = 1

SELECT E.DepartmentID, AVG(E.Salary) AS [AverageSalary]
FROM EmployeesNewTable E
GROUP BY E.DepartmentID