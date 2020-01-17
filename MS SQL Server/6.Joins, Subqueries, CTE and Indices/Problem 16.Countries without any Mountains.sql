SELECT COUNT(*) AS [CountryCode]
FROM Countries c
FULL OUTER JOIN MountainsCountries mc
ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL