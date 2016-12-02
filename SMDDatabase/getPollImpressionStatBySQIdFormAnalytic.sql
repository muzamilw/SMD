GO
/****** Object:  StoredProcedure [dbo].[getCampaignByIdQQFormAnalytic]    Script Date: 12/1/2016 12:29:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getPollImpressionStatBySQIdFormAnalytic] (
@Id INT,
@gender INT,  -- 0 for all, 1 for male and 2 for female
@ageRange INT -- 0 for All , 1 for 10-20 , 2 for 20-30, 3 for 30-40 , 4 for 40-50, 5 for 50-60, 6 for 60-70, 7 for 70-80, 8 for 80-90, 9 for 90+
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
	
	BEGIN
		select count(*) as Stats  from SurveyQuestionResponse sqr
		inner join AspNetUsers usr on sqr.UserID = usr.Id
		where sqr.SQID = @Id and sqr.ResponseType = 1 
		and (usr.Gender = @gender OR @gender = 0)
		and usr.DOB > = @AgedateFrom and usr.DOB < @AgedateTo
	END	
			
	
END
 
--EXEC [getPollImpressionStatBySQIdFormAnalytic] 17, 2, 8

