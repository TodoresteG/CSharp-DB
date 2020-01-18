SELECT u.Username,
		g.[Name] AS [Game],
		COUNT(*) AS [Items Count],
		SUM(i.Price) AS [Item Price]
FROM Users u
FULL OUTER JOIN UsersGames ug
ON u.Id = ug.UserId
FULL OUTER JOIN Games g
ON ug.GameId = g.Id
FULL OUTER JOIN UserGameItems ugi
ON ug.Id = ugi.UserGameId
FULL OUTER JOIN Items i
ON i.Id = ugi.ItemId
GROUP BY u.Username, g.[Name]
HAVING COUNT(*) > 10
ORDER BY [Items Count] DESC, [Item Price] DESC, u.Username ASC