CREATE PROC usp_FindByExtension @extension VARCHAR(20)
AS
BEGIN
	SELECT f.Id,
			f.[Name],
			CONCAT(f.Size, 'KB') AS [Size]
	FROM Files f
	WHERE f.[Name] LIKE '%' + @extension + '%'
	ORDER BY f.Id ASC, f.[Name] ASC, f.Size DESC
END

GO
EXEC usp_FindByExtension 'txt'