SELECT *
FROM Planes p
WHERE p.[Name] LIKE '%tr%'
ORDER BY p.Id ASC, p.[Name] ASC, p.Seats ASC, p.[Range] ASC