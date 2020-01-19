SELECT s.FirstName,
		s.LastName,
		COUNT(st.TeacherId) AS [TeachersCount]
FROM Students s
JOIN StudentsTeachers st
ON s.Id = st.StudentId
GROUP BY s.FirstName, s.LastName