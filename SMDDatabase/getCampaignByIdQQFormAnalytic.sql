-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getCampaignByIdQQFormAnalytic] (
@Id INT,
@choice INT,
@gender INT,  -- 0 for all, 1 for male and 2 for female
@ageRange INT, -- 0 for All , 1 for 10-20 , 2 for 20-30, 3 for 30-40 , 4 for 40-50, 5 for 50-60, 6 for 60-70, 7 for 70-80, 8 for 80-90, 9 for 90+
@Profession nvarchar(250),	
@city nvarchar(250)
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @AgedateFrom DATE =  DATEADD(YYYY,-100,getdate()) ;
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
		set @AgedateFrom = DATEADD(YYYY,-500,getdate()) ;
		set @AgedateTo = DATEADD(YYYY,-90,getdate()) ;
	END
	select count(*) from AdCampaignResponse ac
	inner join AspNetUsers usr on ac.UserID = usr.Id
	where ac.CampaignID = @Id and ac.ResponseType = 3 
	and (ac.UserSelection = @choice OR @choice = 0) and (usr.Gender = @gender OR @gender = 0)
	and (usr.Jobtitle = @Profession OR @Profession = 'All') and (ac.UserLocationCity = @city OR @city = 'All')
	and usr.DOB > = @AgedateFrom and usr.DOB < @AgedateTo
		
		
		
	
END
