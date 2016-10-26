GO
/****** Object:  StoredProcedure [dbo].[getSurvayByPQID]    Script Date: 10/24/2016 9:46:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getSurvayByPQID] (
@PQId INT,
--@status INT, -- 1 for answered, 2 for Skipped
@DateRange INT, -- 1 for last 30 days , 2 for All time
@Granularity INT		----- 1 for day, 2 for week, 3 for month and 4 for year
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @StartDate DATE = '20000101', @NumberOfYears INT = 30, @dateFrom DATE = getdate()-30, @tmp BIGINT =0;

--select 'hkkkkkkkkkkkkkkkkkkkkkkkkk' Granual , @tmp openStats, @tmp Stats
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
	Stats bigint,
	SkipStats bigint
	)

	DECLARE @open TABLE (
	Granual  VARCHAR(200),
	openStats bigint,
	ordr Date
	)
IF @DateRange = 2
	BEGIN
	 Select top 1 @dateFrom= pqu.AnswerDateTime from ProfileQuestionUserAnswer pqu where pqu.PQID = @PQId
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
	Select  t.Granual, count(case when pqu.ResponseType = 1 then pqu.PQUAnswerID else null end) OpenStats , min(t.date) ordr
	from ProfileQuestionUserAnswer pqu
	right outer join   @T1 t on t.date = CONVERT(date, pqu.AnswerDateTime) and pqu.PQID = @PQId
	group by t.Granual

		insert into @stats
		Select  t.Granual, count(case when pqu.ResponseType = 3 then pqu.PQUAnswerID else null end) AnsStats, count(case when pqu.ResponseType = 4 then pqu.PQUAnswerID else null end) SkipStats
		from ProfileQuestionUserAnswer pqu
		right outer join   @T1 t on t.date = CONVERT(date, pqu.AnswerDateTime) and pqu.PQID = @PQId
		group by t.Granual
	
		
	select c.Granual, c.openStats, st.Stats, st.SkipStats
	from @open c
	inner join @stats st on st.Granual = c.Granual
	order by c.ordr
END


--EXEC [getSurvayByPQID] 3, 2, 2
--select * from ProfileQuestionUserAnswer where PQID = 3 and ResponseType = 4
