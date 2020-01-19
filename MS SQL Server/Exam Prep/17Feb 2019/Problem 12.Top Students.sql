SELECT TOP(10) s.FirstName,
				s.LastName,
				FORMAT(AVG(se.Grade), 'N') AS [Grade]
FROM Students s
JOIN StudentsExams se
ON s.Id = se.StudentId
GROUP BY s.FirstName, s.LastName
ORDER BY Grade DESC, s.FirstName ASC, s.LastName ASC