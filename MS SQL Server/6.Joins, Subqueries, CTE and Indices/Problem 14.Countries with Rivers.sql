SELECT TOP(5) c.CountryName,
		r.RiverName
FROM Rivers r
FULL OUTER JOIN CountriesRivers cr
ON r.Id = cr.RiverId
FULL OUTER JOIN Countries c
ON cr.CountryCode = c.CountryCode
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName ASC