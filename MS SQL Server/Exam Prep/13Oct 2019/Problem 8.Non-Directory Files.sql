SELECT f1.Id,
		f1.[Name],
		CONCAT(f1.Size, 'KB') AS [Size]
FROM Files f1
LEFT OUTER JOIN Files f2
ON f1.Id = f2.ParentId
WHERE f2.ParentId IS NULL
ORDER BY f1.Id ASC, f1.[Name] ASC, f1.Size DESC