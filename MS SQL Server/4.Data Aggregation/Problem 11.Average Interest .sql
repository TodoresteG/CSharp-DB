SELECT W.DepositGroup,
		W.IsDepositExpired,
		AVG(W.DepositInterest) AS [AverageInterest]
FROM WizzardDeposits W
WHERE W.DepositStartDate > '01/01/1985'
GROUP BY W.DepositGroup, W.IsDepositExpired
ORDER BY W.DepositGroup DESC, W.IsDepositExpired ASC