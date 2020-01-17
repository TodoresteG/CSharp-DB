SELECT  mc.CountryCode,
		m.MountainRange,
		p.PeakName,
		p.Elevation
FROM Peaks p
INNER JOIN Mountains m
ON p.MountainId = m.Id
INNER JOIN MountainsCountries mc
ON p.MountainId = mc.MountainId
WHERE p.Elevation > 2835 AND mc.CountryCode = 'BG'
ORDER BY p.Elevation DESC