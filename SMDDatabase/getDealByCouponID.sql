GO
/****** Object:  StoredProcedure [dbo].[getDealByCouponID]    Script Date: 10/31/2016 12:36:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getDealByCouponID] (
@CouponID INT,
@DateRange INT, -- 1 for last 30 days , 2 for All time
@Granularity INT		----- 1 for day, 2 for week, 3 for month and 4 for year
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @StartDate DATE = '20000101', @NumberOfYears INT = 30, @dateFrom DATE = getdate()-30, @tmp BIGINT =0;

--select 'hkkkkkkkkkkkkkkkkkkkkkkkkk' Granual , @tmp openStats, @tmp ConvrsionStats
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
	[day]        AS CONVERT(VARCHAR(11), dateadd(day, datediff(day, 0, [date]), 0), 6),
	[month]      AS DATEPART(MONTH,    [date]),
	FirstOfMonth AS CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, [date]), 0)),
	[MonthName]  AS right(convert(varchar, [date], 106), 8),
	[week]       AS CONVERT(VARCHAR(11), dateadd(week, datediff(week, 0, [date]), 0), 6),
	[ISOweek]    AS DATEPART(ISO_WEEK, [date]),
	[DayOfWeek]  AS DATEPART(WEEKDAY,  [date]),
	[quarter]    AS CONVERT(VARCHAR(11), dateadd(QUARTER, datediff(QUARTER, 0, [date]), 0), 6),
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
	DECLARE @stats TABLE (
	Granual  VARCHAR(200),
	ConvrsionStats bigint
	)

	DECLARE @open TABLE (
	Granual  VARCHAR(200),
	openStats bigint,
	ordr Date
	)
IF @DateRange = 2
	BEGIN
	 Select top 1 @dateFrom= ViewDateTime-4 from UserCouponView where CouponId = @CouponID
	END

	IF @Granularity = 1
	BEGIN
		insert into @T1 
		select d.day , d.date 
		from #dim d
		where d.date >= @dateFrom and d.date <= getdate()
	END
	ELSE IF @Granularity = 2
	BEGIN
		insert into @T1 
		select d.week , d.date
		from #dim d
		where d.date >= @dateFrom and d.date <= getdate()
	END
	ELSE IF @Granularity = 3
	BEGIN
		insert into @T1 
		select d.MonthName , d.date
		from #dim d
		where d.date >= @dateFrom and d.date <= getdate()
	END
	ELSE IF @Granularity = 4
	BEGIN
		insert into @T1 
		select d.quarter , d.date
		from #dim d
		where d.date >= @dateFrom and d.date <= getdate()
	END
	ELSE IF @Granularity = 5
	BEGIN
		insert into @T1 
		select d.year , d.date
		from #dim d
		where d.date >= @dateFrom and d.date <= getdate()
	END

	
	insert into @open
	Select  t.Granual, count(ucv.CouponId) OpenStats , min(t.date) ordr
	from UserCouponView ucv 
	right outer join   @T1 t on t.date = CONVERT(date, ucv.ViewDateTime) and ucv.CouponId = @CouponID
	group by t.Granual

		insert into @stats
		Select  t.Granual, count(case when upc.ResponseType = 2 then upc.CouponId else null end) Stats
		from UserPurchasedCoupon upc
		right outer join   @T1 t on t.date = CONVERT(date, upc.PurchaseDateTime) and upc.CouponId = @CouponID
		group by t.Granual
			
	select c.Granual, c.openStats, ln.ConvrsionStats
	from @open c
	inner join @stats ln on ln.Granual = c.Granual
	order by c.ordr
END


--EXEC [getDealByCouponID] 10078, 2, 1

