

/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 9/6/2016 5:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetProducts] 

--  select weightage,* from [GetUserProfileQuestions]('b8a3884f-73f3-41ec-9926-293ea919a5e1', 214) x order by x.weightage

-- exec [dbo].[GetProducts] 		@UserID = N'b8a3884f-73f3-41ec-9926-293ea919a5e1', 		@FromRow = 0, 		@ToRow = 100
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN
DECLARE @dob AS DateTime
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT
DECLARE @currentDate AS DateTime
DECLARE @companyId AS INT
DECLARE @cash4adsSocialHandle AS nvarchar(128)
DECLARE @cash4adsSocialHandleType AS nvarchar(128)

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		     SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
		   SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)


select @cash4adsSocialHandle = case when c.TwitterHandle is not null or c.TwitterHandle <> '' then c.TwitterHandle 
	when c.FacebookHandle is not null or c.FacebookHandle <> '' then c.FacebookHandle 
	when c.InstagramHandle is not null or c.InstagramHandle <> '' then c.InstagramHandle 
	when c.PinterestHandle is not null or c.PinterestHandle <> '' then c.PinterestHandle 
	else ''

	end ,
	@cash4adsSocialHandleType = case when c.TwitterHandle is not null or c.TwitterHandle <> '' then 't'
	when c.FacebookHandle is not null or c.FacebookHandle <> '' then 'f'
	when c.InstagramHandle is not null or c.InstagramHandle <> '' then 'i'
	when c.PinterestHandle is not null or c.PinterestHandle <> '' then 'p'
	else ''

	end from company c inner join AspNetUsers u on u.CompanyId = c.CompanyId and u.email = 'cash4ads@cash4ads.com'



