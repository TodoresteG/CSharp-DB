SELECT CONCAT(s.FirstName, ' ', s.LastName) AS [FullName]
FROM Students s
FULL OUTER JOIN StudentsExams se
ON s.Id = se.StudentId
FULL OUTER JOIN Exams e
ON se.ExamId = e.Id
WHERE se.ExamId IS NULL
ORDER BY FullName ASC