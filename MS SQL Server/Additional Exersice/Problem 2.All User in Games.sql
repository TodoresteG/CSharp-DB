SELECT g.[Name] AS [Game],
		gt.[Name] AS [GameType],
		u.Username,
		ug.[Level],
		ug.Cash,
		ch.[Name] AS [Character]
FROM Users u
LEFT OUTER JOIN UsersGames ug
ON u.Id = ug.UserId
LEFT OUTER JOIN Games g
ON g.Id = ug.GameId
FULL OUTER JOIN Characters ch
ON ch.Id = ug.CharacterId
FULL OUTER JOIN GameTypes gt
ON g.GameTypeId = gt.Id
ORDER BY ug.[Level] DESC, u.Username ASC, g.[Name] ASC