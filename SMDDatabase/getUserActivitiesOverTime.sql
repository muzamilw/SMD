
GO
/****** Object:  StoredProcedure [dbo].[getApprovedCampaignsOverTime]    Script Date: 10/3/2016 7:30:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[getUserActivitiesOverTime] (
@DateFrom DateTime, 
@DateTo	DateTime,
@Granularity INT		----- 1 for day, 2 for week, 3 for month and 4 for year
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @StartDate DATE = '20000101', @NumberOfYears INT = 30;
select 'vvvvvvvvvvvvvv' Granual, 0 newStats, 0 loginStats,  0 deleteStats
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


	
	DECLARE @T1 TABLE (
	Granual  VARCHAR(200),
	date Date
	)
	DECLARE @login TABLE (
	Granual  VARCHAR(200),
	loginStats bigint
	)
	DECLARE @del TABLE (
	Granual  VARCHAR(200),
	deleteStats bigint
	)
	
	DECLARE @new TABLE (
	Granual  VARCHAR(200),
	newStats bigint,
	ordr Date
	)
		


	IF @Granularity = 1
	BEGIN
		insert into @T1 
		select d.date , d.date 
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END
	ELSE IF @Granularity = 2
	BEGIN
		insert into @T1 
		select d.week , d.date
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END
	ELSE IF @Granularity = 3
	BEGIN
		insert into @T1 
		select d.MonthName , d.date
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END
	ELSE IF @Granularity = 4
	BEGIN
		insert into @T1 
		select d.year , d.date
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END

	
	insert into @new
	Select  t.Granual, count(us.id) newStats , min(t.date) ordr
	from AspNetUsers us 
	right outer join   @T1 t on t.date = CONVERT(date, us.CreatedDateTime)
	group by t.Granual

			
	insert into @login
	select t.Granual , count(us.id) as loginStats
	from @T1 t
	left join AspNetUsers us on t.date = CONVERT(date, us.LastLoginTime)
	group by t.Granual
	
				 
	insert into @del
	select t.Granual , count(c.CompanyId) as deleteStats
	from @T1 t
	left join Company c on t.date = CONVERT(date, c.DeleteDate)
	group by t.Granual
	
				 
	
	
		
	select c.Granual, c.newStats, ln.loginStats, d.deleteStats
	from @new c
	inner join @login ln on ln.Granual = c.Granual
	inner join @del d on d.Granual = c.Granual
	order by c.ordr
END


--EXEC [getUserActivitiesOverTime] '2016-8-1', '2016-9-26', 1

