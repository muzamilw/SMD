GO
/****** Object:  StoredProcedure [dbo].[GetLiveCampaignCountOverTime]    Script Date: 10/24/2016 8:09:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetLiveCampaignCountOverTime](

@CampaignType INT,				--Input parameter ,  CampaignType 1 for Ads, 2, games, 3 for survay, 4 for polls, 5 free deals , 6 paid deals
@DateFrom DateTime, 
@DateTo	DateTime,
@Granularity INT			----- 1 for day, 2 for week, 3 for month and 4 for year


)
AS
BEGIN
--select 'nnnnnnnnnnnnnnnnnnnnnnnn' Granual , 0 campCount
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
  [day]        AS CONVERT(VARCHAR(6), dateadd(day, datediff(day, 0, [date]), 0), 6),
  [month]      AS DATEPART(MONTH,    [date]),
  FirstOfMonth AS CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, [date]), 0)),
  [MonthName]  AS right(convert(varchar, [date], 106), 8),
  [week]       AS CONVERT(VARCHAR(6), dateadd(week, datediff(week, 0, [date]), 0), 6),
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
	DECLARE @gran TABLE (
	Granual  VARCHAR(200),
	ordr Date
	)
	
	DECLARE @rev TABLE (
	Granual  VARCHAR(200),
	campCount bigint
	)


	IF @Granularity = 1
	BEGIN
		insert into @T1 
		select d.day , d.date 
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
		select d.quarter , d.date
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END
	ELSE IF @Granularity = 5
	BEGIN
		insert into @T1 
		select d.year , d.date
		from #dim d
		where d.date >= @DateFrom and d.date <= @DateTo
	END

	insert into @gran
	Select  t.Granual, min(t.date) ordr
	from @T1 t 
	group by t.Granual
	
	 IF @CampaignType = 1
		BEGIN -- for video ads
				
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.CampaignID else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.CampaignID, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join AdCampaign ac on ac.CampaignID = c.CampaignID 
				inner join CampaignEventHistory ceh on c.EventId = ceh.EventId
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.CampaignID is not null and ac.Type = 1 
				group by c.CampaignID ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran
		END
		ELSE  IF @CampaignType = 2
		BEGIN -- for Display ads
				
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.CampaignID else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.CampaignID, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join AdCampaign ac on ac.CampaignID = c.CampaignID 
				inner join CampaignEventHistory ceh on c.EventId = ceh.EventId
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.CampaignID is not null and ac.Type = 4 
				group by c.CampaignID ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran
		END 
		ElSE  IF @CampaignType = 3
		BEGIN -- Survay
				
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.PQID else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.PQID, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.PQID is not null  
				group by c.PQID ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran
		END
		ElSE  IF @CampaignType = 4
		BEGIN -- Polls
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.SQID else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.SQID, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.SQID is not null  
				group by c.SQID ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran
		END
		ELSE  IF @CampaignType = 5
		BEGIN -- for Free Deals
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.CouponId else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.CouponId, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join Coupon ac on ac.CouponId = c.CouponId 
				inner join CampaignEventHistory ceh on c.EventId = ceh.EventId
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.CouponId is not null and ac.CouponListingMode = 1
				group by c.CouponId ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran

		END 
		ELSE  IF @CampaignType = 6
		BEGIN -- for Paid Deals
				
			insert into @rev
			select rst.gran Granual , count(case when camp.EventStatusId = 3 then camp.CouponId else null end) stats from CampaignEventHistory camp 
			inner join (
			select Max(c.EventDateTime) maxdate, c.CouponId, Max(c.EventId) Id , t.Granual gran  from
				CampaignEventHistory c
				inner join Coupon ac on ac.CouponId = c.CouponId 
				inner join CampaignEventHistory ceh on c.EventId = ceh.EventId
				inner join @T1 t on c.EventDateTime < dateadd(d,1,t.date)
				where c.CouponId is not null and ac.CouponListingMode = 1
				group by c.CouponId ,  t.Granual 
				) rst on rst.maxdate = camp.EventDateTime and rst.Id = camp.EventId
				group by rst.gran
		END 
	
	select g.Granual, (case when r.campCount is not null then r.campCount else 0 end) campCount from @gran g
	left outer join @rev r on r.Granual = g.Granual
	order by g.ordr
	END
	

	--EXEC [GetLiveCampaignCountOverTime] 1, '2016-9-01', '2016-10-1', 1 