SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [FullName],
		e.Phone AS [PhoneNumber]
FROM Employees e
WHERE e.Phone LIKE '3%'
ORDER BY e.FirstName ASC, e.Phone ASC