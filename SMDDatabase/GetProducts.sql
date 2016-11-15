USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetProducts1]    Script Date: 11/15/2016 9:01:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--top(4) WITH TIES ads.*, g.GameUrl,COUNT(*) OVER( partition by type) AS AdCount from

ALTER PROCEDURE [dbo].[GetProducts] 

--  select weightage,* from [GetUserProfileQuestions]('b8a3884f-73f3-41ec-9926-293ea919a5e11', 214) x order by x.weightage

-- exec [dbo].[GetProducts] 		@UserID = N'bc23eeda-1605-482c-86ec-401901b2fb13', 		@FromRow = 0, 		@ToRow = 190
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
DECLARE @FreeAdsPosition as int

if (@FromRow = 0 )
	set @FreeAdsPosition = 0
else 
	begin
		set @FreeAdsPosition = @FromRow - (@FromRow * 4 /10)

	end

        -- Setting local variables
		   SELECT @dob = DOB,@gender = gender ,@languageId = LanguageID,@industryId = industryId FROM AspNetUsers where id=@UserID
		 
		     SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
		   SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
		  		 
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @dob, @currentDate)


select *, COUNT(*) OVER() AS TotalItems--, 
from
(	


	select ads.*, g.GameUrl,0 as AdCount from
	(
			select adcampaign.campaignid as ItemId, adcampaign.CampaignName ItemName, 'Ad' Type, 
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
			((adcampaign.ClickRate * 50) / 100) * 100 as AdClickRate,  -- Amount AdViewer will get
			case  when adcampaign.Type = 4  -- Flyer
				then
				  'http://manage.cash4ads.com/' + AdCampaign.LogoUrl
				  end
			as AdImagePath,  --adcampaign.ImagePath 
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
			((row_number() over (order by isNUll(clickrate,0) desc) * 100) + 1) Weightage,

			NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
			null as SocialHandle, null as SocialHandleType,
			
				--case when AdCampaign.Type = 1 then
					(select 'http://manage.cash4ads.com/' + c.logo from Company c
					where c.CompanyId = adcampaign.CompanyId)
				--when AdCampaign.Type = 4 then
				--	'http://manage.cash4ads.com/' + AdCampaign.LogoUrl
				--end

			 as AdvertisersLogoPath,

			adcampaign.LandingPageVideoLink as LandingPageUrl,--adcampaign.VideoUrl as LandingPageUrl,
			adcampaign.BuuyItLine1 as BuyItLine1,
			adcampaign.BuyItLine2 as BuyItLine2,
			 adcampaign.BuyItLine3 as BuyItLine3,
			adcampaign.BuyItButtonLabel as BuyItButtonText, 
			 'http://manage.cash4ads.com/' +adcampaign.BuyItImageUrl as BuyItImageUrl,
			'http://manage.cash4ads.com/' + adcampaign.Voucher1ImagePath as VoucherImagePath,
			--(select count(*) from coupon v where AdCampaign.CompanyId = v.CompanyId and v.Status = 3) as VoucherCount,
			0 as VoucherCount,
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
			inner join Company cc on adcampaign.CompanyId = cc.CompanyId and cc.IsSpecialAccount is null
			--calc of max daily budget for the campaign for current date. converting centz to dollar amount by 100 division
			outer apply
				(
					select isnull(sum(debitamount),0)/100 as todaySpent from [transaction] t
					inner join account a on t.accountid = a.accountid and a.AccountType = 4 and a.companyid = adcampaign.companyid
					where t.adcampaignid = adcampaign.campaignid and CAST(TransactionDate AS DATE) = CAST(getdate() AS DATE)
				) as campaignbudget
			--  profile question filtering join
			outer apply
				(	
					select count(*) totalcrit, count(uans.pqid) matchcrit from [AdCampaignTargetCriteria]  crit
					left outer join
						( --get the most recent answer
							select * from 
							(
									select pqa.*, row_number() over (partition by userid,pqid order by answerdatetime desc) as rn
								   from [ProfileQuestionUserAnswer] pqa where userid = @UserID and responsetype = 3 -- 3 is valid response type which has answer

							) dans  where dans.rn = 1 

						) uans on (crit.IncludeorExclude = 1 and uans.PQAnswerID = crit.PQAnswerID) or (crit.IncludeorExclude = 0 and uans.PQAnswerID <> crit.PQAnswerID) --type 1 is profile question
						where crit.campaignid = adcampaign.campaignid and crit.[type] = 1
			) pqf  
			--  my/user survey questions filtering join not enabled right now
			--outer apply
			--	(	
			--		select count(*) totalcrit, count(uans.sqid) matchcrit from [AdCampaignTargetCriteria]  crit
			--		left outer join
			--			( --get the most recent answer
			--				select * from 
			--				(
			--						select sqr.*, row_number() over (partition by userid,sqid order by ResoponseDateTime desc) as rn
			--					   from [SurveyQuestionResponse] sqr where userid = @UserID and responsetype = 3 -- 3 is valid response type which has answer

			--				) dans  where dans.rn = 1 

			--			) uans on crit.[type] = 2  and crit.IncludeorExclude = 1 and uans.UserSelection = crit.SQAnswer  --type 2 is user survey question
			--			where crit.campaignid = adcampaign.campaignid
			--) mysqf  

			--  user Campaign quiz question filtering join 
			outer apply
				(	
					select count(*) totalcrit, count(uans.CampaignId) matchcrit from [AdCampaignTargetCriteria]  crit
					left outer join
						( --get the most recent answer
							select * from 
							(
									select acr.*, row_number() over (partition by userid,campaignid order by CreatedDateTime desc) as rn
								   from AdCampaignResponse acr where userid = @UserID and responsetype = 3 -- 3 is valid response type which has answer

							) dans  where dans.rn = 1 

						) uans on (crit.IncludeorExclude = 1 and uans.UserQuestionResponse = crit.QuizAnswerId ) or (crit.IncludeorExclude = 0 and uans.UserQuestionResponse <> crit.QuizAnswerId )  --type 6 is user Quiz answer selection
						where crit.campaignid = adcampaign.campaignid and crit.[type] = 6
			) acf  

			--  Industry criteria
			outer apply
				(	
					select count(*) totalcrit, count(uans.id) matchcrit from [AdCampaignTargetCriteria]  crit
					left outer join
						( --get the most recent answer
							select isnull(IndustryID,0) industryid,id from aspnetusers where id = @UserID
							   

						) uans on (crit.IncludeorExclude = 1 and uans.industryid = crit.industryid ) or (crit.IncludeorExclude = 0 and uans.industryid <> crit.industryid ) --type 4 is industry
						where crit.campaignid = adcampaign.campaignid and crit.[type] = 4
			) indf  

			--  Location criteria

			outer apply
			(	
				select count(*) totalcrit, count(uans.id) matchcrit from [AdCampaignTargetLocation]  crit
				left outer join
					( --get the most recent answer
						select isnull(cit.cityid,0) BillingCityID,c.BillingCity, cityid,c.billingCountryid,id from aspnetusers u inner join Company c on u.companyid = c.companyid and id = @UserID 
						left outer join city cit on c.BillingCity LIKE CONCAT(cit.CityName, '%') 
							   

					) uans on (crit.IncludeorExclude = 1 and uans.BillingCityID = crit.CityID  and uans.BillingCountryID = crit.CountryID ) or (crit.IncludeorExclude = 0 and uans.BillingCityID <> crit.CityID  and uans.BillingCountryID <> crit.CountryID ) --type 4 is industry
					where crit.campaignid = adcampaign.campaignid
		) locf  


	
			where ( 
						((@age is null) or (adcampaign.AgeRangeEnd >= @age and  @age >= adcampaign.AgeRangeStart))
						and
						((adcampaign.Gender = 1) or (adcampaign.Gender = @gender ))
						--and
						--((@languageId is null) or (adcampaign.LanguageId = @languageId))
						and
							(@currentDate >= adcampaign.approvaldatetime)
						and
						(adcampaign.Approved = 1)
						and
						(adcampaign.Status = 3) -- live
						and
						( adcampaign.MaxdailyBudget >= campaignbudget.todaySpent)

						--and
						--((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
						--	 where MyCampaignLoc.CampaignID=adcampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
						--	 MyCampaignLoc.CityID=@cityId) > 0))

						and
						-- location type matching clauses -  the filtering logic of location country and city
						(
							1=1--(locf.matchcrit > 0 )
						)

						and
						-- industry type matching clauses -  the filtering logic of industry types
						(
							1=1--(indf.matchcrit > 0 )
						)
						and
						--- system profile questions - the filtering logic of profile insights 
						(
						
							(pqf.totalcrit  = pqf.matchcrit ) 
						
						)
						and
						--- user adcampaign quiz answer - the filtering logic of ad quiz results 
						(
							(acf.totalcrit  = acf.matchcrit ) 

						)
						and 
						--do not show the ad again to the user if already watched today.
						(((select count(*) from AdCampaignResponse adResponse where  
							adResponse.CampaignID = adcampaign.CampaignID and responsetype = 3 and adResponse.UserID = @UserID and datepart(day,getdate()) = datepart(day,adResponse.CreatedDateTime) and datepart(month,getdate()) = datepart(month,adResponse.CreatedDateTime) ) = 0)
						 or
						 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
							where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
							is null)
						) 
			)  
			


			

		) ads
		
		left outer join Game g on ads.GameId = g.gameId
		--order by type
		
			
			--response type 1 = normal video watched
			-- response type 2 = skipped
			 -- response type 3 = buy it
			 --type 4 = free game won
			 
			
			
	
	

	------------------- survey questions
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
	pqz where pqz.pqid  NOT in  ( select  LinkedQuestion1ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion1ID IS NOT NULL) and 
    pqz.pqid  NOT in  ( select  LinkedQuestion2ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion2ID IS NOT NULL) and 
    pqz.pqid  NOT in  ( select  LinkedQuestion3ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion3ID IS NOT NULL) and 
    pqz.pqid  NOT in  ( select  LinkedQuestion4ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion4ID IS NOT NULL) and 
    pqz.pqid  NOT in  ( select  LinkedQuestion5ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion5ID IS NOT NULL) and 
    pqz.pqid  NOT in  ( select  LinkedQuestion6ID  from ProfileQuestionAnswer where PQAnswerID in 
  (select PQAnswerID from ProfileQuestionUserAnswer where
   UserID = @UserID) and LinkedQuestion6ID IS NOT NULL)




) as items
	
	order by Weightage
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
END

/* Added By Khurram (02 Feb 2016) - End */

