SELECT W.DepositGroup, SUM(W.DepositAmount) AS [TotalSum]
FROM WizzardDeposits W
GROUP BY W.DepositGroup