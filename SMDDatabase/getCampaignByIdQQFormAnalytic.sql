GO
/****** Object:  StoredProcedure [dbo].[getCampaignByIdQQFormAnalytic]    Script Date: 1/22/2017 9:32:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignByIdQQFormAnalytic] (
@Id INT,
@coice INT,
@gender INT,  -- 0 for all, 1 for male and 2 for female
@ageRange INT, -- 0 for All , 1 for 10-20 , 2 for 20-30, 3 for 30-40 , 4 for 40-50, 5 for 50-60, 6 for 60-70, 7 for 70-80, 8 for 80-90, 9 for 90+
@Profession nvarchar(250),	
@city nvarchar(250),
@type INT,
@QId INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @AgedateFrom DATE =  DATEADD(YYYY,-200,getdate()) ;
DECLARE @AgedateTo DATE = getdate();
 

IF @ageRange = 1
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-20,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-10,getdate()) ;
	END
IF @ageRange = 2
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-30,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-20,getdate()) ;
	END
	IF @ageRange = 3
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-40,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-30,getdate()) ;
	END
	IF @ageRange = 4
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-50,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-40,getdate()) ;
	END
	IF @ageRange = 5
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-60,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-50,getdate()) ;
	END
	IF @ageRange = 6
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-70,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-60,getdate()) ;
	END
	IF @ageRange = 7
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-80,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-70,getdate()) ;
	END
	IF @ageRange = 8
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-90,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-80,getdate()) ;
	END
	IF @ageRange = 9
	BEGIN
		set @AgedateFrom = DATEADD(YYYY,-200,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-90,getdate()) ;
	END
	
	IF @type = 1 
	BEGIN
		select count(*) as Stats  from AdCampaignResponse ac
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and (ac.UserSelection = @coice OR @coice = 0) and (usr.Gender = @gender OR @gender = 0)
		and (usr.Jobtitle = @Profession OR @Profession = 'All') and (ac.UserLocationCity = @city OR @city = 'All')
		and usr.DOB > = @AgedateFrom and usr.DOB < @AgedateTo
	END	
	IF @type = 2 
	BEGIN
		select count(*) as Stats from AdCampaignResponse ac 
		inner join AdCampaignTargetCriteria adc on adc.CampaignID = ac.CampaignID
		inner join AspNetUsers usr on ac.UserID = usr.Id
		inner join ProfileQuestionUserAnswer pqua on pqua.UserID = usr.Id and adc.PQID = pqua.PQID and pqua.PQAnswerID = adc.PQAnswerID
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and pqua.PQID = @QId and (usr.Gender = @gender OR @gender = 0)
		and (ac.UserLocationCity = @city OR @city = 'All')
		and usr.DOB > = @AgedateFrom  and usr.DOB < @AgedateTo		
	END	
	IF @type = 3 
	BEGIN
		select count(*) as Stats from AdCampaignResponse ac 
		inner join AdCampaignTargetCriteria adc on adc.CampaignID = ac.CampaignID
		inner join AspNetUsers usr on ac.UserID = usr.Id
		inner join SurveyQuestionResponse sqr on sqr.UserID = usr.Id and adc.SQID = sqr.SQID and sqr.UserSelection = adc.SQAnswer
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and sqr.SQID = @QId and (usr.Gender = @gender OR @gender = 0)
		and (ac.UserLocationCity = @city OR @city = 'All')
		and usr.DOB > = @AgedateFrom  and usr.DOB < @AgedateTo
		
	END			
	IF @type = 4 
	BEGIN
		select count(*) as Stats from AdCampaignResponse ac 
		inner join AdCampaignTargetCriteria adc on adc.CampaignID = ac.CampaignID
		inner join AspNetUsers usr on ac.UserID = usr.Id
		inner join AdCampaignResponse sqr on sqr.UserID = usr.Id and adc.QuizCampaignId = sqr.CampaignID and sqr.UserSelection = adc.QuizAnswerId
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and sqr.CampaignID = @QId and (usr.Gender = @gender OR @gender = 0)
		and (ac.UserLocationCity = @city OR @city = 'All')
		and usr.DOB > = @AgedateFrom  and usr.DOB < @AgedateTo
		
	END		
	
END
 
--EXEC [getCampaignByIdQQFormAnalytic] 10043, 0, 0, 0, 'All', 'All', 1, 6