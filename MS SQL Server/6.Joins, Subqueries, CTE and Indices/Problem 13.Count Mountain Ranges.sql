SELECT mc.CountryCode,
		COUNT(m.MountainRange) AS [MountainRanges]
FROM Mountains m
INNER JOIN MountainsCountries mc
ON m.Id = mc.MountainId
WHERE mc.CountryCode IN('BG', 'RU', 'US')
GROUP BY mc.CountryCode