SELECT W.DepositGroup, SUM(W.DepositAmount) AS [TotalSum]
FROM WizzardDeposits W
WHERE W.MagicWandCreator = 'Ollivander family'
GROUP BY W.DepositGroup