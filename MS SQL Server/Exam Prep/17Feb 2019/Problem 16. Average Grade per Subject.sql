SELECT s.[Name],
		AVG(ss.Grade) AS [AverageGrade]
FROM Subjects s
JOIN StudentsSubjects ss
ON s.Id = ss.SubjectId
GROUP BY s.[Name], s.Id
ORDER BY s.Id