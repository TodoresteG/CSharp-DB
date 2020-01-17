SELECT TOP(5) c.CountryName AS [Country],
				ISNULL(p.PeakName, '(no highest peak)') AS [Highest Peak Name],
				ISNULL(p.Elevation, 0) AS [Highest Peak Eleveation],
				ISNULL(m.MountainRange, '(no mountain)') AS [Mountain]
FROM Countries c
LEFT OUTER JOIN MountainsCountries mc
ON mc.CountryCode = c.CountryCode
LEFT OUTER JOIN Mountains m
ON m.Id = mc.MountainId
LEFT OUTER JOIN Peaks p
ON p.MountainId = m.Id
ORDER BY c.CountryName ASC, p.PeakName ASC