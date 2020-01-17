SELECT DISTINCT LEFT(W.FirstName, 1) AS [FirstLetter]
FROM WizzardDeposits W
WHERE W.DepositGroup = 'Troll Chest'
GROUP BY [FirstName]
ORDER BY FirstLetter