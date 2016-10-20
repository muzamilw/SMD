USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetAudience]    Script Date: 10/19/2016 3:46:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		
-- Create date: 23-Dec-15
-- Description:	Returns Count Of Matched Users
-- =============================================

ALTER PROCEDURE [dbo].[GetAudience] 

	-- Add the parameters for the stored procedure here
	     @ageFrom AS INT,
		 @ageTo AS INT,
		 @gender AS INT,
		 @countryIds as nvarchar(500),
		 @cityIds as nvarchar(max),
		 @languageIds as nvarchar(500),
		 @industryIds as nvarchar(500),
		 @profileQuestionIds as nvarchar(500),
		 @profileAnswerIds as nvarchar(500),
		 @surveyQuestionIds as nvarchar(500),
		 @surveyAnswerIds as nvarchar(500),
		 @countryIdsExcluded as nvarchar(500),
		 @cityIdsExcluded as nvarchar(500),
		 @languageIdsExcluded as nvarchar(500),
		 @industryIdsExcluded as nvarchar(500),
		 @educationIds as nvarchar(500),
		 @educationIdsExcluded as nvarchar(500),
		 @profileQuestionIdsExcluded as nvarchar(500),
		 @surveyQuestionIdsExcluded as nvarchar(500),
		 @CampaignQuizIds as nvarchar(500),
		 @CampaignQuizAnswerIds as nvarchar(500),
		 @CampaignQuizIdsExcluded as nvarchar(500)

