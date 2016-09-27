USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getApprovedCampaignsOverTime]    Script Date: 9/26/2016 10:17:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getApprovedCampaignsOverTime] (
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


	--select count(ac.CampaignID) GameStats, d.date granular
	--    from AdCampaign ac
	-- inner join #dim d on d.date = CONVERT(date, ac.ApprovalDateTime)
	-- where ac.Type = 4 and ac.ApprovalDateTime >= @DateFrom and ac.ApprovalDateTime <= @DateTo
	--group by d.date
	DECLARE @T1 TABLE (
	Granual  VARCHAR(200),
	date Date
	)
	DECLARE @cpn TABLE (
	Granual  VARCHAR(200),
	dealStats bigint
	)
	DECLARE @sq TABLE (
	Granual  VARCHAR(200),
	sQStats bigint
	)
	DECLARE @pq TABLE (
	Granual  VARCHAR(200),
	pQStats bigint
	)
	DECLARE @campng TABLE (
	Granual  VARCHAR(200),
	GameStats bigint,
	videoStats bigint,
	ordr Date
	)
	--insert into @T1 
	--select d.date 
	--from #dim d
	--where d.date >= @DateFrom and d.date <= @DateTo
		


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


	insert into @campng
	select t.Granual, count(case when ac.Type = 4 then ac.CampaignID else null end) GameStats, count(case when ac.Type = 1 then ac.CampaignID else null end) as videoStats , min(t.date) ordr
	from AdCampaign ac
	right outer join   @T1 t on t.date = CONVERT(date, ac.ApprovalDateTime)
	group by t.Granual
	
		
	insert into @cpn
	select t.Granual , count(cp.CouponId) as dealStats
	from @T1 t
	left join Coupon cp on t.date = CONVERT(date, cp.ApprovalDateTime)
	group by t.Granual
	
				 
	insert into @sq
	select t.Granual , count(sq.SQID) as sQStats
	from @T1 t
	left join SurveyQuestion sq on t.date = CONVERT(date, sq.ApprovalDate)
	group by t.Granual
	
				 
	insert into @pq
	select t.Granual , count(pq.PQID) as pQStats
	from @T1 t
	left join ProfileQuestion pq on t.date = CONVERT(date, pq.ApprovalDate)
	group by t.Granual
	
		
	select c.Granual, c.GameStats, c.videoStats, cn.dealStats, p.pQStats, s.sQStats
	from @campng c
	inner join @cpn cn on cn.Granual = c.Granual
	inner join @pq p on p.Granual = c.Granual
	inner join @sq s on s.Granual = c.Granual
	order by c.ordr
END


--EXEC [getApprovedCampaignsOverTime] '2016-8-1', '2016-9-26', 2

