Create or Alter VIEW [vwGetIncomesAndExpenses] as
select 
[Transaction].[UserId],
Month([Transaction].[PaidOrReceivedAt]) as [Month],
Year([Transaction].[PaidOrReceivedAt]) as [Year],
Sum(case when [Transaction].[Type] = 1 then [Transaction].[Amount] ELSE 0 end) as [Incomes],
Sum(case when [Transaction].[Type] = 2 then [Transaction].[Amount] ELSE 0 end) as [Expenses]
from 
[Transaction]

where [Transaction].[PaidOrReceivedAt] >= DATEADD(Month, -11, Cast(GETDATE() AS DATE))
AND [Transaction].[PaidOrReceivedAt] < DATEADD(Month, 1, Cast(GETDATE() AS DATE)) 

group by 
[Transaction].[UserId],
Month([Transaction].[PaidOrReceivedAt]),
Year([Transaction].[PaidOrReceivedAt])

