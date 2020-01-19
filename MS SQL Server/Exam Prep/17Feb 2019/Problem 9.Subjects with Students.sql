SELECT x.FullName,
		x.Subjects,
		COUNT(st.StudentId) AS [Students]
FROM(
	SELECT CONCAT(t.FirstName, ' ', t.LastName) AS [FullName],
		CONCAT(s.[Name], '-', s.Lessons) AS [Subjects],
		t.Id
	FROM Teachers t
	JOIN Subjects s
	ON t.SubjectId = s.Id
) AS x
JOIN StudentsTeachers st
ON x.Id = st.TeacherId
GROUP BY x.FullName, x.Subjects
ORDER BY Students DESC, FullName ASC, Subjects ASC