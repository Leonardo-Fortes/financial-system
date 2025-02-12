Create or Alter VIEW [vwGetExpensesByCategory] as
select 
[Transaction].[UserId],
[Category].[Title] as [Category],
Year([Transaction].[PaidOrReceivedAt]) as [Year],
Sum([Transaction].[Amount]) as [Expenses]
from 
[Transaction]
inner join [Category] on [Transaction].[CategoryId] = [Category].[Id]
where [Transaction].[PaidOrReceivedAt] >= DATEADD(Month, -11, Cast(GETDATE() AS DATE))
AND [Transaction].[PaidOrReceivedAt] < DATEADD(Month, 1, Cast(GETDATE() AS DATE))
and [Transaction].[Type] = 2
group by 
[Transaction].[UserId],
[Category].[Title],
Year([Transaction].[PaidOrReceivedAt])
