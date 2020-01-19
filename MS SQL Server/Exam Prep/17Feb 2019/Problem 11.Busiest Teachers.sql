SELECT TOP(10) t.FirstName,
		t.LastName,
		COUNT(st.StudentId) AS [Students]
FROM Teachers t
JOIN StudentsTeachers st
ON t.Id = st.TeacherId
GROUP BY t.FirstName, t.LastName
ORDER BY Students DESC, t.FirstName ASC, t.LastName ASC