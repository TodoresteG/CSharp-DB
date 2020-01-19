SELECT CASE
			WHEN s.MiddleName IS NULL THEN CONCAT(s.FirstName, ' ', s.LastName)
			ELSE CONCAT(s.FirstName, ' ', s.MiddleName, ' ', s.LastName)
	   END AS [FullName]
FROM Students s
LEFT OUTER JOIN StudentsSubjects ss
ON s.Id = ss.StudentId
WHERE ss.SubjectId IS NULL
ORDER BY FullName ASC