AS
BEGIN

	Declare @counter int = 1
	Declare @query nvarchar(max)
   
    Declare @where nvarchar(max)

	Declare @join nvarchar(max)

	Declare @dobStart  varchar(max)
	Declare @dobEnd  varchar(max)

	set @dobStart = DATEADD(yy, DATEDIFF(yy, 0, CAST(GETDATE() As date)) - @ageTo, 0)--DATEADD(YEAR, -@ageTo, CAST(GETDATE() As date))
	set @dobEnd = DATEADD (dd, -1, DATEADD(yy, DATEDIFF(yy, 0, GETDATE()) -@ageFrom + 1, 0)) -- DATEADD(YEAR, -@ageFrom, CAST(GETDATE() As date))


	




    set @where = ' where '
	set @join = ''
	set @query = 'SELECT count(*) as MatchingUsers, (select count(*) from AspNetUsers) as AllUsers FROM AspNetUsers SMDUser'
	set @where = @where + '((SMDUser.DOB >= '''+  @dobStart +'''  AND SMDUser.DOB <= '''+ @dobEnd +''')  or SMDUser.DOB is null)'
	--set @where = @where +'(SMDUser.DOB BETWEEN DATEADD(YEAR, -'+ CAST(@ageFrom AS NVARCHAR(10))+', CAST(GETDATE() As date)) AND DATEADD(YEAR, -'+ CAST(@ageTo AS NVARCHAR(10)) +', CAST(GETDATE() As date)))' -- ((SMDUser.Age >= ' + CAST(@ageFrom AS NVARCHAR(10)) + ' and SMDUser.Age <= ' + CAST(@ageTo AS NVARCHAR(10)) + ') or  SMDUser.Age is null) '
	
	if(@gender = 2)
	begin
		set @where = @where + ' and ( SMDUser.Gender in (1) or  SMDUser.Gender is null )'
	end
	else if(@gender = 3)
	begin
		set @where = @where + 'and (SMDUser.Gender in (2)   or  SMDUser.Gender is null )'
	end

	if(@countryIds IS NOT NULL AND @countryIds != '')
	begin
		set @join = ' inner join Company UserCompany on  SMDUser.CompanyId = UserCompany.CompanyId'
		set @where = @where + ' and UserCompany.CountryID in ('+ @countryIds +')'
	end
	if(@countryIdsExcluded IS NOT NULL AND @countryIdsExcluded != '')
	begin
		if(@countryIds IS NULL AND @countryIds = '')
		begin
			set @join = ' inner join Company UserCompany on  SMDUser.CompanyId = UserCompany.CompanyId'
		end
		
		set @where = @where + ' and UserCompany.CountryID not in ('+ @countryIdsExcluded +')'
	end
	if(@cityIds IS NOT NULL AND @cityIds != '')
	begin
		if(@join = '')
		begin
			set @join = ' inner join Company UserCompany on  SMDUser.CompanyId = UserCompany.CompanyId  inner join splitString(@Cityids,'','') cities on  UserCompany.BillingCity LIKE CONCAT(''%'', Cities.item, ''%'')  '
		end
		
		--set @where = @where + ' and  contains( UserCompany.BillingCity, '+ @cityIds +')'
	end
	if(@cityIdsExcluded IS NOT NULL AND @cityIdsExcluded != '')
	begin
		if(@join = '')
		begin
			--set @join = ' inner join Company UserCompany on  SMDUser.CompanyId = UserCompany.CompanyId'
			set @join = ' inner join Company UserCompanyn on  SMDUser.CompanyId = UserCompanyn.CompanyId  inner join splitString(@Cityidsex,'','') cities on  UserCompanyn.BillingCity NOT LIKE CONCAT(''%'', Cities.item, ''%'')  '
		end
		--set @where = @where + ' and UserCompany.City not in ('+ @cityIdsExcluded +')'
	end
    if(@languageIds IS NOT NULL AND @languageIds != '')
	begin
		set @where = @where + ' and SMDUser.LanguageID in ('+ @languageIds +')'
	end
	if(@languageIdsExcluded IS NOT NULL AND @languageIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.LanguageID not in ('+ @languageIdsExcluded +')'
	end
    if(@industryIds IS NOT NULL AND @industryIds != '')
	begin
		set @where = @where + ' and SMDUser.IndustryID in ('+ @industryIds +')'
	end
	if(@industryIdsExcluded IS NOT NULL AND @industryIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.IndustryID not in ('+ @industryIdsExcluded +')'
	end
	if(@educationIds IS NOT NULL AND @educationIds != '')
	begin
		set @where = @where + ' and SMDUser.EducationId in ('+ @educationIds +')'
	end
	if(@educationIdsExcluded IS NOT NULL AND @educationIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.EducationId not in ('+ @educationIdsExcluded +')'
	end


	--profile questions
	if(@profileQuestionIds IS NOT NULL AND @profileQuestionIds != '')
	begin
		
		Declare @Profilewhere nvarchar(max)
		Declare @PQIDFROMARRAY nvarchar(max)
		set @Profilewhere = 'SELECT DISTINCT UserID FROM ProfileQuestionUserAnswer where '
		while len(@profileQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				  if(@profileQuestionIdsExcluded IS NOT NULL AND @profileQuestionIdsExcluded != '')
					 begin
						 set @PQIDFROMARRAY = left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1)	
						 IF (charindex(@PQIDFROMARRAY, @surveyQuestionIdsExcluded) > 0)
							begin
								set @Profilewhere = @Profilewhere + ' (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @Profilewhere = @Profilewhere + ' (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						set @Profilewhere = @Profilewhere + ' (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
					end
				 
			end
			else if(@counter != 1)
			begin
				 if(@profileQuestionIdsExcluded IS NOT NULL AND @profileQuestionIdsExcluded != '')
					 begin
						 set @PQIDFROMARRAY = left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1)	
						 IF (charindex(@PQIDFROMARRAY, @surveyQuestionIdsExcluded) > 0)
							begin
								set @Profilewhere = @Profilewhere + ' intersect  select Distinct UserId from ProfileQuestionUserAnswer where (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @Profilewhere = @Profilewhere + ' intersect  select Distinct UserId from ProfileQuestionUserAnswer where (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						set @Profilewhere = @Profilewhere + ' intersect  select Distinct UserId from ProfileQuestionUserAnswer where (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
					end
			end
		 
		  set @profileQuestionIds = stuff(@profileQuestionIds, 1, charindex(',', @profileQuestionIds+','), '')
		  set @profileAnswerIds = stuff(@profileAnswerIds, 1, charindex(',', @profileAnswerIds +','), '')
		end
		set @where = @where + ' and SMDUser.Id in (' + @Profilewhere + ')'
	end



	--survey answers
	if(@surveyAnswerIds IS NOT NULL AND @surveyAnswerIds != '')
	begin
		set @counter = 1;
		Declare @Surveywhere nvarchar(max)
		DECLARE @SQIDFROMARRAY as varchar(max)
		set @Surveywhere = 'SELECT DISTINCT UserID FROM SurveyQuestionResponse where '
		while len(@surveyQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 if(@surveyQuestionIdsExcluded IS NOT NULL AND @surveyQuestionIdsExcluded != '')
					 begin
						 
						 set @SQIDFROMARRAY = left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1)	
						 IF (charindex(@SQIDFROMARRAY, @surveyQuestionIdsExcluded) > 0)
							begin
								 set @Surveywhere = @Surveywhere + ' (SQID !=' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection !=' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @Surveywhere = @Surveywhere + ' (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						 set @Surveywhere = @Surveywhere + ' (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
					end
				
			end
			else if(@counter != 1)
			begin
				 if(@surveyQuestionIdsExcluded IS NOT NULL AND @surveyQuestionIdsExcluded != '')
					 begin
						 set @SQIDFROMARRAY = left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1)	
						 IF (charindex(@SQIDFROMARRAY, @surveyQuestionIdsExcluded) > 0)
							begin
								 set @Surveywhere = @Surveywhere + ' and (SQID !=' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection !=' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @Surveywhere = @Surveywhere + ' and (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						 set @Surveywhere = @Surveywhere + ' and (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
					end
			end

		  set @surveyQuestionIds = stuff(@surveyQuestionIds, 1, charindex(',', @surveyQuestionIds +','), '')
		  set @surveyAnswerIds = stuff(@surveyAnswerIds, 1, charindex(',', @surveyAnswerIds +','), '')
		 
		end
		set @where = @where + ' and SMDUser.Id in (' + @Surveywhere + ')'
	end



	--campaign quiz answers
	if(@CampaignQuizIds IS NOT NULL AND @CampaignQuizIds != '')
	begin
		set @counter = 1;
		Declare @CampQuizwhere nvarchar(max)
		declare @QuizIDFROMARRAY nvarchar(max)
		
		set @CampQuizwhere = 'SELECT DISTINCT UserID FROM AdCampaignResponse where '
		while len(@CampaignQuizIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 if(@CampaignQuizIdsExcluded IS NOT NULL AND @CampaignQuizIdsExcluded != '')
					 begin
						 
						 set @QuizIDFROMARRAY = left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1)	
						 IF (charindex(@QuizIDFROMARRAY, @CampaignQuizIdsExcluded) > 0)
							begin
								 set @CampQuizwhere = @CampQuizwhere + ' (CampaignID !=' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse !=' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @CampQuizwhere = @CampQuizwhere + ' (CampaignID =' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse =' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						 set @CampQuizwhere = @CampQuizwhere + ' (CampaignID =' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse =' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
					end
				
			end
			else if(@counter != 1)
			begin
				 if(@CampaignQuizIdsExcluded IS NOT NULL AND @CampaignQuizIdsExcluded != '')
					 begin
						 set @QuizIDFROMARRAY = left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1)	
						 IF (charindex(@QuizIDFROMARRAY, @CampaignQuizIdsExcluded) > 0)
							begin
								 set @CampQuizwhere = @CampQuizwhere + ' and (CampaignID !=' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse !=' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
							end
						else
							begin
								set @CampQuizwhere = @CampQuizwhere + ' and (CampaignID =' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse =' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
							end
					  
					 end
				 else
					begin
						 set @CampQuizwhere = @CampQuizwhere + ' and (CampaignID =' + left(@CampaignQuizIds, charindex(',', @CampaignQuizIds+',')-1) + ' and UserQuestionResponse =' + left(@CampaignQuizAnswerIds, charindex(',', @CampaignQuizAnswerIds +',')-1) + ')'
					end
			end

		  set @CampaignQuizIds= stuff(@CampaignQuizIds, 1, charindex(',', @CampaignQuizIds+','), '')
		  set @CampaignQuizAnswerIds = stuff(@CampaignQuizAnswerIds, 1, charindex(',', @CampaignQuizAnswerIds +','), '')
		 
		end
		set @where = @where + ' and SMDUser.Id in (' + @CampQuizwhere + ')'
	end

--select (@query + @join + @where)
--exec(@query + @join + @where)

declare @SQL nvarchar(max)
set @SQL = @query + @join + @where

EXECUTE SP_EXECUTESQL @SQL,N'@Cityids nvarchar(max), @Cityidsex nvarchar(max)',
@Cityids = @cityIds, @Cityidsex = @cityIdsExcluded;
END

 