select *, COUNT(*) OVER() AS TotalItems--, 
from
(	

	select top(1) WITH TIES ads.*, g.GameUrl,COUNT(*) OVER( partition by type) AS AdCount from
	(
			select adcampaign.campaignid as ItemId, adcampaign.DisplayTitle ItemName, 'Ad' Type, 
			adcampaign.Description +'\n' + adcampaign.CampaignDescription as description,
			Case 
				when adcampaign.Type = 3  -- Flyer
				THEN
				  2
				when adcampaign.Type = 4
				THEN
					3
				when adcampaign.Type = 1
				THEN
					1
				END as
			  ItemType, 
			((adcampaign.ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
			adcampaign.ImagePath as AdImagePath, 
			Case 
				when adcampaign.Type = 3  -- Flyer
				THEN
				  adcampaign.LandingPageVideoLink--'http://manage.cash4ads.com/' +   LandingPageVideoLink -- 'http://manage.cash4ads.com/Ads/Ads/Content/' + CONVERT(NVARCHAR, CampaignID)
				when adcampaign.Type != 3
				THEN
					adcampaign.LandingPageVideoLink
			END as AdVideoLink,
			adcampaign.Answer1 as AdAnswer1, adcampaign.Answer2 as AdAnswer2, adcampaign.Answer3 as AdAnswer3, adcampaign.CorrectAnswer as AdCorrectAnswer, adcampaign.VerifyQuestion as AdVerifyQuestion, 
			adcampaign.RewardType as AdRewardType,
			adcampaign.Voucher1Heading as AdVoucher1Heading, adcampaign.Voucher1Description as AdVoucher1Description,
			adcampaign.Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	
			NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqA1LinkedQ1, NULL as PqA1LinkedQ2, 
			NULL as	PQA1LinkedQ3,NULL as PQA1LinkedQ4,NULL as PQA1LinkedQ5,NULL as PQA1LinkedQ6,
			NULL as PqA1Type, NULL as PqA1SortOrder, NULL as PqA1ImagePath,
			NULL as PqAnswer2Id, NULL as PqAnswer2, NULL as PqA2LinkedQ1, NULL as PqA2LinkedQ2,
			NULL as PQA2LinkedQ3,NULL as  PQA2LinkedQ4,NULL as  PQA2LinkedQ5,NULL as PQA2LinkedQ6,
			NULL as PqA2Type, NULL as PqA2SortOrder, NULL as PqA2ImagePath,
			NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqA3LinkedQ1, NULL as PqA3LinkedQ2,
			NULL as PQA3LinkedQ3,NULL as  PQA3LinkedQ4,NULL as  PQA3LinkedQ5,NULL as PQA3LinkedQ6,
			NULL as PqA3Type, NULL as PqA3SortOrder, NULL as PqA3ImagePath, 
			NULL as PqAnswer4Id, NULL as PqAnswer4, NULL as PqA4LinkedQ1, NULL as PqA4LinkedQ2,
			NULL as PQA4LinkedQ3,NULL as  PQA4LinkedQ4,NULL as  PQA4LinkedQ5,NULL as PQA4LinkedQ6,
			NULL as PqA4Type, NULL as PqA4SortOrder, NULL as PqA4ImagePath,
			NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqA5LinkedQ1, NULL as PqA5LinkedQ2,
			NULL as PQA5LinkedQ3,NULL as  PQA5LinkedQ4,NULL as  PQA5LinkedQ5,NULL as PQA5LinkedQ6,
			NULL as PqA5Type, NULL as PqA5SortOrder, NULL as PqA5ImagePath,
			NULL as PqAnswer6Id, NULL as PqAnswer6, NULL as PqA6LinkedQ1, NULL as PqA6LinkedQ2,
			NULL as PQA6LinkedQ3,NULL as  PQA6LinkedQ4,NULL as  PQA6LinkedQ5,NULL as PQA6LinkedQ6,
			NULL as PqA6Type, NULL as PqA6SortOrder, NULL as PqA6ImagePath,

			--((row_number() over (order by ABS(CHECKSUM(NewId())) % 100 desc) * 100) + 1) Weightage,
			((row_number() over (order by isNUll(Priority,0) desc) * 100) + 1) Weightage,
			NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
			null as SocialHandle, null as SocialHandleType,
			(select 
			CASE
				WHEN adcampaign.LogoUrl is null or adcampaign.LogoUrl = ''
				THEN 'http://manage.cash4ads.com/' + usr.ProfileImage
				WHEN usr.ProfileImage is not null
				THEN 'http://manage.cash4ads.com/' + adcampaign.LogoUrl
			END as AdvertisersLogoPath from AspNetUsers usr where usr.Id = adcampaign.UserID) as AdvertisersLogoPath,

			

			adcampaign.VideoUrl as LandingPageUrl,
			adcampaign.BuuyItLine1 as BuyItLine1,
			adcampaign.BuyItLine2 as BuyItLine2,
			 adcampaign.BuyItLine3 as BuyItLine3,
			adcampaign.BuyItButtonLabel as BuyItButtonText, 
			 'http://manage.cash4ads.com/' +adcampaign.BuyItImageUrl as BuyItImageUrl,
			'http://manage.cash4ads.com/' + adcampaign.Voucher1ImagePath as VoucherImagePath,
			(select count(*) from coupon v where AdCampaign.CompanyId = v.CompanyId and v.Status = 3) as VoucherCount,
			AdCampaign.CompanyId,VideoLink2,IsShowVoucherSetting,
			
			Case 
				when adcampaign.Type = 4   -- Game
				THEN
					(select top 1 GameId from Game  where Game.Status = 1 ORDER BY NEWID() )
				when adcampaign.Type != 4
				THEN
					NULL
			END as GameId,
			null as FreeCouponID
			 
			from adcampaign
			--- voucher join for the same company.
	
			where (
				((@age is null) or (adcampaign.AgeRangeEnd >= @age and  @age >= adcampaign.AgeRangeStart))
				and
				((@gender is null) or (adcampaign.Gender = @gender))
				and
				((@languageId is null) or (adcampaign.LanguageId = @languageId))
				and
				(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
				and
				(adcampaign.Approved = 1)
				and
				(adcampaign.Type  <> 5)		-- do not load coupons
				and
				(adcampaign.Status = 3) -- live
				and
				((adcampaign.AmountSpent is null) or (adcampaign.MaxBudget > adcampaign.AmountSpent))
				and
				((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
					 where MyCampaignLoc.CampaignID=adcampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
					 MyCampaignLoc.CityID=@cityId) > 0))
				and
				((@languageId is null or @industryId is null) or ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
					 where MyCampaignCrit.CampaignID = adcampaign.CampaignID and 
					 MyCampaignCrit.LanguageID=@languageId and MyCampaignCrit.IndustryID=@industryId) > 0 ))
				and
				(((select count(*) from AdCampaignResponse adResponse where  
					adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID and datepart(day,getdate()) = datepart(day,adResponse.CreatedDateTime) and datepart(month,getdate()) = datepart(month,adResponse.CreatedDateTime) ) = 0)
				 or
				 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
					where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
					is null)
				) 
			)  
			union

			-------- fall back freee games to be offered if no paid ad campaigns are available and only max 2 possible in one day.
		select freeGame.* from (select  -2 as ItemId, 'Free Coupon Offer Game' ItemName, 'freeGame' Type, 
			'' as description,
			9 as ItemType, 
			0 as AdClickRate,  -- Amount AdViewer will get
			'' as AdImagePath, 
			'' as AdVideoLink,
			'' as AdAnswer1, '' as AdAnswer2, '' as AdAnswer3, '' as AdCorrectAnswer, '' as AdVerifyQuestion, 
			0 as AdRewardType,
			'' as AdVoucher1Heading, '' as AdVoucher1Description,
			'' as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	
			NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqA1LinkedQ1, NULL as PqA1LinkedQ2, 
			NULL as	PQA1LinkedQ3,NULL as PQA1LinkedQ4,NULL as PQA1LinkedQ5,NULL as PQA1LinkedQ6,
			NULL as PqA1Type, NULL as PqA1SortOrder, NULL as PqA1ImagePath,
			NULL as PqAnswer2Id, NULL as PqAnswer2, NULL as PqA2LinkedQ1, NULL as PqA2LinkedQ2,
			NULL as PQA2LinkedQ3,NULL as  PQA2LinkedQ4,NULL as  PQA2LinkedQ5,NULL as PQA2LinkedQ6,
			NULL as PqA2Type, NULL as PqA2SortOrder, NULL as PqA2ImagePath,
			NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqA3LinkedQ1, NULL as PqA3LinkedQ2,
			NULL as PQA3LinkedQ3,NULL as  PQA3LinkedQ4,NULL as  PQA3LinkedQ5,NULL as PQA3LinkedQ6,
			NULL as PqA3Type, NULL as PqA3SortOrder, NULL as PqA3ImagePath, 
			NULL as PqAnswer4Id, NULL as PqAnswer4, NULL as PqA4LinkedQ1, NULL as PqA4LinkedQ2,
			NULL as PQA4LinkedQ3,NULL as  PQA4LinkedQ4,NULL as  PQA4LinkedQ5,NULL as PQA4LinkedQ6,
			NULL as PqA4Type, NULL as PqA4SortOrder, NULL as PqA4ImagePath,
			NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqA5LinkedQ1, NULL as PqA5LinkedQ2,
			NULL as PQA5LinkedQ3,NULL as  PQA5LinkedQ4,NULL as  PQA5LinkedQ5,NULL as PQA5LinkedQ6,
			NULL as PqA5Type, NULL as PqA5SortOrder, NULL as PqA5ImagePath,
			NULL as PqAnswer6Id, NULL as PqAnswer6, NULL as PqA6LinkedQ1, NULL as PqA6LinkedQ2,
			NULL as PQA6LinkedQ3,NULL as  PQA6LinkedQ4,NULL as  PQA6LinkedQ5,NULL as PQA6LinkedQ6,
			NULL as PqA6Type, NULL as PqA6SortOrder, NULL as PqA6ImagePath,

			
			0 as  Weightage,
			NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
			null as SocialHandle, null as SocialHandleType,
			'' as AdvertisersLogoPath,
			'' as LandingPageUrl,
			'' as BuyItLine1,
			'' as BuyItLine2,
			 '' as BuyItLine3,
			'' as BuyItButtonText, 
			 '' as BuyItImageUrl,
			'' as VoucherImagePath,
			0 as VoucherCount,
			0 as CompanyId,'' as VideoLink2,cast(1 as bit) as IsShowVoucherSetting,
			
			(select top 1 GameId from Game  where Game.Status = 1 ORDER BY NEWID() ) as GameId,  
			(select top 1 CouponId from coupon c 
			
			--coupon where clause
			where c.Status = 3
			and c.CouponExpirydate > GETDATE()
			and 
				(--unlimited listing  -- valid for 3 months
				( CouponListingMode = 2 and @currentDate between DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1) and dateadd(month,2,DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1)))))
					and  c.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = c.couponid and upc.userId = @UserId) 
				)
					--
				or
				--free more - valid for 1 month now.
				(
					CouponListingMode = 1 and @currentDate  between DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1) and DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,30))
					and c.CouponIssuedCount < 10
				)
				or 
				--nationwide mode valid for 3 months but higher priority
				(
				CouponListingMode = 3 and @currentDate between DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1) and dateadd(month,2,DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1)))))
					and  c.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = c.couponid and upc.userId = @UserId) 
				)
				

			--coupon end clause
			
			
			
			
			 order by NEWID())  as FreeCouponID
			) as freeGame
			inner join AdCampaignResponse  acr on freeGame.ItemId = acr.CampaignID and acr.ResponseType = 4 and acr.UserID = 'b8a3884f-73f3-41ec-9926-293ea919a5e1' and acr.CreatedDateTime between DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) and dateadd(HOUR,24,GETDATE())
			group by ItemId, ItemName, Type, 
			description,
			ItemType, 
			AdClickRate,  -- Amount AdViewer will get
			AdImagePath, 
			AdVideoLink,
			AdAnswer1, AdAnswer2, AdAnswer3, AdCorrectAnswer, AdVerifyQuestion, 
			AdRewardType,
			AdVoucher1Heading, AdVoucher1Description,
			AdVoucher1Value, SqLeftImagePath, SqRightImagePath,
	
			PqAnswer1Id, PqAnswer1, PqA1LinkedQ1, PqA1LinkedQ2, 
			PQA1LinkedQ3,PQA1LinkedQ4,PQA1LinkedQ5,PQA1LinkedQ6,
			PqA1Type, PqA1SortOrder, PqA1ImagePath,
			PqAnswer2Id, PqAnswer2, PqA2LinkedQ1, PqA2LinkedQ2,
			PQA2LinkedQ3, PQA2LinkedQ4, PQA2LinkedQ5,PQA2LinkedQ6,
			PqA2Type, PqA2SortOrder, PqA2ImagePath,
			PqAnswer3Id, PqAnswer3, PqA3LinkedQ1, PqA3LinkedQ2,
			PQA3LinkedQ3, PQA3LinkedQ4, PQA3LinkedQ5,PQA3LinkedQ6,
			PqA3Type, PqA3SortOrder, PqA3ImagePath, 
			PqAnswer4Id, PqAnswer4, PqA4LinkedQ1, PqA4LinkedQ2,
			PQA4LinkedQ3, PQA4LinkedQ4, PQA4LinkedQ5,PQA4LinkedQ6,
			PqA4Type, PqA4SortOrder, PqA4ImagePath,
			PqAnswer5Id, PqAnswer5, PqA5LinkedQ1, PqA5LinkedQ2,
			PQA5LinkedQ3, PQA5LinkedQ4, PQA5LinkedQ5,PQA5LinkedQ6,
			PqA5Type, PqA5SortOrder, PqA5ImagePath,
			PqAnswer6Id, PqAnswer6, PqA6LinkedQ1, PqA6LinkedQ2,
			PQA6LinkedQ3, PQA6LinkedQ4, PQA6LinkedQ5,PQA6LinkedQ6,
			PqA6Type, PqA6SortOrder, PqA6ImagePath,

			
			 Weightage,
			SqLeftImagePercentage, SqRightImagePercentage,
			SocialHandle, SocialHandleType,
			AdvertisersLogoPath,
			LandingPageUrl,
			BuyItLine1,
			BuyItLine2,
			 BuyItLine3,
			BuyItButtonText, 
			 BuyItImageUrl,
			VoucherImagePath,
			VoucherCount,
			freegame.CompanyId,VideoLink2,IsShowVoucherSetting, freegame.GameId, acr.CampaignID,acr.UserID, freeGame.FreeCouponID
			having COUNT(acr.CampaignID)  < 3
			
			-------- fall back if all 3 free games are also played then show no ads card. .
			union

			select  -1 as ItemId, 'No Ads card' ItemName, 'noAds' Type, 
			'' as description,
			9 as ItemType, 
			0 as AdClickRate,  -- Amount AdViewer will get
			'' as AdImagePath, 
			'' as AdVideoLink,
			'' as AdAnswer1, '' as AdAnswer2, '' as AdAnswer3, '' as AdCorrectAnswer, '' as AdVerifyQuestion, 
			0 as AdRewardType,
			'' as AdVoucher1Heading, '' as AdVoucher1Description,
			'' as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	
			NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqA1LinkedQ1, NULL as PqA1LinkedQ2, 
			NULL as	PQA1LinkedQ3,NULL as PQA1LinkedQ4,NULL as PQA1LinkedQ5,NULL as PQA1LinkedQ6,
			NULL as PqA1Type, NULL as PqA1SortOrder, NULL as PqA1ImagePath,
			NULL as PqAnswer2Id, NULL as PqAnswer2, NULL as PqA2LinkedQ1, NULL as PqA2LinkedQ2,
			NULL as PQA2LinkedQ3,NULL as  PQA2LinkedQ4,NULL as  PQA2LinkedQ5,NULL as PQA2LinkedQ6,
			NULL as PqA2Type, NULL as PqA2SortOrder, NULL as PqA2ImagePath,
			NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqA3LinkedQ1, NULL as PqA3LinkedQ2,
			NULL as PQA3LinkedQ3,NULL as  PQA3LinkedQ4,NULL as  PQA3LinkedQ5,NULL as PQA3LinkedQ6,
			NULL as PqA3Type, NULL as PqA3SortOrder, NULL as PqA3ImagePath, 
			NULL as PqAnswer4Id, NULL as PqAnswer4, NULL as PqA4LinkedQ1, NULL as PqA4LinkedQ2,
			NULL as PQA4LinkedQ3,NULL as  PQA4LinkedQ4,NULL as  PQA4LinkedQ5,NULL as PQA4LinkedQ6,
			NULL as PqA4Type, NULL as PqA4SortOrder, NULL as PqA4ImagePath,
			NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqA5LinkedQ1, NULL as PqA5LinkedQ2,
			NULL as PQA5LinkedQ3,NULL as  PQA5LinkedQ4,NULL as  PQA5LinkedQ5,NULL as PQA5LinkedQ6,
			NULL as PqA5Type, NULL as PqA5SortOrder, NULL as PqA5ImagePath,
			NULL as PqAnswer6Id, NULL as PqAnswer6, NULL as PqA6LinkedQ1, NULL as PqA6LinkedQ2,
			NULL as PQA6LinkedQ3,NULL as  PQA6LinkedQ4,NULL as  PQA6LinkedQ5,NULL as PQA6LinkedQ6,
			NULL as PqA6Type, NULL as PqA6SortOrder, NULL as PqA6ImagePath,

			
			0 as  Weightage,
			NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
			null as SocialHandle, null as SocialHandleType,
			'' as AdvertisersLogoPath,
			'' as LandingPageUrl,
			'' as BuyItLine1,
			'' as BuyItLine2,
			 '' as BuyItLine3,
			'' as BuyItButtonText, 
			 '' as BuyItImageUrl,
			'' as VoucherImagePath,
			0 as VoucherCount,
			0 as CompanyId,'' as VideoLink2,cast(1 as bit) as IsShowVoucherSetting,
			
			0 as GameId,
			null as FreeCouponID


			

		) ads
		
		left outer join Game g on ads.GameId = g.gameId
		order by type
		
			
			--response type 1 = normal video watched
			-- response type 2 = skipped
			 -- response type 3 = buy it
			 --type 4 = free game won
			 
			
			
	
	

	--------------------- survey questions
	union
	select *, NULL as AdvertisersLogoPath,
	null as LandingPageUrl,
	null as BuyItLine1,
	null as BuyItLine2,
	 null as BuyItLine3,
	null as BuyItButtonText, 
	null as BuyItImageUrl,
	null as VoucherImagePath,
	null as VoucherCount,
	null as CompanyId,
	null as VideoLink2,
	cast(1 as bit) as IsShowVoucherSetting,
	
	null as GameId,
	null as FreeCouponID,
	null as GameUrl,
	null as AdCount
	  from [GetUserSurveys](@UserID,@cash4adsSocialHandle,@cash4adsSocialHandleType)
	  
	  
	  
	  -------------------profile questions
	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL,
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1LinkedQ3, PQA1LinkedQ4, PQA1LinkedQ5,PQA1LinkedQ6, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2LinkedQ3, PQA2LinkedQ4, PQA2LinkedQ5,PQA2LinkedQ6, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3LinkedQ3, PQA3LinkedQ4, PQA3LinkedQ5,PQA3LinkedQ6, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4LinkedQ3, PQA4LinkedQ4, PQA4LinkedQ5,PQA4LinkedQ6, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5LinkedQ3, PQA5LinkedQ4, PQA5LinkedQ5,PQA5LinkedQ6, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6LinkedQ3, PQA6LinkedQ4, PQA6LinkedQ5,PQA6LinkedQ6, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	SocialHandle,SocialHandleType,
	NULL as AdvertisersLogoPath ,
	null as LandingPageUrl,
	null as BuyItLine1,
	null as BuyItLine2,
	 null as BuyItLine3,
	null as BuyItButtonText, 
	null as BuyItImageUrl,
	null as VoucherImagePath,
	null as VoucherCount,
	null as CompanyId,
	null as VideoLink2,
	cast(1 as bit) as IsShowVoucherSetting,
	
	null as GameId,
	null as FreeCouponID,
	null as GameUrl,
	null as AdCount
	from [GetUserProfileQuestions](@UserID, @countryId,@cash4adsSocialHandle,@cash4adsSocialHandleType)
	--pqz where pqz.pqid  NOT in  ( select  LinkedQuestion1ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion1ID IS NOT NULL) and 
   -- pqz.pqid  NOT in  ( select  LinkedQuestion2ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion2ID IS NOT NULL) and 
  --  pqz.pqid  NOT in  ( select  LinkedQuestion3ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion3ID IS NOT NULL) and 
  --  pqz.pqid  NOT in  ( select  LinkedQuestion4ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion4ID IS NOT NULL) and 
  --  pqz.pqid  NOT in  ( select  LinkedQuestion5ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion5ID IS NOT NULL) and 
  --  pqz.pqid  NOT in  ( select  LinkedQuestion6ID  from ProfileQuestionAnswer where PQAnswerID in 
  --(select PQAnswerID from ProfileQuestionUserAnswer where
  -- UserID = @UserID) and LinkedQuestion6ID IS NOT NULL)
	) as items
	
	order by Weightage
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
END

/* Added By Khurram (02 Feb 2016) - End */

