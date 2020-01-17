SELECT TOP(5) e.EmployeeID,
				e.JobTitle,
				a.AddressID,
				a.AddressText
FROM Employees e
INNER JOIN Addresses a ON e.AddressID = A.AddressID
ORDER BY a.AddressID ASC