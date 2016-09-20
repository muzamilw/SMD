USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetRevenueOverTime]    Script Date: 9/19/2016 1:40:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[GetRevenueOverTime](

@CompanyId INT,                   --Input parameter ,  CompanyID of owner 466
@DateFrom DateTime, 
@DateTo	DateTime,
@Granularity INT			----- 1 for day, 2 for week, 3 for month and 4 for year
--@AccountBalance FLOAT out

)
AS
BEGIN

DECLARE @StartDate DATE = '20000101', @NumberOfYears INT = 30;

-- prevent set or regional settings from interfering with 
-- interpretation of dates / literals

SET DATEFIRST 7;
SET DATEFORMAT mdy;
SET LANGUAGE US_ENGLISH;

DECLARE @CutoffDate DATE = DATEADD(YEAR, @NumberOfYears, @StartDate);

-- this is just a holding table for intermediate calculations:

CREATE TABLE #dim
(
  [date]       DATE PRIMARY KEY, 
  [day]        AS DATEPART(DAY,      [date]),
  [month]      AS DATEPART(MONTH,    [date]),
  FirstOfMonth AS CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, [date]), 0)),
  [MonthName]  AS right(convert(varchar, [date], 106), 8),
  [week]       AS CONVERT(VARCHAR(6), dateadd(week, datediff(week, 0, [date]), 0), 6),
  [ISOweek]    AS DATEPART(ISO_WEEK, [date]),
  [DayOfWeek]  AS DATEPART(WEEKDAY,  [date]),
  [quarter]    AS DATEPART(QUARTER,  [date]),
  [year]       AS DATEPART(YEAR,     [date]),
  FirstOfYear  AS CONVERT(DATE, DATEADD(YEAR,  DATEDIFF(YEAR,  0, [date]), 0)),
  Style112     AS CONVERT(CHAR(8),   [date], 112),
  Style101     AS CONVERT(CHAR(10),  [date], 101)
)

-- use the catalog views to generate as many rows as we need

INSERT INTO #dim([date]) 

SELECT d
FROM
(
  SELECT d = DATEADD(DAY, rn - 1, @StartDate)
  FROM 
  (
		SELECT TOP (DATEDIFF(DAY, @StartDate, @CutoffDate)) 
		rn = ROW_NUMBER() OVER (ORDER BY s1.[object_id])
		FROM sys.all_objects AS s1
		CROSS JOIN sys.all_objects AS s2
		ORDER BY s1.[object_id]
  ) AS x
) AS y;





--select * from #dim;

IF @Granularity = 1
	BEGIN
		select   sum(t.CreditAmount) amountcollected, LEFT(CONVERT(VARCHAR, d.date, 120), 10)  granular
		  from [Transaction] t
		  inner join Account a on t.AccountID = a.AccountId
		  inner join Company co on a.CompanyId = co.CompanyId
		  inner join #dim d on d.date = CONVERT(date, t.TransactionDate)
		where a.CompanyId = @CompanyId and a.AccountType = 1 and t.TransactionDate >= @DateFrom and t.TransactionDate <= @DateTo
		group by d.date

	END
	
ELSE IF @Granularity = 2
	BEGIN
		select   sum(t.CreditAmount) amountcollected, d.week granular
	     from [Transaction] t
		 inner join Account a on t.AccountID = a.AccountId
		 inner join Company co on a.CompanyId = co.CompanyId
		 inner join #dim d on d.date = CONVERT(date, t.TransactionDate)
		where a.CompanyId = @CompanyId and a.AccountType = 1 and t.TransactionDate >= @DateFrom and t.TransactionDate <= @DateTo
		group by d.week
	END
ELSE IF @Granularity = 3
	BEGIN
		select   sum(t.CreditAmount) amountcollected, d.MonthName granular

		  from [Transaction] t
		  inner join Account a on t.AccountID = a.AccountId
		  inner join Company co on a.CompanyId = co.CompanyId
		  inner join #dim d on d.date = CONVERT(date, t.TransactionDate)
		where a.CompanyId = @CompanyId and a.AccountType = 1 and t.TransactionDate >= @DateFrom and t.TransactionDate <= @DateTo
		group by d.MonthName
	END
ELSE IF @Granularity = 4
	BEGIN
		select   sum(t.CreditAmount) amountcollected, convert(varchar(10),d.year,100)  granular

		  from [Transaction] t
		  inner join Account a on t.AccountID = a.AccountId
		  inner join Company co on a.CompanyId = co.CompanyId
		  inner join #dim d on d.date = CONVERT(date, t.TransactionDate)
		where a.CompanyId = @CompanyId and a.AccountType = 1 and t.TransactionDate >= @DateFrom and t.TransactionDate <= @DateTo
		group by d.year
	END
		
	END


--@Granularity INT,			----- 1 for day, 2 for week, 3 for month and 4 for year

--EXEC GetRevenueOverTime 466, '2016-8-01', '2016-9-17', 1