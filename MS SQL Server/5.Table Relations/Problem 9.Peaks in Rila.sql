SELECT m.MountainRange,
		p.PeakName,
		p.Elevation
FROM Peaks p
JOIN Mountains m
ON p.MountainId = M.Id
WHERE m.Id = 17
ORDER BY p.Elevation DESC