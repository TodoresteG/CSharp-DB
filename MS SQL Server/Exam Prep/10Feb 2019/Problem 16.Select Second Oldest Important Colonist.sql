SELECT x.JobDuringJourney,
		x.FullName,
		x.JobRank
FROM (
	SELECT tc.JobDuringJourney,
		CONCAT(c.FirstName, ' ', c.LastName) AS [FullName],
		DENSE_RANK() OVER (PARTITION BY tc.JobDuringJourney ORDER BY DATEDIFF(DAY, c.BirthDate, '01/01/2019') DESC) AS [JobRank]
	FROM Colonists c
	JOIN TravelCards tc
	ON c.Id = tc.ColonistId
) AS x
WHERE x.JobRank = 2