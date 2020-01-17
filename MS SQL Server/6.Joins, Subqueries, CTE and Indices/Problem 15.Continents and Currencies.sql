SELECT x.ContinentCode,
		x.CurrencyCode,
		x.CurrencyUsage
FROM (SELECT c.ContinentCode,
		c.CurrencyCode,
		COUNT(c.CurrencyCode) AS [CurrencyUsage],
		DENSE_RANK() OVER (PARTITION BY c.ContinentCode ORDER BY COUNT(c.CurrencyCode) DESC) AS [Rank]
FROM Countries c
GROUP BY c.ContinentCode, c.CurrencyCode
HAVING COUNT(c.CurrencyCode) > 1) AS x
WHERE x.[Rank] = 1
ORDER BY x.ContinentCode
