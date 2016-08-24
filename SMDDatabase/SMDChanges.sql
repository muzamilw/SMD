﻿
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Industry
	(
	IndustryID int NOT NULL IDENTITY (1, 1),
	IndustryName nvarchar(200) NULL,
	Status int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Industry ADD CONSTRAINT
	PK_Industry PRIMARY KEY CLUSTERED 
	(
	IndustryID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Industry SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	LanguageID int NULL,
	IndustryID int NULL
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Industry FOREIGN KEY
	(
	IndustryID
	) REFERENCES dbo.Industry
	(
	IndustryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Language FOREIGN KEY
	(
	LanguageID
	) REFERENCES dbo.Language
	(
	LanguageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products ADD
	ClausePrice float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products ADD
	AffiliatePercentage float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Products', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'CONTROL') as Contr_Per 

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Industry SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD
	IndustryID int NULL
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD CONSTRAINT
	FK_AdCampaignTargetCriteria_Industry FOREIGN KEY
	(
	IndustryID
	) REFERENCES dbo.Industry
	(
	IndustryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD
	IndustryID int NULL
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_Industry FOREIGN KEY
	(
	IndustryID
	) REFERENCES dbo.Industry
	(
	IndustryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


-------------------------------20151217


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation
	DROP CONSTRAINT FK_SurveyQuestionTargetLocation_Country
GO
ALTER TABLE dbo.Country SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation
	DROP CONSTRAINT FK_SurveyQuestionTargetLocation_City
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation
	DROP CONSTRAINT FK_SurveyQuestionTargetLocation_SurveyQuestion
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SurveyQuestionTargetLocation
	(
	ID bigint NOT NULL IDENTITY (1, 1),
	SQID bigint NULL,
	CountryID int NULL,
	CityID int NULL,
	Radius int NULL,
	IncludeorExclude bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SurveyQuestionTargetLocation SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SurveyQuestionTargetLocation ON
GO
IF EXISTS(SELECT * FROM dbo.SurveyQuestionTargetLocation)
	 EXEC('INSERT INTO dbo.Tmp_SurveyQuestionTargetLocation (ID, SQID, CountryID, CityID, Radius, IncludeorExclude)
		SELECT ID, SQID, CountryID, CityID, Radius, IncludeorExclude FROM dbo.SurveyQuestionTargetLocation WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SurveyQuestionTargetLocation OFF
GO
DROP TABLE dbo.SurveyQuestionTargetLocation
GO
EXECUTE sp_rename N'dbo.Tmp_SurveyQuestionTargetLocation', N'SurveyQuestionTargetLocation', 'OBJECT' 
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation ADD CONSTRAINT
	PK_SurveyQuestionTargetLocation PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveyQuestionTargetLocation ADD CONSTRAINT
	FK_SurveyQuestionTargetLocation_SurveyQuestion FOREIGN KEY
	(
	SQID
	) REFERENCES dbo.SurveyQuestion
	(
	SQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation ADD CONSTRAINT
	FK_SurveyQuestionTargetLocation_City FOREIGN KEY
	(
	CityID
	) REFERENCES dbo.City
	(
	CityID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetLocation ADD CONSTRAINT
	FK_SurveyQuestionTargetLocation_Country FOREIGN KEY
	(
	CountryID
	) REFERENCES dbo.Country
	(
	CountryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria
	DROP CONSTRAINT FK_SurveyQuestionTargetCriteria_ProfileQuestion
GO
ALTER TABLE dbo.ProfileQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria
	DROP CONSTRAINT FK_SurveyQuestionTargetCriteria_Industry
GO
ALTER TABLE dbo.Industry SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria
	DROP CONSTRAINT FK_SurveyQuestionTargetCriteria_ProfileQuestionAnswer
GO
ALTER TABLE dbo.ProfileQuestionAnswer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria
	DROP CONSTRAINT FK_SurveyQuestionTargetCriteria_SurveyQuestion
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria
	DROP CONSTRAINT FK_SurveyQuestionTargetCriteria_SurveyQuestion1
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SurveyQuestionTargetCriteria
	(
	ID bigint NOT NULL IDENTITY (1, 1),
	SQID bigint NULL,
	Type int NULL,
	PQID int NULL,
	PQAnswerID int NULL,
	LinkedSQID bigint NULL,
	LinkedSQAnswer int NULL,
	IncludeorExclude bit NULL,
	LanguageID int NULL,
	IndustryID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SurveyQuestionTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'1 - Profile Question , 2 - Survery Question, 3 - Language'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_SurveyQuestionTargetCriteria', N'COLUMN', N'Type'
GO
SET IDENTITY_INSERT dbo.Tmp_SurveyQuestionTargetCriteria ON
GO
IF EXISTS(SELECT * FROM dbo.SurveyQuestionTargetCriteria)
	 EXEC('INSERT INTO dbo.Tmp_SurveyQuestionTargetCriteria (ID, SQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSQAnswer, IncludeorExclude, LanguageID, IndustryID)
		SELECT ID, SQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSQAnswer, IncludeorExclude, LanguageID, IndustryID FROM dbo.SurveyQuestionTargetCriteria WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SurveyQuestionTargetCriteria OFF
GO
DROP TABLE dbo.SurveyQuestionTargetCriteria
GO
EXECUTE sp_rename N'dbo.Tmp_SurveyQuestionTargetCriteria', N'SurveyQuestionTargetCriteria', 'OBJECT' 
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	PK_SurveyQuestionTargetCriteria PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_SurveyQuestion FOREIGN KEY
	(
	SQID
	) REFERENCES dbo.SurveyQuestion
	(
	SQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_SurveyQuestion1 FOREIGN KEY
	(
	LinkedSQID
	) REFERENCES dbo.SurveyQuestion
	(
	SQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_ProfileQuestionAnswer FOREIGN KEY
	(
	PQAnswerID
	) REFERENCES dbo.ProfileQuestionAnswer
	(
	PQAnswerID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_Industry FOREIGN KEY
	(
	IndustryID
	) REFERENCES dbo.Industry
	(
	IndustryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_ProfileQuestion FOREIGN KEY
	(
	PQID
	) REFERENCES dbo.ProfileQuestion
	(
	PQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
-- ====================================== By  Baqer On  21-Dec-15 5:45:37 PM

GO
/****** Object:  StoredProcedure [dbo].[GetAds]    Script Date: 21-Dec-15 5:45:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Baqer Naqvi - IST 
-- Create date: 17-Dec-15
-- Description:	Returns Ads for specified User
-- =============================================

ALTER PROCEDURE [dbo].[GetAds] 

	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	-- newly added 
	@FromRow int =0           ,
	@ToRow int =0

AS
BEGIN
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT

        -- Setting local variables
		   SELECT @age = age FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SELECT @countryId = CountryID FROM AspNetUsers where id=@UserID
		   SELECT @cityId = CityID FROM AspNetUsers where id=@UserID


		   select MyCampaign.CampaignID, MyCampaign.CampaignName, MyCampaign.Description, MyCampaign.VerifyQuestion, MyCampaign.LandingPageVideoLink ,
		   MyCampaign.Answer1, MyCampaign.Answer2, MyCampaign.Answer3, MyCampaign.CorrectAnswer , MyCampaign.ClickRate
		   from AdCampaign MyCampaign

		   where ( 
		    (MyCampaign.AgeRangeEnd >= @age and  @age >= MyCampaign.AgeRangeStart) 
			 and
			(MyCampaign.Gender= @gender)
			 and
			(MyCampaign.EndDateTime >= GETDATE() and GETDATE() >= MyCampaign.StartDateTime ) 
			 and
			(MyCampaign.Approved = 1) 
		  	 and
			(MyCampaign.Status = 6)   
			 and 
			((select count(*) from AdCampaignResponse MyCampaignResponse
			 where MyCampaignResponse.UserID=@UserID and MyCampaignResponse.CampaignID = MyCampaign.CampaignID) = 0) 
			 and
		    (MyCampaign.MaxBudget > MyCampaign.AmountSpent) 
			 and
			(MyCampaign.LanguageID=@languageId) 
			 and
			 ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			 where MyCampaignLoc.CampaignID=MyCampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
			 MyCampaignLoc.CityID=@cityId) > 0 
			 and 
			 ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			 where MyCampaignCrit.CampaignID = MyCampaign.CampaignID and 
			 MyCampaignCrit.LanguageID=@languageId and MyCampaignCrit.IndustryID=@industryId) > 0 )) 
		   )

		   -- newly added for paging 
		   order by MyCampaign.ApprovalDateTime
		   OFFSET @FromRow ROWS -- skip 10 rows
		   FETCH NEXT @ToRow ROWS ONLY; -- take 10 rows
END



GO


/* Added By Khurram - 17 Dec 2015 (Start) */ 
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[AccountId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](200) NULL,
	[AccountType] [int] NULL,
	[AccountBalance] [decimal](18, 0) NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_AspNetUsers]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Account] ([AccountId])
GO

ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO

/* Added By Khurram - 17 Dec 2015 (End) */ 

-- Baqer Naqvi - IST 21-DEC-15
GO
/****** Object:  StoredProcedure [dbo].[GetSurveys]    Script Date: 21-Dec-15 5:46:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Baqer Naqvi - IST 
-- Create date: 18-Dec-15
-- Description:	Returns Surveys for specified User
-- =============================================

ALTER PROCEDURE [dbo].[GetSurveys] 

	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	@FromRow int =0           ,
	@ToRow int =0

AS
BEGIN
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT

        -- Setting local variables
		   SELECT @age = age FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SELECT @countryId = CountryID FROM AspNetUsers where id=@UserID
		   SELECT @cityId = CityID FROM AspNetUsers where id=@UserID


		   select MySurveyQuestion.SQID,  MySurveyQuestion.Question, MySurveyQuestion.Description, MySurveyQuestion.DisplayQuestion ,
		   MySurveyQuestion.LeftPicturePath, MySurveyQuestion.RightPicturePath, MySurveyQuestion.ApprovalDate, MySurveyQuestion.VoucherCode , 
		   MySurveyQuestion.ResultClicks 
		   from SurveyQuestion MySurveyQuestion

		   where ( 
		    (MySurveyQuestion.AgeRangeEnd >= @age and  @age >= MySurveyQuestion.AgeRangeStart) 
			 and
			(MySurveyQuestion.Gender= @gender)
			 and
			(MySurveyQuestion.EndDate >= GETDATE() and GETDATE() >= MySurveyQuestion.StartDate ) 
			 and
			(MySurveyQuestion.Approved = 1) 
		  	 and
			(MySurveyQuestion.Status = 6)   
			 and 
			((select count(*) from SurveyQuestionResponse MySurveyQuestionResponse
			 where MySurveyQuestionResponse.UserID=@UserID and MySurveyQuestionResponse.SQID = MySurveyQuestion.SQID) = 0) 
			 and
			(MySurveyQuestion.LanguageID=@languageId) 
			 and
			 ((select count(*) from SurveyQuestionTargetLocation MySurveyQuestionLoc
			 where MySurveyQuestionLoc.SQID=MySurveyQuestion.SQID and MySurveyQuestionLoc.CountryID=@countryId and
			 MySurveyQuestionLoc.CityID=@cityId)) > 0 
			 and 
			 ((select count(*) from SurveyQuestionTargetCriteria MySurveyQuestionCrit
			 where MySurveyQuestionCrit.SQID = MySurveyQuestion.SQID and 
			 MySurveyQuestionCrit.LanguageID=@languageId and MySurveyQuestionCrit.IndustryID=@industryId)) > 0 
		   )

		   --  for paging 
		   order by MySurveyQuestion.ApprovalDate
		   OFFSET @FromRow ROWS -- skip 10 rows
		   FETCH NEXT @ToRow ROWS ONLY; -- take 10 rows
END










/* Added by baqer 18 Dec 2015 (End)*/

/* added by iqra 21 dec 2015*/


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD CONSTRAINT
	FK_AdCampaignTargetCriteria_Language FOREIGN KEY
	(
	LanguageID
	) REFERENCES dbo.Language
	(
	LanguageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_Language FOREIGN KEY
	(
	LanguageID
	) REFERENCES dbo.Language
	(
	LanguageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* added by iqra 21 dec 2015*/



-- =========================== ADDED by Baqer  21-DEC-2015  | start

GO
/****** Object:  StoredProcedure [dbo].[GetAudienceAdCampaign]    Script Date: 21-Dec-15 5:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Baqer Naqvi - IST 
-- Create date: 19-Dec-15
-- Description:	Returns Count Of Matched Users
-- =============================================

ALTER PROCEDURE [dbo].[GetAudienceAdCampaign] 

	-- Add the parameters for the stored procedure here
	     @age AS INT,
		 @gender AS INT,
		 @countryId AS INT,
		 @cityId AS INT,
		 @languageId AS INT,
		 @industryId AS INT,
		 @profileQuestionIds as nvarchar(500)


AS
BEGIN
DECLARE @countOfids AS INT

    -- User Defined Split function 
	SELECT @countOfids=  count(*) from SplitString(@profileQuestionIds,',') 

    SELECT * FROM AspNetUsers SMDUser
	where
	SMDUser.Age= @age
	and
	SMDUser.Gender= @gender 
	and 
	SMDUser.CountryID= @countryId
	and 
	SMDUser.CityID= @cityId
	and 
	SMDUser.LanguageID= @languageId
	and 
	SMDUser.IndustryID= @industryId
	and
	(select count(*) from ProfileQuestionUserAnswer MyAnswer where MyAnswer.UserID = SMDUser.Id and 
     MyAnswer.PQID IN (SELECT  * from SplitString(@profileQuestionIds,','))) > = @countOfids

END

 
GO
/****** Object:  StoredProcedure [dbo].[GetAudienceSurvey]    Script Date: 21-Dec-15 5:50:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Baqer Naqvi - IST 
-- Create date: 19-Dec-15
-- Description:	Returns Count Of Matched Users
-- =============================================

ALTER PROCEDURE [dbo].[GetAudienceSurvey] 

	-- Add the parameters for the stored procedure here
	     @age AS INT,
		 @gender AS INT,
		 @countryId AS INT,
		 @cityId AS INT,
		 @languageId AS INT,
		 @industryId AS INT,
		 @profileQuestionIds as nvarchar(500)


AS
BEGIN
DECLARE @countOfids AS INT

    -- User Defined Split function 
	SELECT @countOfids=  count(*) from SplitString(@profileQuestionIds,',') 

    SELECT COUNT(*) FROM AspNetUsers SMDUser
	where
	SMDUser.Age= @age
	and
	SMDUser.Gender= @gender 
	and 
	SMDUser.CountryID= @countryId
	and 
	SMDUser.CityID= @cityId
	and 
	SMDUser.LanguageID= @languageId
	and 
	SMDUser.IndustryID= @industryId
	and
	(select count(*) from ProfileQuestionUserAnswer MyAnswer where MyAnswer.UserID = SMDUser.Id and 
     MyAnswer.PQID IN (SELECT  * from SplitString(@profileQuestionIds,','))) >= @countOfids

END



GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 21-Dec-15 5:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[SplitString] (
      @InputString                  VARCHAR(8000),
      @Delimiter                    VARCHAR(50)
)

RETURNS @Items TABLE (
      Item                          VARCHAR(8000)
)

AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END

      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','

--INSERT INTO @Items VALUES (@Delimiter) -- Diagnostic
--INSERT INTO @Items VALUES (@InputString) -- Diagnostic

      DECLARE @Item                 VARCHAR(8000)
      DECLARE @ItemList       VARCHAR(8000)
      DECLARE @DelimIndex     INT

      SET @ItemList = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)

            -- Set @ItemList = @ItemList minus one less item
            SET @ItemList = SUBSTRING(@ItemList, @DelimIndex+1, LEN(@ItemList)-@DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END -- End WHILE

      IF @Item IS NOT NULL -- At least one delimiter was encountered in @InputString
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END

      -- No delimiters were encountered in @InputString, so just return @InputString
      ELSE INSERT INTO @Items VALUES (@InputString)
      RETURN
END -- End Function
GO

-- =========================== ADDED by Baqer  21-DEC-2015  | end


/* Added by Khurram - 22 Dec 2015 (Start) */
/* Invoice */

/****** Object:  Table [dbo].[Invoice]    Script Date: 12/22/2015 1:06:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceDueDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Tax] [float] NULL,
	[Amout] [float] NULL,
	[GrossAmount] [nchar](10) NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 12/22/2015 1:12:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ItemName] [nvarchar](250) NOT NULL,
	[ItemTax] [float] NULL,
	[ItemDiscount] [float] NULL,
	[ItemAmount] [float] NULL,
	[ItemGrossAmount] [float] NULL,
	[ItemDescription] [nvarchar](300) NULL,
	[ItemId] [bigint] NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO

/* Added by Khurram - 22 Dec 2015 (End) */





--   =============================  By Baqer - 22 DEC 2015 START



GO

ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_AspNetUsers]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 22-Dec-15 4:49:39 PM ******/
DROP TABLE [dbo].[Invoice]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 22-Dec-15 4:49:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceDueDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[TaxPercentage] [float] NULL,
	[Total] [float] NOT NULL,
	[NetTotal] [nchar](10) NOT NULL,
	[TaxValue] [float] NULL,
	[CompanyName] [nvarchar](250) NOT NULL,
	[Address1] [nvarchar](250) NULL,
	[Address2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[Country] [nvarchar](250) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[CreditCardRef] [nvarchar](250) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_AspNetUsers]

GO


-- second 


GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_SurveyQuestion]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_Products]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_InvoiceDetail]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_AdCampaign]
GO

/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 22-Dec-15 4:52:52 PM ******/
DROP TABLE [dbo].[InvoiceDetail]
GO

/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 22-Dec-15 4:52:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ItemName] [nvarchar](250) NOT NULL,
	[ItemTax] [float] NULL,
	[ItemAmount] [float] NOT NULL,
	[ItemGrossAmount] [float] NOT NULL,
	[ItemDescription] [nvarchar](300) NULL,
	[CampaignId] [bigint] NULL,
	[SQId] [bigint] NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_AdCampaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[AdCampaign] ([CampaignID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_AdCampaign]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceDetail] FOREIGN KEY([InvoiceDetailId])
REFERENCES [dbo].[InvoiceDetail] ([InvoiceDetailId])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceDetail]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Products]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_SurveyQuestion] FOREIGN KEY([SQId])
REFERENCES [dbo].[SurveyQuestion] ([SQID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_SurveyQuestion]
GO






--   =============================  By Baqer - 22 DEC 2015 END


-- ============================= updated on server =============================
--   =============================  By Baqer - 23 DEC 2015 START

GO

ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_AspNetUsers]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 23-Dec-15 1:17:50 PM ******/
DROP TABLE [dbo].[Invoice]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 23-Dec-15 1:17:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceDueDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[TaxPercentage] [float] NULL,
	[Total] [float] NOT NULL,
	[NetTotal] [float] NOT NULL,
	[TaxValue] [float] NULL,
	[CompanyName] [nvarchar](250) NOT NULL,
	[Address1] [nvarchar](250) NULL,
	[Address2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[Country] [nvarchar](250) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[CreditCardRef] [nvarchar](250) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_AspNetUsers]
GO



GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_SurveyQuestion]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_Products]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_InvoiceDetail]
GO

ALTER TABLE [dbo].[InvoiceDetail] DROP CONSTRAINT [FK_InvoiceDetail_AdCampaign]
GO

/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 23-Dec-15 1:18:27 PM ******/
DROP TABLE [dbo].[InvoiceDetail]
GO

/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 23-Dec-15 1:18:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ItemName] [nvarchar](250) NOT NULL,
	[ItemTax] [float] NULL,
	[ItemAmount] [float] NOT NULL,
	[ItemGrossAmount] [float] NOT NULL,
	[ItemDescription] [nvarchar](300) NULL,
	[CampaignId] [bigint] NULL,
	[SQId] [bigint] NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_AdCampaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[AdCampaign] ([CampaignID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_AdCampaign]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceDetail] FOREIGN KEY([InvoiceDetailId])
REFERENCES [dbo].[InvoiceDetail] ([InvoiceDetailId])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceDetail]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Products]
GO

ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_SurveyQuestion] FOREIGN KEY([SQId])
REFERENCES [dbo].[SurveyQuestion] ([SQID])
GO

ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_SurveyQuestion]
GO


--   =============================  By Baqer - 23 DEC 2015 END



--   =============================  By  - 28 DEC 2015 

/****** Object:  StoredProcedure [dbo].[GetAudience]    Script Date: 12/28/2015 10:58:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		
-- Create date: 23-Dec-15
-- Description:	Returns Count Of Matched Users
-- =============================================

Create PROCEDURE [dbo].[GetAudience] 

	-- Add the parameters for the stored procedure here
	     @ageFrom AS INT,
		 @ageTo AS INT,
		 @gender AS INT,
		 @countryIds as nvarchar(500),
		 @cityIds as nvarchar(500),
		 @languageIds as nvarchar(500),
		 @industryIds as nvarchar(500),
		 @profileQuestionIds as nvarchar(500),
		 @profileAnswerIds as nvarchar(500),
		 @surveyQuestionIds as nvarchar(500),
		 @surveyAnswerIds as nvarchar(500),
		 @countryIdsExcluded as nvarchar(500),
		 @cityIdsExcluded as nvarchar(500),
		 @languageIdsExcluded as nvarchar(500),
		 @industryIdsExcluded as nvarchar(500)


AS
BEGIN

	Declare @counter int = 1
	Declare @query nvarchar(max)
   
    Declare @where nvarchar(max)
	

    set @where = ' where '

	set @query = 'SELECT count(*) as MatchingUsers, (select count(*) from AspNetUsers) as AllUsers FROM AspNetUsers SMDUser'
	set @where = @where + ' SMDUser.Age >= ' + CAST(@ageFrom AS NVARCHAR(10)) + ' and SMDUser.Age <= ' + CAST(@ageTo AS NVARCHAR(10)) + ' and SMDUser.Gender = ' + CAST(@gender AS NVARCHAR(10))

	if(@countryIds IS NOT NULL AND @countryIds != '')
	begin
		set @where = @where + ' and SMDUser.CountryID in ('+ @countryIds +')'
	end
	if(@countryIdsExcluded IS NOT NULL AND @countryIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.CountryID not in ('+ @countryIdsExcluded +')'
	end
	if(@cityIds IS NOT NULL AND @cityIds != '')
	begin
		set @where = @where + ' and SMDUser.CityID in ('+ @cityIds +')'
	end
	if(@cityIdsExcluded IS NOT NULL AND @cityIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.CityID not in ('+ @cityIdsExcluded +')'
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
	if(@profileQuestionIds IS NOT NULL AND @profileQuestionIds != '')
	begin
		
		Declare @Profilewhere nvarchar(max)
		set @Profilewhere = 'SELECT DISTINCT UserID FROM ProfileQuestionUserAnswer where '
		while len(@profileQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 set @Profilewhere = @Profilewhere + ' (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
			end
			else if(@counter != 1)
			begin
				 set @Profilewhere = @Profilewhere + ' and (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
			end
		 
		  set @profileQuestionIds = stuff(@profileQuestionIds, 1, charindex(',', @profileQuestionIds+','), '')
		  set @profileAnswerIds = stuff(@profileAnswerIds, 1, charindex(',', @profileAnswerIds +','), '')
		end
		set @where = @where + ' and SMDUser.Id in (' + @Profilewhere + ')'
	end
	if(@surveyAnswerIds IS NOT NULL AND @surveyAnswerIds != '')
	begin
		set @counter = 1;
		Declare @Surveywhere nvarchar(max)
		set @Surveywhere = 'SELECT DISTINCT UserID FROM SurveyQuestionResponse where '
		while len(@surveyQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 set @Surveywhere = @Surveywhere + ' (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
			end
			else if(@counter != 1)
			begin
				 set @Surveywhere = @Surveywhere + ' and (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
			end

		  set @surveyQuestionIds = stuff(@surveyQuestionIds, 1, charindex(',', @surveyQuestionIds +','), '')
		  set @surveyAnswerIds = stuff(@surveyAnswerIds, 1, charindex(',', @surveyAnswerIds +','), '')
		 
		end
		set @where = @where + ' and SMDUser.Id in (' + @Surveywhere + ')'
	end

--select (@query + @where)
exec(@query + @where)

END

 -- ============================= updated on server =============================

 -- by Baqer on 29-Dec
 alter table products
 add ProductCode nvarchar(50) NULL 



 
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 29-Dec-15 2:52:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tax](
	[TaxId] [int] IDENTITY(1,1) NOT NULL,
	[TaxName] [nvarchar](200) NULL,
	[TaxValue] [float] NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED 
(
	[TaxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Tax]  WITH CHECK ADD  CONSTRAINT [FK_Tax_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryID])
GO

ALTER TABLE [dbo].[Tax] CHECK CONSTRAINT [FK_Tax_Country]
GO


 -- ============================= updated on server 20151230 =============================

 BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.Products.ClausePrice', N'Tmp_AgeClausePrice', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Products.Tmp_AgeClausePrice', N'AgeClausePrice', 'COLUMN' 
GO
ALTER TABLE dbo.Products ADD
	GenderClausePrice float(53) NULL,
	LocationClausePrice float(53) NULL,
	OtherClausePrice float(53) NULL,
	ProfessionClausePrice float(53) NULL,
	EducationClausePrice float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Education](
	[EducationId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[EducationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Education SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	EducationId bigint NULL
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Education FOREIGN KEY
	(
	EducationId
	) REFERENCES dbo.Education
	(
	EducationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Education SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD
	EducationID bigint NULL
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD CONSTRAINT
	FK_AdCampaignTargetCriteria_AdCampaignTargetCriteria FOREIGN KEY
	(
	CriteriaID
	) REFERENCES dbo.AdCampaignTargetCriteria
	(
	CriteriaID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD CONSTRAINT
	FK_AdCampaignTargetCriteria_Education FOREIGN KEY
	(
	EducationID
	) REFERENCES dbo.Education
	(
	EducationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Education SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD
	EducationID bigint NULL
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_Education FOREIGN KEY
	(
	EducationID
	) REFERENCES dbo.Education
	(
	EducationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




 -- ============================= updated on server 20151230 =============================

 GO
 ALTER PROCEDURE [dbo].[GetAds] 

	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	-- newly added 
	@FromRow int =0           ,
	@ToRow int =0

AS
BEGIN
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT

        -- Setting local variables
		   SELECT @age = age FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SELECT @countryId = CountryID FROM AspNetUsers where id=@UserID
		   SELECT @cityId = CityID FROM AspNetUsers where id=@UserID


		   select MyCampaign.CampaignID, MyCampaign.CampaignName, MyCampaign.Description, MyCampaign.VerifyQuestion, MyCampaign.LandingPageVideoLink ,
		   MyCampaign.Answer1, MyCampaign.Answer2, MyCampaign.Answer3, MyCampaign.CorrectAnswer , MyCampaign.ClickRate, MyCampaign.Type as AdType
		   from AdCampaign MyCampaign

		 --  where ( 
		 --   (MyCampaign.AgeRangeEnd >= @age and  @age >= MyCampaign.AgeRangeStart) 
			-- and
			--(MyCampaign.Gender= @gender)
			-- and
			--(MyCampaign.EndDateTime >= GETDATE() and GETDATE() >= MyCampaign.StartDateTime ) 
			-- and
			--(MyCampaign.Approved = 1) 
		 -- 	 and
			--(MyCampaign.Status = 6)   
			-- and 
			--((select count(*) from AdCampaignResponse MyCampaignResponse
			-- where MyCampaignResponse.UserID=@UserID and MyCampaignResponse.CampaignID = MyCampaign.CampaignID) = 0) 
			-- and
		 --   (MyCampaign.MaxBudget > MyCampaign.AmountSpent) 
			-- and
			--(MyCampaign.LanguageID=@languageId) 
			-- and
			-- ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			-- where MyCampaignLoc.CampaignID=MyCampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
			-- MyCampaignLoc.CityID=@cityId) > 0 
			-- and 
			-- ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			-- where MyCampaignCrit.CampaignID = MyCampaign.CampaignID and 
			-- MyCampaignCrit.LanguageID=@languageId and MyCampaignCrit.IndustryID=@industryId) > 0 )) 
		 --  )

		 --  -- newly added for paging 
		 --  order by MyCampaign.ApprovalDateTime
		 --  OFFSET @FromRow ROWS -- skip 10 rows
		 --  FETCH NEXT @ToRow ROWS ONLY; -- take 10 rows
END





/****** Object:  StoredProcedure [dbo].[GetAudience]    Script Date: 12/30/2015 3:12:29 PM ******/
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
		 @cityIds as nvarchar(500),
		 @languageIds as nvarchar(500),
		 @industryIds as nvarchar(500),
		 @profileQuestionIds as nvarchar(500),
		 @profileAnswerIds as nvarchar(500),
		 @surveyQuestionIds as nvarchar(500),
		 @surveyAnswerIds as nvarchar(500),
		 @countryIdsExcluded as nvarchar(500),
		 @cityIdsExcluded as nvarchar(500),
		 @languageIdsExcluded as nvarchar(500),
		 @industryIdsExcluded as nvarchar(500)


AS
BEGIN

	Declare @counter int = 1
	Declare @query nvarchar(max)
   
    Declare @where nvarchar(max)
	

    set @where = ' where '

	set @query = 'SELECT count(*) as MatchingUsers, (select count(*) from AspNetUsers) as AllUsers FROM AspNetUsers SMDUser'
	set @where = @where +' ((SMDUser.Age >= ' + CAST(@ageFrom AS NVARCHAR(10)) + ' and SMDUser.Age <= ' + CAST(@ageTo AS NVARCHAR(10)) + ') or  SMDUser.Age is null) '
	
	if(@gender = 1)
	begin
		set @where = @where + ' and ( SMDUser.Gender in (2,3) or  SMDUser.Gender is null )'
	end
	else
	begin
		set @where = @where + 'and (SMDUser.Gender = ' + CAST(@gender AS NVARCHAR(10)) + '  or  SMDUser.Gender is null )'
	end

	if(@countryIds IS NOT NULL AND @countryIds != '')
	begin
		set @where = @where + ' and SMDUser.CountryID in ('+ @countryIds +')'
	end
	if(@countryIdsExcluded IS NOT NULL AND @countryIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.CountryID not in ('+ @countryIdsExcluded +')'
	end
	if(@cityIds IS NOT NULL AND @cityIds != '')
	begin
		set @where = @where + ' and SMDUser.CityID in ('+ @cityIds +')'
	end
	if(@cityIdsExcluded IS NOT NULL AND @cityIdsExcluded != '')
	begin
		set @where = @where + ' and SMDUser.CityID not in ('+ @cityIdsExcluded +')'
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
	if(@profileQuestionIds IS NOT NULL AND @profileQuestionIds != '')
	begin
		
		Declare @Profilewhere nvarchar(max)
		set @Profilewhere = 'SELECT DISTINCT UserID FROM ProfileQuestionUserAnswer where '
		while len(@profileQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 set @Profilewhere = @Profilewhere + ' (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
			end
			else if(@counter != 1)
			begin
				 set @Profilewhere = @Profilewhere + ' and (PQID =' + left(@profileQuestionIds, charindex(',', @profileQuestionIds+',')-1) + ' and PQAnswerID =' + left(@profileAnswerIds, charindex(',', @profileAnswerIds +',')-1) + ')'
			end
		 
		  set @profileQuestionIds = stuff(@profileQuestionIds, 1, charindex(',', @profileQuestionIds+','), '')
		  set @profileAnswerIds = stuff(@profileAnswerIds, 1, charindex(',', @profileAnswerIds +','), '')
		end
		set @where = @where + ' and SMDUser.Id in (' + @Profilewhere + ')'
	end
	if(@surveyAnswerIds IS NOT NULL AND @surveyAnswerIds != '')
	begin
		set @counter = 1;
		Declare @Surveywhere nvarchar(max)
		set @Surveywhere = 'SELECT DISTINCT UserID FROM SurveyQuestionResponse where '
		while len(@surveyQuestionIds) > 0
		begin
		 
			if(@counter = 1)
			begin
				 set @counter = @counter + 1;
				 set @Surveywhere = @Surveywhere + ' (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
			end
			else if(@counter != 1)
			begin
				 set @Surveywhere = @Surveywhere + ' and (SQID =' + left(@surveyQuestionIds, charindex(',', @surveyQuestionIds +',')-1) + ' and UserSelection =' + left(@surveyAnswerIds, charindex(',', @surveyAnswerIds +',')-1) + ')'
			end

		  set @surveyQuestionIds = stuff(@surveyQuestionIds, 1, charindex(',', @surveyQuestionIds +','), '')
		  set @surveyAnswerIds = stuff(@surveyAnswerIds, 1, charindex(',', @surveyAnswerIds +','), '')
		 
		end
		set @where = @where + ' and SMDUser.Id in (' + @Surveywhere + ')'
	end

--select (@query + @where)
exec(@query + @where)

END

 

GO
 -- ============================= updated on server 20151230 =============================

/*
   Wednesday, December 30, 20153:45:40 PM
   User: smdsa
   Server: www.myprintcloud.com,9998
   Database: SMD
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Country SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_City FOREIGN KEY
	(
	CityID
	) REFERENCES dbo.City
	(
	CityID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Country FOREIGN KEY
	(
	CountryID
	) REFERENCES dbo.Country
	(
	CountryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

 -- ============================= updated on smd live server 20151230 ============================= here

 
 -- ============================= updated on smd live server 201611 =============================

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomUrl](
	[UrlId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShortUrl] [nvarchar](50) NULL,
	[ActualUrl] [nvarchar](250) NULL,
	[UserId] [bigint] NULL,
 CONSTRAINT [PK_CustomUrl] PRIMARY KEY CLUSTERED 
(
	[UrlId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	RewardType int NULL,
	Voucher1Heading nvarchar(200) NULL,
	Voucher1Description nvarchar(MAX) NULL,
	Voucher1Value nvarchar(MAX) NULL,
	Voucher2Heading nvarchar(200) NULL,
	Voucher2Description nvarchar(MAX) NULL,
	Voucher2Value nvarchar(MAX) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Game](
	[GameId] [bigint] IDENTITY(1,1) NOT NULL,
	[GameName] [nvarchar](200) NULL,
	[Status] [bit] NULL,
	[AgeRangeStart] [int] NULL,
	[AgeRangeEnd] [int] NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion ADD
	ParentSurveyId bigint NULL,
	Priority int NULL
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionResponse ADD
	SkipCount int NULL
GO
ALTER TABLE dbo.SurveyQuestionResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignResponse ADD
	SkipCount int NULL
GO
ALTER TABLE dbo.AdCampaignResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	ProfileImage nvarchar(200) NULL,
	UserCode nvarchar(300) NULL,
	SmsCode nvarchar(50) NULL,
	WebsiteLink nvarchar(100) NULL,
	AdvertisingContact nvarchar(MAX) NULL,
	AdvertisingContactPhoneNumber nvarchar(MAX) NULL,
	AdvertisingContactEmail nvarchar(MAX) NULL
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

 
 -- ============================= updated on smd live server 201611 ============================= Jan-05_2016

 -- 06-Jan-16  by Baqer STARTS

 alter table aspnetusers
add PaypalCustomerId nvarchar (250) null

alter table aspnetusers
add GoogleWalletCustomerId nvarchar (250) null

alter table aspnetusers
add PreferredPayoutAccount int null

-- 06-Jan-16  by Baqer ENDS

/* Added by khurram - (7 Jan 2016) Starts (Updated on SMD live server) */

/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 1/7/2016 3:05:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProducts] 

	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT
DECLARE @currentDate AS DateTime

        -- Setting local variables
		   SELECT @age = age FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()

select *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' ItemType, 
	((row_number() over (order by campaignid) * 10) + 1) Weightage from adcampaign
	where (
		((@age is null) or (adcampaign.AgeRangeEnd >= @age and  @age >= adcampaign.AgeRangeStart))
		and
		((@gender is null) or (adcampaign.Gender = @gender))
		and
		((@languageId is null) or (adcampaign.LanguageId = @languageId))
		and
		(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
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
	)
	
	union
	select sqid, question, 'Survey', 
	((row_number() over (order by sqid) * 10) + 2) Weightage from surveyquestion

	union
	select pqid, question, 'Question', 
	((row_number() over (order by pqid) * 10) + 3) Weightage from profilequestion

	) as items
	order by Weightage
	OFFSET @FromRow ROWS -- skip 10 rows
	FETCH NEXT @ToRow ROWS ONLY -- take 10 rows

END
GO

/* Added by khurram - (7 Jan 2016) Ends */

-- 07-Jan-16 by Baqer STARTS

GO

/****** Object:  Table [dbo].[TransactionLog]    Script Date: 07-Jan-16 7:20:30 PM  (Updated on SMD live server) ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransactionLog](
	[TransactionLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[TxId] [bigint] NOT NULL,
	[LogDate] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[Type] [int] NOT NULL,
	[FromUser] [nvarchar](250) NULL,
	[ToUser] [nvarchar](250) NULL,
	[IsCompleted] [bit] NULL,
 CONSTRAINT [PK_TransactionLog] PRIMARY KEY CLUSTERED 
(
	[TransactionLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [FK_TransactionLog_TransactionLog] FOREIGN KEY([TxId])
REFERENCES [dbo].[Transaction] ([TxID])
GO

ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [FK_TransactionLog_TransactionLog]
GO

ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [FK_TransactionLog_TransactionLog1] FOREIGN KEY([TransactionLogId])
REFERENCES [dbo].[TransactionLog] ([TransactionLogId])
GO

ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [FK_TransactionLog_TransactionLog1]
GO
-- 07-Jan-16  by Baqer ENDS



-- 08-Jan-16 by iqra   STARTS   updated on live server

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Game ADD
	GameType int NULL,
	Complexity int NULL,
	GameUrl nvarchar(200) NULL
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	Voucher1ImagePath nvarchar(200) NULL,
	Voucher2ImagePath nvarchar(200) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


-- 08-Jan-16 by iqra End


/* Added By Khurram (09 Jan 2016) - Starts (Updated on smd live server) */ 

GO
ALTER PROCEDURE [dbo].[GetProducts] 

	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN
DECLARE @age AS INT
DECLARE @gender AS INT
DECLARE @countryId AS INT
DECLARE @cityId AS INT
DECLARE @languageId AS INT
DECLARE @industryId AS INT
DECLARE @currentDate AS DateTime

        -- Setting local variables
		   SELECT @age = age FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()

select *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 4  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 4
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqAnswer2Id, NULL as PqAnswer2,
	NULL as PqAnswer3Id, NULL as PqAnswer3,
	((row_number() over (order by campaignid) * 10) + 1) Weightage from adcampaign
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
		(adcampaign.Status = 6)
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
	)
	
	union
	select sqid, question, 'Survey', 
	Description, Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, LeftPicturePath as SqLeftImagePath, RightPicturePath as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	((row_number() over (order by sqid) * 10) + 2) Weightage from surveyquestion
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @UserID and mySurveyQuestionResponse.SQID = surveyQuestion.SQID) = 0)

	union
	select pq.pqid, pq.question, 'Question', 
	NULL, pq.Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1,
	pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2,
	pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3,
	((row_number() over (order by pq.pqid) * 10) + 3) Weightage 
	from profilequestion pq
	outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryid) = 0)
		  or 	 
		  ((select datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryid) > 0 )	
		)
	)
	
	) as items
	order by Weightage
	OFFSET @FromRow ROWS -- skip 10 rows
	FETCH NEXT @ToRow ROWS ONLY -- take 10 rows
END
GO

/* Added By Khurram (09 Jan 2016) - Ends */


/* Added By Khurram (11 Jan 2016) - Starts (Updated on smd live server) */

GO

alter table adCampaignResponse
add UserSelection int null

GO

GO

  insert into systemMails
  values
  (14, 'Voucher', 'Sell My Data Team', 'info@myprintcloud.com',
  'You received a voucher', 'You received a voucher ++VoucherDescription++, worth ++VoucherValue++.', NULL)

GO

/* Added By Khurram (11 Jan 2016) - Ends */

/* Added By Khurram (12 Jan 2016) - Starts (Updated on smd live server) */

GO

/****** Object:  UserDefinedFunction [dbo].[GetRootParentSurvey]    Script Date: 1/12/2016 7:19:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Khurram
-- Create date: 2016-01-12 17:37
-- Description:	Returns Root level parent for given survey question
-- Used In Get Products SP
-- =============================================
CREATE FUNCTION [dbo].[GetRootParentSurvey]
(	
	-- Add the parameters for the function here
	@sqId int
)
RETURNS TABLE 
AS
RETURN 
(

	with parentsurveyquestions(sqid, question, parentsurveyid, weightage)
	as (
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 10) + 2) as weightage
			
		from surveyQuestion sq
		where sq.parentsurveyid is null
		UNION ALL
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 10) + 2) as weightage
			from SurveyQuestion sq
			where sq.SQID = @sqId
		UNION ALL
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 10) + 2) weightage
			from SurveyQuestion sq
			join parentsurveyquestions sqs on sqs.parentsurveyid = sq.SQID
	)
	
	select psq.weightage, psq.sqid, psq.question from parentsurveyquestions psq
	join parentsurveyquestions pqs on pqs.sqid = psq.sqid
	where psq.weightage > pqs.weightage
)

GO

GO

/****** Object:  UserDefinedFunction [dbo].[GetUserSurveys]    Script Date: 1/12/2016 7:20:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, pqanswer1id, pqanswer1, pqanswer2id, pqanswer2, pqanswer3id, pqanswer3 ,weightage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', sq.LeftPicturePath as SqLeftImagePath, sq.RightPicturePath as SqRightImagePath, '', 
	NULL, '', NULL, '', NULL, '', -- PQAnswers
	((row_number() over (order by sq.sqid) * 10) + 2) Weightage
	from surveyquestion sq
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)

	and sq.ParentSurveyId is null
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, sqp.LeftPicturePath as SqLeftImagePath, sqp.RightPicturePath as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	(select weightage from [GetRootParentSurvey](sq.SQID)) as weightage
	from surveyquestion sq
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
    ) 

	select * from surveyquestions
)

GO

GO

alter table aspnetusers
drop column age

alter table aspnetusers
add DOB datetime null

GO

/* Added By Khurram (12 Jan 2016) - Ends */

/* Added By Khurram (13 Jan 2016) - Starts (Updated on live db server) */

GO
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveys]    Script Date: 1/13/2016 4:04:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, pqanswer1id, pqanswer1, pqanswer2id, pqanswer2, pqanswer3id, pqanswer3,
 pqanswer4id, pqanswer4, pqanswer5id, pqanswer5, pqanswer6id, pqanswer6 ,weightage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', sq.LeftPicturePath as SqLeftImagePath, sq.RightPicturePath as SqRightImagePath, '', 
	NULL, '', NULL, '', NULL, '',NULL, '', NULL, '', NULL, '', -- PQAnswers
	((row_number() over (order by sq.sqid) * 10) + 2) Weightage
	from surveyquestion sq
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)

	and sq.ParentSurveyId is null
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, sqp.LeftPicturePath as SqLeftImagePath, sqp.RightPicturePath as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	(select weightage from [GetRootParentSurvey](sq.SQID)) as weightage
	from surveyquestion sq
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
    ) 

	select * from surveyquestions
)
GO

GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 1/13/2016 3:34:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetProducts] 

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

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 4  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 4
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqAnswer2Id, NULL as PqAnswer2,
	NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqAnswer4Id, NULL as PqAnswer4,
	NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqAnswer6Id, NULL as PqAnswer6,
	((row_number() over (order by campaignid) * 10) + 1) Weightage from adcampaign
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
		(adcampaign.Status = 6)
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
	)
	
	union
	select * from [GetUserSurveys](@UserID)

	union
	select pq.pqid, pq.question, 'Question', 
	NULL, pq.Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1,
	pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2,
	pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3,
	pqa4.PQAnswerID as PQAnswerID4, pqa4.AnswerString as PQAnswer4,
	pqa5.PQAnswerID as PQAnswerID5, pqa5.AnswerString as PQAnswer5,
	pqa6.PQAnswerID as PQAnswerID6, pqa6.AnswerString as PQAnswer6,
	((row_number() over (order by pq.pqid) * 10) + 3) Weightage 
	from profilequestion pq
	outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryid) = 0)
		  or 	 
		  ((select datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryid) > 0 )	
		)
	)
	
	) as items
	order by Weightage
	OFFSET @FromRow ROWS -- skip 10 rows
	FETCH NEXT @ToRow ROWS ONLY -- take 10 rows
END

/* Added By Khurram (13 Jan 2016) - Ends */

/* Added By Khurram (14 Jan 2016) - Start (Updated on live db server) */
GO

ALTER FUNCTION [dbo].[GetRootParentSurvey]
(	
	-- Add the parameters for the function here
	@sqId int
)
RETURNS TABLE 
AS
RETURN 
(

	with parentsurveyquestions(sqid, question, parentsurveyid, weightage)
	as (
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 10) + 2) as weightage
						
		from surveyQuestion sq
		where sq.parentsurveyid is null
		UNION ALL
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 100) + 3) as weightage
			from SurveyQuestion sq
			where sq.SQID = 17
		UNION ALL
		select sq.sqid, sq.question,
			sq.ParentSurveyId,
			((ROW_NUMBER() over (order by sq.sqid) * 100000) + 4) weightage
			from SurveyQuestion sq
			join parentsurveyquestions sqs on sqs.parentsurveyid = sq.SQID
	)
	
	select sq.sqid, sq.weightage
	from parentsurveyquestions sq
	join parentsurveyquestions pqs on pqs.sqid = sq.sqid 
	where sq.weightage < pqs.weightage
)

GO

/* Added By Khurram (14 Jan 2016) - Ends */

/* Added By Khurram (18 Jan 2016, 19 Jan 2016) - Start (Updated on live db server) */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khurram
-- Create date: 2016-01-18 15:23
-- Description:	% of users selected left image and right image
-- =============================================
CREATE FUNCTION GetUserSurveySelectionPercentage
(
	-- Add the parameters for the function here
	@sqId int = 0
)
RETURNS 
@SurveySelectionPercentage TABLE
(
	-- Add the column definitions for the TABLE variable here
	leftImagePercentage float null, 
	rightImagePercentage float null
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @surveyResponses float
	SELECT @surveyResponses = count(*) from SurveyQuestionResponse where SQID = @sqId

	-- Add the SELECT statement with parameter references here
	insert into @SurveySelectionPercentage
	values
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		((count(*) / @surveyResponses) * 100)
	END as leftImagePercentage
	  from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 1),
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		((count(*) / @surveyResponses) * 100)
	END as rightImagePercentage
	 from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 2)))

	RETURN 
END
GO

GO
ALTER FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, pqanswer1id, pqanswer1, pqanswer2id, pqanswer2, pqanswer3id, pqanswer3,
 pqanswer4id, pqanswer4, pqanswer5id, pqanswer5, pqanswer6id, pqanswer6 ,weightage, sqleftImagePercentage,
 sqRightImagePercentage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', 
	CASE
		WHEN sq.LeftPicturePath is null or sq.LeftPicturePath = ''
		THEN sq.LeftPicturePath
		WHEN sq.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sq.RightPicturePath is null or sq.RightPicturePath = ''
		THEN sq.RightPicturePath
		WHEN sq.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, '', NULL, '',NULL, '', NULL, '', NULL, '', -- PQAnswers
	(((row_number() over (order by sq.sqid) * 10) + 2) + ISNULL(sq.priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	(((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
	)
	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, 
	CASE
		WHEN sqp.LeftPicturePath is null or sqp.LeftPicturePath = ''
		THEN sqp.LeftPicturePath
		WHEN sqp.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sqp.RightPicturePath is null or sqp.RightPicturePath = ''
		THEN sqp.RightPicturePath
		WHEN sqp.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.RightPicturePath
	END as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	(select (ISNULL(weightage, 0) + ISNULL(sq.priority,0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
		)
	  and
	  sq.Status = 3 -- live
    )
  ) 

	select * from surveyquestions
)
GO

/****** Object:  UserDefinedFunction [dbo].[GetUserProfileQuestions]    Script Date: 1/20/2016 12:07:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Khurram
-- Create date: 2016-01-15 17:17
-- Description:	Return Profile Questions for user having
-- Linked ones coming as a batch
-- =============================================
CREATE FUNCTION [dbo].[GetUserProfileQuestions] 
(	
	-- Add the parameters for the function here
	@UserID uniqueidentifier = '', 
	@countryId int = 0
)
RETURNS TABLE 
AS
RETURN 
(

	-- Add the SELECT statement with parameter references here
	with profilequestions(pqid, question, linkedquestion1id, linkedquestion2id,
linkedquestion3id, linkedquestion4id, linkedquestion5id, linkedquestion6id,
linkedquestion7id, linkedquestion8id,
linkedquestion9id, linkedquestion10id, linkedquestion11id, linkedquestion12id, refreshtime,
countryid, status, type, rowNumber, weightage)
as
( 
	select pqo.pqid, pqo.question, pql.LinkedQuestion1ID, pql.LinkedQuestion2ID,
	pql2.LinkedQuestion1ID, pql2.LinkedQuestion2ID, pql3.LinkedQuestion1ID, pql3.LinkedQuestion2ID,
	pql4.LinkedQuestion1ID, pql4.LinkedQuestion2ID,pql5.LinkedQuestion1ID, pql5.LinkedQuestion2ID,
	pql6.LinkedQuestion1ID, pql6.LinkedQuestion2ID,
	pqo.refreshtime, pqo.CountryID, pqo.status, pqo.type,
	((row_number() over (order by pqo.pqid)) + isNUll(pqo.Priority,0)) rowNumber,
	(((row_number() over (order by pqo.pqid) * 10) + 3) + isNull(pqo.Priority,0)) Weightage 
	from ProfileQuestion pqo
	outer apply
	(
		select top 1 pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
	) as pql
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 1 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql2
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 2 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql3
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 3 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql4
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 4 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql5
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 5 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql6
	where pqo.HasLinkedQuestions = 1 and pqo.Status = 1
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	pq.RefreshTime, pq.CountryID, pq.status, pq.type,
	pqs.rowNumber + isnull(pq.Priority, 0),
	pqs.weightage + isnull(pq.Priority, 0) Weightage 
	from profilequestion pq
	join profilequestions pqs on 
	pqs.linkedquestion1id = pq.PQID or pqs.linkedquestion2id = pq.PQID or pqs.linkedquestion3id = pq.PQID
	or pqs.linkedquestion4id = pq.PQID or pqs.linkedquestion5id = pq.PQID or pqs.linkedquestion6id = pq.PQID
	or pqs.linkedquestion7id = pq.PQID or pqs.linkedquestion8id = pq.PQID or pqs.linkedquestion9id = pq.PQID
	or pqs.linkedquestion10id = pq.PQID or pqs.linkedquestion11id = pq.PQID or pqs.linkedquestion12id = pq.PQID
)

select pq.*,
pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1,
	pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2,
	pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3,
	pqa4.PQAnswerID as PQAnswerID4, pqa4.AnswerString as PQAnswer4,
	pqa5.PQAnswerID as PQAnswerID5, pqa5.AnswerString as PQAnswer5,
	pqa6.PQAnswerID as PQAnswerID6, pqa6.AnswerString as PQAnswer6
from profilequestions pq
outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId 
			order by pqu.pquanswerid) > 0 )	
		)
		and pq.status = 1
	)
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	pq.refreshtime, pq.countryid, pq.Status, pq.type,
	(select max(rownumber) + 1 from profilequestions) rowNumber,
	(select (((max(rowNumber) + 1) * 10) + 3) from profilequestions) Weightage,
	pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1,
	pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2,
	pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3,
	pqa4.PQAnswerID as PQAnswerID4, pqa4.AnswerString as PQAnswer4,
	pqa5.PQAnswerID as PQAnswerID5, pqa5.AnswerString as PQAnswer5,
	pqa6.PQAnswerID as PQAnswerID6, pqa6.AnswerString as PQAnswer6
	from profilequestion pq
	outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where pq.PQID not in
	(select pqid from profilequestions)
	and
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId 
			order by pqu.pquanswerid) > 0 )	
		)
	)
	and
	pq.Status = 1
		
	
)
GO

GO
ALTER PROCEDURE [dbo].[GetProducts] 

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

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select top 10 *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 3  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 3
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqAnswer2Id, NULL as PqAnswer2,
	NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqAnswer4Id, NULL as PqAnswer4,
	NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqAnswer6Id, NULL as PqAnswer6,
	((row_number() over (order by campaignid) * 10) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage  
	from adcampaign
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
			adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) = 0)
		 or
		 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
			where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
			is null)
		)
	)
	
	union
	select * from [GetUserSurveys](@UserID)

	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQAnswerID2, PQAnswer2,
	PQAnswerID3, PQAnswer3, PQAnswerID4, PQAnswer4,
	PQAnswerID5, PQAnswer5, PQAnswerID6, PQAnswer6,
	Weightage, NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage  
	from [GetUserProfileQuestions](@UserID, @countryId)
	
	) as items
	order by Weightage
END
GO


/* Added By Khurram (18 Jan 2016, 19 Jan 2016) - End */

/* Added By Khurram (20 Jan 2016) - Start (Updated on live db server) */
GO
ALTER PROCEDURE [dbo].[GetProducts] 

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

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select top 10 *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 3  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 3
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqAnswer2Id, NULL as PqAnswer2,
	NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqAnswer4Id, NULL as PqAnswer4,
	NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqAnswer6Id, NULL as PqAnswer6,
	((row_number() over (order by campaignid) * 10) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
	(select 
	CASE
		WHEN usr.ProfileImage is null or usr.ProfileImage = ''
		THEN usr.ProfileImage
		WHEN usr.ProfileImage is not null
		THEN 'http://manage.cash4ads.com/' + usr.ProfileImage
	END as AdvertisersLogoPath from AspNetUsers usr where usr.Id = UserID) as AdvertisersLogoPath 
	from adcampaign
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
			adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) = 0)
		 or
		 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
			where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
			is null)
		)
	)
	
	union
	select *, NULL as AdvertisersLogoPath  from [GetUserSurveys](@UserID)

	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQAnswerID2, PQAnswer2,
	PQAnswerID3, PQAnswer3, PQAnswerID4, PQAnswer4,
	PQAnswerID5, PQAnswer5, PQAnswerID6, PQAnswer6,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	NULL as AdvertisersLogoPath 
	from [GetUserProfileQuestions](@UserID, @countryId)
	
	) as items
	order by Weightage
END
GO
/* Added By Khurram (20 Jan 2016) - End */

/* Added By Khurram (21 Jan 2016) - Start (Updated on live db server) */

GO
delete  from adcampaignresponse

alter table adcampaignresponse
drop PK_AdCampaignResponse

alter table adcampaignresponse
drop column ResponseID

alter table adcampaignresponse
add ResponseID int identity(1,1) primary key
GO

/* Added By Khurram (21 Jan 2016) - End */

/* Added By Khurram (27 Jan 2016) - Start */

GO

alter table account
drop column accountbalance

alter table account
add AccountBalance float null

GO

GO

ALTER FUNCTION [dbo].[GetUserProfileQuestions] 
(	
	-- Add the parameters for the function here
	@UserID uniqueidentifier = '', 
	@countryId int = 0
)
RETURNS TABLE 
AS
RETURN 
(
  -- Add the SELECT statement with parameter references here
  with profilequestions(pqid, question, linkedquestion1id, linkedquestion2id,
linkedquestion3id, linkedquestion4id, linkedquestion5id, linkedquestion6id,
linkedquestion7id, linkedquestion8id,
linkedquestion9id, linkedquestion10id, linkedquestion11id, linkedquestion12id, refreshtime,
countryid, status, type, rowNumber, weightage)
as
( 
	select pqo.pqid, pqo.question, pql.LinkedQuestion1ID, pql.LinkedQuestion2ID,
	pql2.LinkedQuestion1ID, pql2.LinkedQuestion2ID, pql3.LinkedQuestion1ID, pql3.LinkedQuestion2ID,
	pql4.LinkedQuestion1ID, pql4.LinkedQuestion2ID,pql5.LinkedQuestion1ID, pql5.LinkedQuestion2ID,
	pql6.LinkedQuestion1ID, pql6.LinkedQuestion2ID,
	pqo.refreshtime, pqo.CountryID, pqo.status, pqo.type,
	((row_number() over (order by pqo.pqid)) + isNUll(pqo.Priority,0)) rowNumber,
	(((row_number() over (order by pqo.pqid) * 10) + 3) + isNull(pqo.Priority,0)) Weightage 
	from ProfileQuestion pqo
	outer apply
	(
		select top 1 pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
	) as pql
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 1 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql2
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 2 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql3
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 3 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql4
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 4 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql5
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by pq.PQID
		OFFSET 5 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql6
	where pqo.HasLinkedQuestions = 1 and pqo.Status = 1
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	pq.RefreshTime, pq.CountryID, pq.status, pq.type,
	pqs.rowNumber + isnull(pq.Priority, 0),
	pqs.weightage + isnull(pq.Priority, 0) Weightage 
	from profilequestion pq
	join profilequestions pqs on 
	pqs.linkedquestion1id = pq.PQID or pqs.linkedquestion2id = pq.PQID or pqs.linkedquestion3id = pq.PQID
	or pqs.linkedquestion4id = pq.PQID or pqs.linkedquestion5id = pq.PQID or pqs.linkedquestion6id = pq.PQID
	or pqs.linkedquestion7id = pq.PQID or pqs.linkedquestion8id = pq.PQID or pqs.linkedquestion9id = pq.PQID
	or pqs.linkedquestion10id = pq.PQID or pqs.linkedquestion11id = pq.PQID or pqs.linkedquestion12id = pq.PQID
)

select pq.*,
pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1, 
pqa1.LinkedQuestion1ID as PQA1LinkedQ1, pqa1.LinkedQuestion2ID PQA1LinkedQ2,
pqa1.type as PQA1Type, pqa1.SortOrder as PQA1SortOrder,
CASE
	WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
	THEN pqa1.ImagePath
	WHEN pqa1.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
END as PQA1ImagePath,
pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2, 
pqa2.LinkedQuestion1ID as PQA2LinkedQ1, pqa2.LinkedQuestion2ID PQA2LinkedQ2,
pqa2.type as PQA2Type, pqa2.SortOrder as PQA2SortOrder,
CASE
	WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
	THEN pqa2.ImagePath
	WHEN pqa2.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
END as PQA2ImagePath,
pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3, 
pqa3.LinkedQuestion1ID as PQA3LinkedQ1, pqa3.LinkedQuestion2ID PQA3LinkedQ2,
pqa3.type as PQA3Type, pqa3.SortOrder as PQA3SortOrder,
CASE
	WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
	THEN pqa3.ImagePath
	WHEN pqa3.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
END as PQA3ImagePath,
pqa4.PQAnswerID as PQAnswerID4, pqa4.AnswerString as PQAnswer4, 
pqa4.LinkedQuestion1ID as PQA4LinkedQ1, pqa4.LinkedQuestion2ID PQA4LinkedQ2,
pqa4.type as PQA4Type, pqa4.SortOrder as PQA4SortOrder,
CASE
	WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
	THEN pqa4.ImagePath
	WHEN pqa4.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
END as PQA4ImagePath,
pqa5.PQAnswerID as PQAnswerID5, pqa5.AnswerString as PQAnswer5, 
pqa5.LinkedQuestion1ID as PQA5LinkedQ1, pqa5.LinkedQuestion2ID PQA5LinkedQ2,
pqa5.type as PQA5Type, pqa5.SortOrder as PQA5SortOrder,
CASE
	WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
	THEN pqa5.ImagePath
	WHEN pqa5.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
END as PQA5ImagePath,
pqa6.PQAnswerID as PQAnswerID6, pqa6.AnswerString as PQAnswer6,
pqa6.LinkedQuestion1ID as PQA6LinkedQ1, pqa1.LinkedQuestion2ID PQA6LinkedQ2, 
pqa6.type as PQA6Type, pqa6.SortOrder as PQA6SortOrder,
CASE
	WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
	THEN pqa6.ImagePath
	WHEN pqa6.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
END as PQA6ImagePath
from profilequestions pq
outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID, pqa.type, 
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.type, 
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.type, 
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId 
			order by pqu.pquanswerid) > 0 )	
		)
		and pq.status = 1
	)
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	pq.refreshtime, pq.countryid, pq.Status, pq.type,
	(select max(rownumber) + 1 from profilequestions) rowNumber,
	(select (((max(rowNumber) + 1) * 10) + 3) from profilequestions) Weightage,
	pqa1.PQAnswerID as PQAnswerID1, pqa1.AnswerString as PQAnswer1, 
	pqa1.LinkedQuestion1ID as PQA1LinkedQ1, pqa1.LinkedQuestion2ID PQA1LinkedQ2,
	pqa1.type as PQA1Type, pqa1.SortOrder as PQA1SortOrder,
	CASE
		WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
		THEN pqa1.ImagePath
		WHEN pqa1.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
	END as PQA1ImagePath,
	pqa2.PQAnswerID as PQAnswerID2, pqa2.AnswerString as PQAnswer2, 
	pqa2.LinkedQuestion1ID as PQA2LinkedQ1, pqa2.LinkedQuestion2ID PQA2LinkedQ2,
	pqa2.type as PQA2Type, pqa2.SortOrder as PQA2SortOrder,
	CASE
		WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
		THEN pqa2.ImagePath
		WHEN pqa2.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
	END as PQA2ImagePath,
	pqa3.PQAnswerID as PQAnswerID3, pqa3.AnswerString as PQAnswer3, 
	pqa3.LinkedQuestion1ID as PQA3LinkedQ1, pqa3.LinkedQuestion2ID PQA3LinkedQ2,
	pqa3.type as PQA3Type, pqa3.SortOrder as PQA3SortOrder,
	CASE
		WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
		THEN pqa3.ImagePath
		WHEN pqa3.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
	END as PQA3ImagePath,
	pqa4.PQAnswerID as PQAnswerID4, pqa4.AnswerString as PQAnswer4, 
	pqa4.LinkedQuestion1ID as PQA4LinkedQ1, pqa4.LinkedQuestion2ID PQA4LinkedQ2,
	pqa4.type as PQA4Type, pqa4.SortOrder as PQA4SortOrder,
	CASE
		WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
		THEN pqa4.ImagePath
		WHEN pqa4.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
	END as PQA4ImagePath,
	pqa5.PQAnswerID as PQAnswerID5, pqa5.AnswerString as PQAnswer5, 
	pqa5.LinkedQuestion1ID as PQA5LinkedQ1, pqa5.LinkedQuestion2ID PQA5LinkedQ2,
	pqa5.type as PQA5Type, pqa5.SortOrder as PQA5SortOrder,
	CASE
		WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
		THEN pqa5.ImagePath
		WHEN pqa5.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
	END as PQA5ImagePath,
	pqa6.PQAnswerID as PQAnswerID6, pqa6.AnswerString as PQAnswer6,
	pqa6.LinkedQuestion1ID as PQA6LinkedQ1, pqa1.LinkedQuestion2ID PQA6LinkedQ2,
	pqa6.type as PQA6Type, pqa6.SortOrder as PQA6SortOrder,
	CASE
		WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
		THEN pqa6.ImagePath
		WHEN pqa6.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
	END as PQA6ImagePath
	from profilequestion pq
	outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID, pqa.type, 
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.type, 
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.type, 
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where pq.PQID not in
	(select pqid from profilequestions)
	and
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pq.CountryID = @countryId 
			order by pqu.pquanswerid) > 0 )	
		)
	)
	and
	pq.Status = 1
)

GO

GO

ALTER FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, 
 pqanswer1id, pqanswer1, pqa1Linkedq1, pqa1Linkedq2, pqa1Type, pqa1SortOrder, pqa1ImagePath,
 pqanswer2id, pqanswer2, pqa2Linkedq1, pqa2Linkedq2, pqa2Type, pqa2SortOrder, pqa2ImagePath,
 pqanswer3id, pqanswer3, pqa3Linkedq1, pqa3Linkedq2, pqa3Type, pqa3SortOrder, pqa3ImagePath,
 pqanswer4id, pqanswer4, pqa4Linkedq1, pqa4Linkedq2, pqa4Type, pqa4SortOrder, pqa4ImagePath,
 pqanswer5id, pqanswer5, pqa5Linkedq1, pqa5Linkedq2, pqa5Type, pqa5SortOrder, pqa5ImagePath,
 pqanswer6id, pqanswer6, pqa6Linkedq1, pqa6Linkedq2, pqa6Type, pqa6SortOrder, pqa6ImagePath,
 weightage, sqleftImagePercentage,
 sqRightImagePercentage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', 
	CASE
		WHEN sq.LeftPicturePath is null or sq.LeftPicturePath = ''
		THEN sq.LeftPicturePath
		WHEN sq.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sq.RightPicturePath is null or sq.RightPicturePath = ''
		THEN sq.RightPicturePath
		WHEN sq.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(((row_number() over (order by sq.sqid) * 10) + 2) + isnull(sq.Priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	(((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
	)
	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'',
	CASE
		WHEN sqp.LeftPicturePath is null or sqp.LeftPicturePath = ''
		THEN sqp.LeftPicturePath
		WHEN sqp.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sqp.RightPicturePath is null or sqp.RightPicturePath = ''
		THEN sqp.RightPicturePath
		WHEN sqp.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(select (isnull(weightage, 0) + isnull(sq.Priority, 0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq	
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
		)
	  and
	  sq.Status = 3 -- live
    ) 
  )
	select * from surveyquestions
)

GO

GO

ALTER PROCEDURE [dbo].[GetProducts] 

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

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select top 10 *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 3  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 3
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqA1LinkedQ1, NULL as PqA1LinkedQ2, 
	NULL as PqA1Type, NULL as PqA1SortOrder, NULL as PqA1ImagePath,
	NULL as PqAnswer2Id, NULL as PqAnswer2, NULL as PqA2LinkedQ1, NULL as PqA2LinkedQ2,
	NULL as PqA2Type, NULL as PqA2SortOrder, NULL as PqA2ImagePath,
	NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqA3LinkedQ1, NULL as PqA3LinkedQ2,
	NULL as PqA3Type, NULL as PqA3SortOrder, NULL as PqA3ImagePath, 
	NULL as PqAnswer4Id, NULL as PqAnswer4, NULL as PqA4LinkedQ1, NULL as PqA4LinkedQ2,
	NULL as PqA4Type, NULL as PqA4SortOrder, NULL as PqA4ImagePath,
	NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqA5LinkedQ1, NULL as PqA5LinkedQ2,
	NULL as PqA5Type, NULL as PqA5SortOrder, NULL as PqA5ImagePath,
	NULL as PqAnswer6Id, NULL as PqAnswer6, NULL as PqA6LinkedQ1, NULL as PqA6LinkedQ2,
	NULL as PqA6Type, NULL as PqA6SortOrder, NULL as PqA6ImagePath,
	((row_number() over (order by campaignid) * 10) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
	(select 
	CASE
		WHEN usr.ProfileImage is null or usr.ProfileImage = ''
		THEN usr.ProfileImage
		WHEN usr.ProfileImage is not null
		THEN 'http://manage.cash4ads.com/' + usr.ProfileImage
	END as AdvertisersLogoPath from AspNetUsers usr where usr.Id = UserID) as AdvertisersLogoPath 
	from adcampaign
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
			adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) = 0)
		 or
		 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
			where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
			is null)
		)
	)
	
	union
	select *, NULL as AdvertisersLogoPath from [GetUserSurveys](@UserID)

	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	NULL as AdvertisersLogoPath 
	from [GetUserProfileQuestions](@UserID, @countryId)
	
	) as items
	order by Weightage
END

GO

/* Added By Khurram (27 Jan 2016) - End */

/* Added By Khurram (29 Jan 2016) - Start */

GO

alter table TransactionLog
drop constraint FK_TransactionLog_TransactionLog1

alter table TransactionLog
add Description nvarchar(500)

GO

/* Added By Khurram (29 Jan 2016) - End */

/* Added By Khurram (02 Feb 2016) - Start */

GO

ALTER PROCEDURE [dbo].[GetProducts] 

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

        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		   SELECT @countryId = countryId FROM AspNetUsers where id=@UserID
		   SELECT @cityId = cityId FROM AspNetUsers where id=@UserID
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, campaignname ItemName, 'Ad' Type, 
    Description, Type ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, LandingPageVideoLink as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 3  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 3
		THEN
		    NULL
	END as GameUrl, 
	NULL as PqAnswer1Id, NULL as PqAnswer1, NULL as PqA1LinkedQ1, NULL as PqA1LinkedQ2, 
	NULL as PqA1Type, NULL as PqA1SortOrder, NULL as PqA1ImagePath,
	NULL as PqAnswer2Id, NULL as PqAnswer2, NULL as PqA2LinkedQ1, NULL as PqA2LinkedQ2,
	NULL as PqA2Type, NULL as PqA2SortOrder, NULL as PqA2ImagePath,
	NULL as PqAnswer3Id, NULL as PqAnswer3, NULL as PqA3LinkedQ1, NULL as PqA3LinkedQ2,
	NULL as PqA3Type, NULL as PqA3SortOrder, NULL as PqA3ImagePath, 
	NULL as PqAnswer4Id, NULL as PqAnswer4, NULL as PqA4LinkedQ1, NULL as PqA4LinkedQ2,
	NULL as PqA4Type, NULL as PqA4SortOrder, NULL as PqA4ImagePath,
	NULL as PqAnswer5Id, NULL as PqAnswer5, NULL as PqA5LinkedQ1, NULL as PqA5LinkedQ2,
	NULL as PqA5Type, NULL as PqA5SortOrder, NULL as PqA5ImagePath,
	NULL as PqAnswer6Id, NULL as PqAnswer6, NULL as PqA6LinkedQ1, NULL as PqA6LinkedQ2,
	NULL as PqA6Type, NULL as PqA6SortOrder, NULL as PqA6ImagePath,
	((row_number() over (order by campaignid) * 10) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
	(select 
	CASE
		WHEN usr.ProfileImage is null or usr.ProfileImage = ''
		THEN usr.ProfileImage
		WHEN usr.ProfileImage is not null
		THEN 'http://manage.cash4ads.com/' + usr.ProfileImage
	END as AdvertisersLogoPath from AspNetUsers usr where usr.Id = UserID) as AdvertisersLogoPath 
	from adcampaign
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
			adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) = 0)
		 or
		 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
			where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
			is null)
		)
	)
	
	union
	select *, NULL as AdvertisersLogoPath from [GetUserSurveys](@UserID)

	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	NULL as AdvertisersLogoPath 
	from [GetUserProfileQuestions](@UserID, @countryId)
	
	) as items
	order by Weightage
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
END

/* Added By Khurram (02 Feb 2016) - End */
/* added by iqra 16 feb 2-16*/
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
 VideoUrl nvarchar(1000) NULL,
 BuuyItLine1 nvarchar(MAX) NULL,
 BuyItLine2 nvarchar(MAX) NULL,
 BuyItLine3 nvarchar(MAX) NULL,
 BuyItButtonLabel nvarchar(MAX) NULL,
 BuyItImageUrl nvarchar(200) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/*executed*/


alter table adcampaignResponse 
 add UserQuestionResponse int null 
 
 /*07-04-2016*/
 alter table adcampaign add couponSmdComission nvarchar(200),
couponImage2 nvarchar(300),
CouponImage3 nvarchar(300),
CouponImage4 nvarchar(300),
CouponExpiryLabel  nvarchar(300)





/****** Object:  Table [dbo].[CouponCategory]    Script Date: 4/7/2016 9:33:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CouponCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_CouponCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[CouponCodes]    Script Date: 4/7/2016 9:34:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CouponCodes](
	[CodeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CampaignId] [bigint] NULL,
	[Code] [nvarchar](300) NULL,
	[IsTaken] [bit] NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_CouponCodes] PRIMARY KEY CLUSTERED 
(
	[CodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CouponCodes]  WITH CHECK ADD  CONSTRAINT [FK_CouponCodes_AdCampaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[AdCampaign] ([CampaignID])
GO

ALTER TABLE [dbo].[CouponCodes] CHECK CONSTRAINT [FK_CouponCodes_AdCampaign]
GO

ALTER TABLE [dbo].[CouponCodes]  WITH CHECK ADD  CONSTRAINT [FK_CouponCodes_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CouponCodes] CHECK CONSTRAINT [FK_CouponCodes_AspNetUsers]
GO



/****** Object:  Table [dbo].[CampaignCategories]    Script Date: 4/7/2016 9:35:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CampaignCategories](
	[CampaignId] [bigint] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CampaignCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CampaignCategories]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCategories_AdCampaign] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[AdCampaign] ([CampaignID])
GO

ALTER TABLE [dbo].[CampaignCategories] CHECK CONSTRAINT [FK_CampaignCategories_AdCampaign]
GO

ALTER TABLE [dbo].[CampaignCategories]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCategories_CouponCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CouponCategory] ([CategoryId])
GO

ALTER TABLE [dbo].[CampaignCategories] CHECK CONSTRAINT [FK_CampaignCategories_CouponCategory]
GO


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	CouponDiscountValue float(53) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


/**********4--11--2016************/


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products ADD
	BuyItClausePrice float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	CouponType int NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	DeliveryDays int NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products ADD
	QuizQuestionClausePrice float(53) NULL,
	TenDayDeliveryClausePrice float(53) NULL,
	FiveDayDeliveryClausePrice float(53) NULL,
	ThreeDayDeliveryClausePrice float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignTargetCriteria
	DROP CONSTRAINT FK_AdCampaignTargetCriteria_AdCampaign1
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignTargetCriteria ADD CONSTRAINT
	FK_AdCampaignTargetCriteria_AdCampaignQuiz FOREIGN KEY
	(
	QuizCampaignId
	) REFERENCES dbo.AdCampaign
	(
	CampaignID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
-----------------------------------------------22 april 2016
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveys]    Script Date: 4/22/2016 4:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
ALTER FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, 
 pqanswer1id, pqanswer1, pqa1Linkedq1, pqa1Linkedq2, pqa1Linkedq3, pqa1Linkedq4, pqa1Linkedq5, pqa1Linkedq6, pqa1Type, pqa1SortOrder, pqa1ImagePath,
 pqanswer2id, pqanswer2, pqa2Linkedq1, pqa2Linkedq2, pqa2Linkedq3, pqa2Linkedq4, pqa2Linkedq5, pqa2Linkedq6, pqa2Type, pqa2SortOrder, pqa2ImagePath,
 pqanswer3id, pqanswer3, pqa3Linkedq1, pqa3Linkedq2, pqa3Linkedq3, pqa3Linkedq4, pqa3Linkedq5, pqa3Linkedq6, pqa3Type, pqa3SortOrder, pqa3ImagePath,
 pqanswer4id, pqanswer4, pqa4Linkedq1, pqa4Linkedq2, pqa4Linkedq3, pqa4Linkedq4, pqa4Linkedq5, pqa4Linkedq6, pqa4Type, pqa4SortOrder, pqa4ImagePath,
 pqanswer5id, pqanswer5, pqa5Linkedq1, pqa5Linkedq2, pqa5Linkedq3, pqa5Linkedq4, pqa5Linkedq5, pqa5Linkedq6, pqa5Type, pqa5SortOrder, pqa5ImagePath,
 pqanswer6id, pqanswer6, pqa6Linkedq1, pqa6Linkedq2, pqa6Linkedq3, pqa6Linkedq4, pqa6Linkedq5, pqa6Linkedq6, pqa6Type, pqa6SortOrder, pqa6ImagePath,
 weightage, sqleftImagePercentage,
 sqRightImagePercentage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', 
	CASE
		WHEN sq.LeftPicturePath is null or sq.LeftPicturePath = ''
		THEN sq.LeftPicturePath
		WHEN sq.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sq.RightPicturePath is null or sq.RightPicturePath = ''
		THEN sq.RightPicturePath
		WHEN sq.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sq.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(((row_number() over (order by sq.sqid) * 10) + 2) + isnull(sq.Priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	(((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
	)
	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'',
	CASE
		WHEN sqp.LeftPicturePath is null or sqp.LeftPicturePath = ''
		THEN sqp.LeftPicturePath
		WHEN sqp.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sqp.RightPicturePath is null or sqp.RightPicturePath = ''
		THEN sqp.RightPicturePath
		WHEN sqp.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(select (isnull(weightage, 0) + isnull(sq.Priority, 0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq	
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
		)
	  and
	  sq.Status = 3 -- live
    ) 
  )
	select * from surveyquestions
)

USE [SMDv2]
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserProfileQuestions]    Script Date: 4/22/2016 4:18:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
ALTER FUNCTION [dbo].[GetUserProfileQuestions] 
(	
	-- Add the parameters for the function here
	@UserID uniqueidentifier = '', 
	@countryId int = 0
)
RETURNS TABLE 
AS
RETURN 
(
  -- Add the SELECT statement with parameter references here
  with profilequestions(pqid, question, linkedquestion1id, linkedquestion2id,
linkedquestion3id, linkedquestion4id, linkedquestion5id, linkedquestion6id,
linkedquestion7id, linkedquestion8id,
linkedquestion9id, linkedquestion10id, linkedquestion11id, linkedquestion12id,
linkedquestion13id, linkedquestion14id, linkedquestion15id, linkedquestion16id,
linkedquestion17id, linkedquestion18id, linkedquestion19id, linkedquestion20id,
linkedquestion21id, linkedquestion22id, linkedquestion23id, linkedquestion24id,
linkedquestion25id, linkedquestion26id, linkedquestion27id, linkedquestion28id,
linkedquestion29id, linkedquestion30id, linkedquestion31id, linkedquestion32id,
linkedquestion33id, linkedquestion34id, linkedquestion35id, linkedquestion36id,
 refreshtime,
countryid, status, type, rowNumber, weightage)
as
( 
	select pqo.pqid, pqo.question, pql.LinkedQuestion1ID, pql.LinkedQuestion2ID,pql.PQA1LinkedQ3,pql.PQA1LinkedQ4,
	pql.PQA1LinkedQ5,pql.PQA1LinkedQ6,pql2.LinkedQuestion1ID, pql2.LinkedQuestion2ID,pql2.PQA1LinkedQ3,pql2.PQA1LinkedQ4,
	pql2.PQA1LinkedQ5,pql2.PQA1LinkedQ6, pql3.LinkedQuestion1ID, pql3.LinkedQuestion2ID,pql3.PQA1LinkedQ3,pql3.PQA1LinkedQ4,
	pql3.PQA1LinkedQ5,pql3.PQA1LinkedQ6,pql4.LinkedQuestion1ID, pql4.LinkedQuestion2ID,pql4.PQA1LinkedQ3,pql4.PQA1LinkedQ4,
	pql4.PQA1LinkedQ5,pql4.PQA1LinkedQ6,pql5.LinkedQuestion1ID, pql5.LinkedQuestion2ID,pql5.PQA1LinkedQ3,pql5.PQA1LinkedQ4,
	pql5.PQA1LinkedQ5,pql5.PQA1LinkedQ6,pql6.LinkedQuestion1ID, pql6.LinkedQuestion2ID,pql6.PQA1LinkedQ3,pql6.PQA1LinkedQ4,
	pql6.PQA1LinkedQ5,pql6.PQA1LinkedQ6,pqo.refreshtime, pqo.CountryID, pqo.status, pqo.type,
	((row_number() over (order by pqo.pqid)) + isNUll(pqo.Priority,0)) rowNumber,
	(((row_number() over (order by pqo.pqid) * 100) + 3) + isNull(pqo.Priority,0)) Weightage 
	from ProfileQuestion pqo
	outer apply
	(
		select top 1 pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc
		order by pq.PQID
	) as pql
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc
		order by pq.PQID
		OFFSET 1 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql2
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 2 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql3
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--order by pq.PQID
		OFFSET 3 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql4
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 4 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql5
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 5 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql6
	where pqo.HasLinkedQuestions = 1 and pqo.Status = 1
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	null, null, null, null, null, null, null, null, null, null, null, null, 
	null, null, null, null, null, null, null, null, null, null, null, null, 
	pq.RefreshTime, pq.CountryID, pq.status, pq.type,
	pqs.rowNumber + isnull(pq.Priority, 0),
	pqs.weightage + isnull(pq.Priority, 0) Weightage 
	from profilequestion pq
	join profilequestions pqs on 
	(pqs.linkedquestion1id = pq.PQID or pqs.linkedquestion2id = pq.PQID or pqs.linkedquestion3id = pq.PQID
	or pqs.linkedquestion4id = pq.PQID or pqs.linkedquestion5id = pq.PQID or pqs.linkedquestion6id = pq.PQID
	or pqs.linkedquestion7id = pq.PQID or pqs.linkedquestion8id = pq.PQID or pqs.linkedquestion9id = pq.PQID
	or pqs.linkedquestion11id = pq.PQID or pqs.linkedquestion12id = pq.PQID or pqs.linkedquestion13id = pq.PQID
	or pqs.linkedquestion14id = pq.PQID or pqs.linkedquestion15id = pq.PQID or pqs.linkedquestion16id = pq.PQID
	or pqs.linkedquestion17id = pq.PQID or pqs.linkedquestion18id = pq.PQID or pqs.linkedquestion19id = pq.PQID
	or pqs.linkedquestion20id = pq.PQID or pqs.linkedquestion21id = pq.PQID or pqs.linkedquestion22id = pq.PQID
	or pqs.linkedquestion23id = pq.PQID or pqs.linkedquestion24id = pq.PQID or pqs.linkedquestion25id = pq.PQID
	or pqs.linkedquestion26id = pq.PQID or pqs.linkedquestion27id = pq.PQID or pqs.linkedquestion28id = pq.PQID
	or pqs.linkedquestion29id = pq.PQID or pqs.linkedquestion30id = pq.PQID or pqs.linkedquestion31id = pq.PQID
	or pqs.linkedquestion32id = pq.PQID or pqs.linkedquestion33id = pq.PQID or pqs.linkedquestion34id = pq.PQID
	or pqs.linkedquestion35id = pq.PQID or pqs.linkedquestion36id = pq.PQID	) 
	and pqs.pqid = pq.PQID

)

select pq.*,
pqa1.PQAnswerID as PQAnswerID1, 
pqa1.AnswerString as PQAnswer1, 
pqa1.LinkedQuestion1ID as PQA1LinkedQ1,
pqa1.LinkedQuestion2ID as PQA1LinkedQ2,
pqa1.LinkedQuestion3ID as PQA1LinkedQ3,
pqa1.LinkedQuestion4ID as PQA1LinkedQ4,
pqa1.LinkedQuestion5ID as PQA1LinkedQ5,
pqa1.LinkedQuestion6ID as PQA1LinkedQ6,
pqa1.type as PQA1Type, 
pqa1.SortOrder as PQA1SortOrder,
CASE
	WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
	THEN pqa1.ImagePath
	WHEN pqa1.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
END as PQA1ImagePath,
pqa2.PQAnswerID as PQAnswerID2, 
pqa2.AnswerString as PQAnswer2, 
pqa2.LinkedQuestion1ID as PQA2LinkedQ1, 
pqa2.LinkedQuestion2ID as PQA2LinkedQ2,
pqa2.LinkedQuestion3ID as PQA2LinkedQ3,
pqa2.LinkedQuestion4ID as PQA2LinkedQ4,
pqa2.LinkedQuestion5ID as PQA2LinkedQ5,
pqa2.LinkedQuestion6ID as PQA2LinkedQ6,
pqa2.type as PQA2Type,
 pqa2.SortOrder as PQA2SortOrder,
CASE
	WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
	THEN pqa2.ImagePath
	WHEN pqa2.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
END as PQA2ImagePath,
pqa3.PQAnswerID as PQAnswerID3, 
pqa3.AnswerString as PQAnswer3, 
pqa3.LinkedQuestion1ID as PQA3LinkedQ1, 
pqa3.LinkedQuestion2ID as PQA3LinkedQ2,
pqa3.LinkedQuestion3ID as PQA3LinkedQ3,
pqa3.LinkedQuestion4ID as PQA3LinkedQ4,
pqa3.LinkedQuestion5ID as PQA3LinkedQ5,
pqa3.LinkedQuestion6ID as PQA3LinkedQ6,
pqa3.type as PQA3Type, 
pqa3.SortOrder as PQA3SortOrder,
CASE
	WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
	THEN pqa3.ImagePath
	WHEN pqa3.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
END as PQA3ImagePath,
pqa4.PQAnswerID as PQAnswerID4, 
pqa4.AnswerString as PQAnswer4, 
pqa4.LinkedQuestion1ID as PQA4LinkedQ1, 
pqa4.LinkedQuestion2ID as PQA4LinkedQ2,
pqa4.LinkedQuestion3ID as PQA4LinkedQ3,
pqa4.LinkedQuestion4ID as PQA4LinkedQ4,
pqa4.LinkedQuestion5ID as PQA4LinkedQ5,
pqa4.LinkedQuestion6ID as PQA4LinkedQ6,
pqa4.type as PQA4Type,
 pqa4.SortOrder as PQA4SortOrder,
CASE
	WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
	THEN pqa4.ImagePath
	WHEN pqa4.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
END as PQA4ImagePath,
pqa5.PQAnswerID as PQAnswerID5, 
pqa5.AnswerString as PQAnswer5, 
pqa5.LinkedQuestion1ID as PQA5LinkedQ1,
pqa5.LinkedQuestion2ID as PQA5LinkedQ2,
pqa5.LinkedQuestion3ID as PQA5LinkedQ3,
pqa5.LinkedQuestion4ID as PQA5LinkedQ4,
pqa5.LinkedQuestion5ID as PQA5LinkedQ5,
pqa5.LinkedQuestion6ID as PQA5LinkedQ6,
pqa5.type as PQA5Type, 
pqa5.SortOrder as PQA5SortOrder,
CASE
	WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
	THEN pqa5.ImagePath
	WHEN pqa5.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
END as PQA5ImagePath,
pqa6.PQAnswerID as PQAnswerID6, 
pqa6.AnswerString as PQAnswer6,
pqa6.LinkedQuestion1ID as PQA6LinkedQ1, 
pqa6.LinkedQuestion2ID as PQA6LinkedQ2,
pqa6.LinkedQuestion3ID as PQA6LinkedQ3,
pqa6.LinkedQuestion4ID as PQA6LinkedQ4,
pqa6.LinkedQuestion5ID as PQA6LinkedQ5,
pqa6.LinkedQuestion6ID as PQA6LinkedQ6, 
pqa6.type as PQA6Type,
 pqa6.SortOrder as PQA6SortOrder,
CASE
	WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
	THEN pqa6.ImagePath
	WHEN pqa6.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
END as PQA6ImagePath
from profilequestions pq
outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,pqa.LinkedQuestion3ID,pqa.LinkedQuestion4ID,pqa.LinkedQuestion5ID,pqa.LinkedQuestion6ID,
		pqa.type, 
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID and pqa.Status != 0
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.LinkedQuestion3ID,pqa2.LinkedQuestion4ID,pqa2.LinkedQuestion5ID,pqa2.LinkedQuestion6ID,
		pqa2.type, 
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID  and pqa2.Status != 0
		--order by ISNULL( pqa2.SortOrder, 99999) asc--order by pqa2.SortOrder asc,pqa2.AnswerString asc--
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.LinkedQuestion3ID,pqa3.LinkedQuestion4ID,pqa3.LinkedQuestion5ID,pqa3.LinkedQuestion6ID,
		 pqa3.type, 
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID  and pqa3.Status != 0
		--order by ISNULL( pqa3.SortOrder, 99999) asc--order by pqa3.SortOrder asc,pqa3.AnswerString asc--
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.LinkedQuestion3ID,pqa4.LinkedQuestion4ID,pqa4.LinkedQuestion5ID,pqa4.LinkedQuestion6ID,
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID  and pqa4.Status != 0
		--order by ISNULL( pqa4.SortOrder, 99999) asc--order by pqa4.SortOrder asc,pqa4.AnswerString asc--
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.LinkedQuestion3ID,pqa5.LinkedQuestion4ID,pqa5.LinkedQuestion5ID,pqa5.LinkedQuestion6ID,
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID  and pqa5.Status != 0
		--order by ISNULL( pqa5.SortOrder, 99999) asc--order by pqa5.SortOrder asc,pqa5.AnswerString asc--
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.LinkedQuestion3ID,pqa6.LinkedQuestion4ID,pqa6.LinkedQuestion5ID,pqa6.LinkedQuestion6ID,
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID  and pqa6.Status != 0
		--order by ISNULL( pqa6.SortOrder, 99999) asc--order by pqa6.SortOrder asc,pqa6.AnswerString asc--
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID )=0)--and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID --and pq.CountryID = @countryId -- commented because no need for country
			order by pqu.pquanswerid) > 0 )	
		)
		and pq.status = 1
	)
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null,  null, null,
	null, null, null, null,
	null, null,
	null, null, null, null,  null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	--null, null, null, null,  null, null,null, null, null, null,  null, null,
	--null, null, null, null,  null, null,null, null, null, null,  null, null,
	pq.refreshtime,
	 pq.countryid,
	 pq.Status,
	 pq.type,
	((select max(rownumber) + 1 from profilequestions))+(row_number() over (order by pq.pqid)) rowNumber,
	((select (((max(rowNumber) + 1) ) ) from profilequestions)+(row_number() over (order by pq.pqid))*100)+3 Weightage,
	pqa1.PQAnswerID as PQAnswerID1, 
	pqa1.AnswerString as PQAnswer1, 
	pqa1.LinkedQuestion1ID as PQA1LinkedQ1, 
	pqa1.LinkedQuestion2ID as PQA1LinkedQ2,
	pqa1.LinkedQuestion3ID as PQA1LinkedQ3,
	pqa1.LinkedQuestion4ID as PQA1LinkedQ4,
	pqa1.LinkedQuestion5ID as PQA1LinkedQ5,
	pqa1.LinkedQuestion6ID as PQA1LinkedQ6,
	pqa1.type as PQA1Type, 
	pqa1.SortOrder as PQA1SortOrder,
	CASE
		WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
		THEN pqa1.ImagePath
		WHEN pqa1.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
	END as PQA1ImagePath,
	pqa2.PQAnswerID as PQAnswerID2,
	 pqa2.AnswerString as PQAnswer2, 
	pqa2.LinkedQuestion1ID as PQA2LinkedQ1, 
	pqa2.LinkedQuestion2ID as PQA2LinkedQ2,
	pqa2.LinkedQuestion3ID as PQA2LinkedQ3,
	pqa2.LinkedQuestion4ID as PQA2LinkedQ4,
	pqa2.LinkedQuestion5ID as PQA2LinkedQ5,
	pqa2.LinkedQuestion6ID as PQA2LinkedQ6,
	pqa2.type as PQA2Type,
	 pqa2.SortOrder as PQA2SortOrder,
	CASE
		WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
		THEN pqa2.ImagePath
		WHEN pqa2.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
	END as PQA2ImagePath,
	pqa3.PQAnswerID as PQAnswerID3, 
	pqa3.AnswerString as PQAnswer3, 
	pqa3.LinkedQuestion1ID as PQA3LinkedQ1, 
	pqa3.LinkedQuestion2ID as PQA3LinkedQ2,
	pqa3.LinkedQuestion3ID as PQA3LinkedQ3,
	pqa3.LinkedQuestion4ID as PQA3LinkedQ4,
	pqa3.LinkedQuestion5ID as PQA3LinkedQ5,
	pqa3.LinkedQuestion6ID as PQA3LinkedQ6,
	pqa3.type as PQA3Type, 
	pqa3.SortOrder as PQA3SortOrder,
	CASE
		WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
		THEN pqa3.ImagePath
		WHEN pqa3.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
	END as PQA3ImagePath,
	pqa4.PQAnswerID as PQAnswerID4, 
	pqa4.AnswerString as PQAnswer4, 
	pqa4.LinkedQuestion1ID as PQA4LinkedQ1,
	 pqa4.LinkedQuestion2ID as PQA4LinkedQ2,
	pqa4.LinkedQuestion3ID as PQA4LinkedQ3,
	pqa4.LinkedQuestion4ID as PQA4LinkedQ4,
	pqa4.LinkedQuestion5ID as PQA4LinkedQ5,
	pqa4.LinkedQuestion6ID as PQA4LinkedQ6,
	pqa4.type as PQA4Type, 
	pqa4.SortOrder as PQA4SortOrder,
	CASE
		WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
		THEN pqa4.ImagePath
		WHEN pqa4.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
	END as PQA4ImagePath,
	pqa5.PQAnswerID as PQAnswerID5, 
	pqa5.AnswerString as PQAnswer5, 
	pqa5.LinkedQuestion1ID as PQA5LinkedQ1, 
	pqa5.LinkedQuestion2ID as PQA5LinkedQ2,
	pqa5.LinkedQuestion3ID as PQA5LinkedQ3,
	pqa5.LinkedQuestion4ID as PQA5LinkedQ4,
	pqa5.LinkedQuestion5ID as PQA5LinkedQ5,
	pqa5.LinkedQuestion6ID as PQA5LinkedQ6,
	pqa5.type as PQA5Type, 
	pqa5.SortOrder as PQA5SortOrder,
	CASE
		WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
		THEN pqa5.ImagePath
		WHEN pqa5.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
	END as PQA5ImagePath,
	pqa6.PQAnswerID as PQAnswerID6, 
	pqa6.AnswerString as PQAnswer6,
	pqa6.LinkedQuestion1ID as PQA6LinkedQ1, 
	pqa1.LinkedQuestion2ID PQA6LinkedQ2,
	pqa6.LinkedQuestion3ID as PQA6LinkedQ3,
	pqa6.LinkedQuestion4ID as PQA6LinkedQ4,
	pqa6.LinkedQuestion5ID as PQA6LinkedQ5,
	pqa6.LinkedQuestion6ID as PQA6LinkedQ6, 
	pqa6.type as PQA6Type, 
	pqa6.SortOrder as PQA6SortOrder,
	CASE
		WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
		THEN pqa6.ImagePath
		WHEN pqa6.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
	END as PQA6ImagePath
	from profilequestion pq 
	
		outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID, pqa.type
		,pqa.LinkedQuestion3ID,pqa.LinkedQuestion4ID,pqa.LinkedQuestion5ID,pqa.LinkedQuestion6ID,
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.type
		,pqa2.LinkedQuestion3ID,pqa2.LinkedQuestion4ID,pqa2.LinkedQuestion5ID,pqa2.LinkedQuestion6ID,
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		--order by ISNULL( pqa2.SortOrder, 99999) asc--order by pqa2.SortOrder asc,pqa2.AnswerString asc--
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.type, 
		pqa3.LinkedQuestion3ID,pqa3.LinkedQuestion4ID,pqa3.LinkedQuestion5ID,pqa3.LinkedQuestion6ID,
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		--order by ISNULL( pqa3.SortOrder, 99999) asc--order by pqa3.SortOrder asc,pqa3.AnswerString asc--
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.LinkedQuestion3ID,pqa4.LinkedQuestion4ID,pqa4.LinkedQuestion5ID,pqa4.LinkedQuestion6ID,
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		--order by ISNULL( pqa4.SortOrder, 99999) asc--order by pqa4.SortOrder asc,pqa4.AnswerString asc--
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.LinkedQuestion3ID,pqa5.LinkedQuestion4ID,pqa5.LinkedQuestion5ID,pqa5.LinkedQuestion6ID,
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		--order by ISNULL( pqa5.SortOrder, 99999) asc--order by pqa5.SortOrder asc,pqa5.AnswerString asc--
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.LinkedQuestion3ID,pqa6.LinkedQuestion4ID,pqa6.LinkedQuestion5ID,pqa6.LinkedQuestion6ID,
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		--order by ISNULL( pqa6.SortOrder, 99999) asc--order by pqa6.SortOrder asc,pqa6.AnswerString asc--
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where pq.PQID not in
	(select pqid from profilequestions)
	and
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID) =0)-- and pq.CountryID = @countryId) = 0)-- commented because no need for country
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID --and pq.CountryID = @countryId -- commented because no need for country
			order by pqu.pquanswerid) > 0 )	
		)
	)
	and
	pq.Status = 1
)


USE [SMDv2]
GO

/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 4/22/2016 4:18:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[GetProducts] 

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
        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		     SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
		   SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select *, COUNT(*) OVER() AS TotalItems
from
(	select campaignid as ItemId, DisplayTitle ItemName, 'Ad' Type, 
    Description +'\n' + CampaignDescription as description,
	Case 
	    when Type = 3  -- Flyer
		THEN
		  2
		when Type = 4
		THEN
		    3
		when Type = 1
		THEN
		    1
		END as
	  ItemType, 
	((ClickRate * 50) / 100) as AdClickRate,  -- Amount AdViewer will get
	ImagePath as AdImagePath, 
	Case 
	    when Type = 3  -- Flyer
		THEN
		  LandingPageVideoLink--'http://manage.cash4ads.com/' +   LandingPageVideoLink -- 'http://manage.cash4ads.com/Ads/Ads/Content/' + CONVERT(NVARCHAR, CampaignID)
		when Type != 3
		THEN
		    LandingPageVideoLink
	END as AdVideoLink,
	Answer1 as AdAnswer1, Answer2 as AdAnswer2, Answer3 as AdAnswer3, CorrectAnswer as AdCorrectAnswer, VerifyQuestion as AdVerifyQuestion, 
	RewardType as AdRewardType,
	Voucher1Heading as AdVoucher1Heading, Voucher1Description as AdVoucher1Description,
	Voucher1Value as AdVoucher1Value, NULL as SqLeftImagePath, NULL as SqRightImagePath,
	Case 
	    when Type = 4  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when Type != 4
		THEN
		    NULL
	END as GameUrl, 
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

	((row_number() over (order by isNUll(Priority,0) desc) * 100) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
	(select 
	CASE
		WHEN usr.ProfileImage is null or usr.ProfileImage = ''
		THEN usr.ProfileImage
		WHEN usr.ProfileImage is not null
		THEN 'http://manage.cash4ads.com/' + usr.ProfileImage
	END as AdvertisersLogoPath from AspNetUsers usr where usr.Id = UserID) as AdvertisersLogoPath,
	VideoUrl as LandingPageUrl,
	BuuyItLine1 as BuyItLine1,
	BuyItLine2 as BuyItLine2,
	 BuyItLine3 as BuyItLine3,
	BuyItButtonLabel as BuyItButtonText, 
	 'http://manage.cash4ads.com/' +BuyItImageUrl as BuyItImageUrl,
	'http://manage.cash4ads.com/' + Voucher1ImagePath as VoucherImagePath
	from adcampaign
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
			adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) = 0)
		 or
		 ((select top 1 adResponse.UserSelection from AdCampaignResponse adResponse 
			where adResponse.CampaignID = adcampaign.CampaignID and adResponse.UserID = @UserID) 
			is null)
		)
	)
	
	union
	select *, NULL as AdvertisersLogoPath,
	null as LandingPageUrl,
	null as BuyItLine1,
	null as BuyItLine2,
	 null as BuyItLine3,
	null as BuyItButtonText, 
	null as BuyItImageUrl,
	null as VoucherImagePath
	
	  from [GetUserSurveys](@UserID)

	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1LinkedQ3, PQA1LinkedQ4, PQA1LinkedQ5,PQA1LinkedQ6, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2LinkedQ3, PQA2LinkedQ4, PQA2LinkedQ5,PQA2LinkedQ6, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3LinkedQ3, PQA3LinkedQ4, PQA3LinkedQ5,PQA3LinkedQ6, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4LinkedQ3, PQA4LinkedQ4, PQA4LinkedQ5,PQA4LinkedQ6, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5LinkedQ3, PQA5LinkedQ4, PQA5LinkedQ5,PQA5LinkedQ6, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6LinkedQ3, PQA6LinkedQ4, PQA6LinkedQ5,PQA6LinkedQ6, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	NULL as AdvertisersLogoPath ,
	null as LandingPageUrl,
	null as BuyItLine1,
	null as BuyItLine2,
	 null as BuyItLine3,
	null as BuyItButtonText, 
	null as BuyItImageUrl,
	null as VoucherImagePath
	from [GetUserProfileQuestions](@UserID, @countryId)
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

GO
/* Added By  (26 April 2016) - End */


/****** Object:  View [dbo].[vw_GetUserTransactions]    Script Date: 4/27/2016 2:08:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_GetUserTransactions]
AS
SELECT        dbo.AdCampaignResponse.ResponseID AS tid, dbo.AdCampaignResponse.CreatedDateTime AS TDate, dbo.AdCampaignResponse.EndUserDollarAmount AS Deposit, NULL AS Withdrawal, 
                         'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], dbo.AdCampaignResponse.UserID AS userId
FROM            dbo.AdCampaign INNER JOIN
                         dbo.AdCampaignResponse ON dbo.AdCampaignResponse.CampaignID = dbo.AdCampaign.CampaignID
WHERE        (dbo.AdCampaignResponse.EndUserDollarAmount IS NOT NULL)
UNION
SELECT         [Transaction].txId AS tid, [Transaction].TransactionDate AS TDate, NULL AS Deposit, [Transaction].DebitAmount AS Withdrawal, 'Paypal Withdrawal ' AS [Transaction], AspNetUsers.Id AS userId
FROM            [Transaction] JOIN
                         Account ON Account.AccountId = [Transaction].AccountID JOIN
                         AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
WHERE        [Transaction].DebitAmount IS NOT NULL

GO


/*********************************************/



CREATE TABLE [dbo].[EmailQueue](
	[EmailQueueId] [int] IDENTITY(1,1) NOT NULL,
	[To] [varchar](200) NULL,
	[Cc] [varchar](200) NULL,
	[EmailFrom] [varchar](200) NULL,
	[Type] [smallint] NULL,
	[Subject] [varchar](200) NULL,
	[Body] [text] NULL,
	[Images] [text] NULL,
	[SendDateTime] [datetime] NULL,
	[IsDeliverd] [tinyint] NULL,
	[SMTPUserName] [varchar](50) NULL,
	[SMTPPassword] [varchar](50) NULL,
	[SMTPServer] [varchar](50) NOT NULL,
	[ErrorResponse] [nvarchar](max) NULL,
	[FileAttachment] [nvarchar](max) NULL,
	[AttemptCount] [int] NULL,
	[ToName] [varchar](100) NULL,
	[FromName] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_MailBox] PRIMARY KEY CLUSTERED 
(
	[EmailQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_tbl_Emails_MailBox_SendDateTime]  DEFAULT (getdate()) FOR [SendDateTime]
GO

ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_tbl_MailBox_IsDevliverd]  DEFAULT (0) FOR [IsDeliverd]
GO

ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_tbl_Emails_MailBox_SMTPUserName]  DEFAULT ('') FOR [SMTPUserName]
GO

ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_tbl_Emails_MailBox_SMTPPassword]  DEFAULT ('') FOR [SMTPPassword]
GO

ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_tbl_Emails_MailBox_SMTPServer]  DEFAULT ('') FOR [SMTPServer]
GO


alter table [transaction] add AccountBalance float null
alter table CouponCodes add TakenDateTime datetime null


/****** Object:  View [dbo].[vw_GetUserTransactions]    Script Date: 4/28/2016 9:34:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[vw_GetUserTransactions]
AS
SELECT         [Transaction].txId AS tid, [Transaction].TransactionDate AS TDate, [Transaction].CreditAmount AS Deposit, null AS Withdrawal,
 'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], AspNetUsers.Id AS userId,[Transaction].AccountBalance as AccountBalance,
  Account.AccountBalance as CurentBalance
FROM            [Transaction] JOIN AdCampaign on [Transaction].AdCampaignID =  AdCampaign.CampaignID JOIN
                         Account ON Account.AccountId = [Transaction].AccountID JOIN
                         AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
WHERE        [Transaction].CreditAmount IS NOT NULL and Account.AccountType = 4
UNION
SELECT         [Transaction].txId AS tid, [Transaction].TransactionDate AS TDate, NULL AS Deposit, 
[Transaction].DebitAmount AS Withdrawal, 'Paypal Withdrawal ' AS [Transaction], AspNetUsers.Id AS userId,[Transaction].AccountBalance as AccountBalance,
Null as CurentBalance
FROM            [Transaction] JOIN
                         Account ON Account.AccountId = [Transaction].AccountID JOIN
                         AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
WHERE        [Transaction].DebitAmount IS NOT NULL and Account.AccountType = 2


GO




SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vw_PublisherTransaction]
AS
SELECT        dbo.[Transaction].TxID AS tid, dbo.[Transaction].TransactionDate AS TDate,
  NULL AS Deposit, 
[Transaction].DebitAmount AS Withdrawal,
  'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], 
                         dbo.AspNetUsers.Id AS userId, dbo.AdCampaign.CompanyId AS ownerCompanyId, dbo.Account.AccountBalance AS CurentBalance, dbo.AdCampaign.CampaignID
FROM            dbo.[Transaction] INNER JOIN
                         dbo.AdCampaign ON dbo.[Transaction].AdCampaignID = dbo.AdCampaign.CampaignID INNER JOIN
                         dbo.Account ON dbo.Account.AccountId = dbo.[Transaction].AccountID INNER JOIN
                         dbo.AspNetUsers ON dbo.Account.CompanyId = dbo.AspNetUsers.CompanyId
WHERE       [Transaction].DebitAmount IS NOT NULL  AND (dbo.Account.AccountType = 4)




GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Transaction"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 213
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdCampaign"
            Begin Extent = 
               Top = 6
               Left = 251
               Bottom = 136
               Right = 461
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Account"
            Begin Extent = 
               Top = 6
               Left = 499
               Bottom = 136
               Right = 674
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 6
               Left = 712
               Bottom = 136
               Right = 938
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_PublisherTransaction'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_PublisherTransaction'
GO


USE [SMDv2]
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveys]    Script Date: 4/29/2016 7:43:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[GetUserSurveys]
(	
	-- Add the parameters for the function here
	@userId uniqueidentifier = ''
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath, gameurl, 
 pqanswer1id, pqanswer1, pqa1Linkedq1, pqa1Linkedq2, pqa1Linkedq3, pqa1Linkedq4, pqa1Linkedq5, pqa1Linkedq6, pqa1Type, pqa1SortOrder, pqa1ImagePath,
 pqanswer2id, pqanswer2, pqa2Linkedq1, pqa2Linkedq2, pqa2Linkedq3, pqa2Linkedq4, pqa2Linkedq5, pqa2Linkedq6, pqa2Type, pqa2SortOrder, pqa2ImagePath,
 pqanswer3id, pqanswer3, pqa3Linkedq1, pqa3Linkedq2, pqa3Linkedq3, pqa3Linkedq4, pqa3Linkedq5, pqa3Linkedq6, pqa3Type, pqa3SortOrder, pqa3ImagePath,
 pqanswer4id, pqanswer4, pqa4Linkedq1, pqa4Linkedq2, pqa4Linkedq3, pqa4Linkedq4, pqa4Linkedq5, pqa4Linkedq6, pqa4Type, pqa4SortOrder, pqa4ImagePath,
 pqanswer5id, pqanswer5, pqa5Linkedq1, pqa5Linkedq2, pqa5Linkedq3, pqa5Linkedq4, pqa5Linkedq5, pqa5Linkedq6, pqa5Type, pqa5SortOrder, pqa5ImagePath,
 pqanswer6id, pqanswer6, pqa6Linkedq1, pqa6Linkedq2, pqa6Linkedq3, pqa6Linkedq4, pqa6Linkedq5, pqa6Linkedq6, pqa6Type, pqa6SortOrder, pqa6ImagePath,
 weightage, sqleftImagePercentage,
 sqRightImagePercentage)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'', 
	--CASE
	--	WHEN sq.LeftPicturePath is null or sq.LeftPicturePath = ''
	--	THEN
	 sq.LeftPicturePath
		--WHEN sq.LeftPicturePath is not null
	--	THEN 'http://manage.cash4ads.com/' + sq.LeftPicturePath
	--END 
	as SqLeftImagePath, 
--	CASE
	--	WHEN sq.RightPicturePath is null or sq.RightPicturePath = ''
		--THEN 
		sq.RightPicturePath
	--	WHEN sq.RightPicturePath is not null
		--THEN 'http://manage.cash4ads.com/' + sq.RightPicturePath
	--END 
	as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(((row_number() over (order by sq.sqid) * 100) + 2) + isnull(sq.Priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	(((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
	)
	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'',
	CASE
		WHEN sqp.LeftPicturePath is null or sqp.LeftPicturePath = ''
		THEN sqp.LeftPicturePath
		WHEN sqp.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sqp.RightPicturePath is null or sqp.RightPicturePath = ''
		THEN sqp.RightPicturePath
		WHEN sqp.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.RightPicturePath
	END as SqRightImagePath, '', 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(select (isnull(weightage, 0) + isnull(sq.Priority, 0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq	
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) is null)
		)
	  and
	  sq.Status = 3 -- live
    ) 
  )
	select * from surveyquestions
)

/* May 06,2016 to be executed*/

/****** Object:  View [dbo].[vw_GetUserTransactions]    Script Date: 5/6/2016 4:41:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_GetUserTransactions]
AS
SELECT        [Transaction].txId AS tid, [Transaction].TransactionDate AS TDate, [Transaction].CreditAmount AS Deposit, NULL AS Withdrawal, 'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], 
                         AspNetUsers.Id AS userId, [Transaction].AccountBalance AS AccountBalance, Account.AccountBalance AS CurentBalance, '' as VoucherTitle

FROM            [Transaction] JOIN
                         AdCampaign ON [Transaction].AdCampaignID = AdCampaign.CampaignID JOIN
                         Account ON Account.AccountId = [Transaction].AccountID JOIN
                         AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
WHERE        [Transaction].CreditAmount IS NOT NULL AND Account.AccountType = 4
UNION
SELECT        [Transaction].txId AS tid, [Transaction].TransactionDate AS TDate, NULL AS Deposit, [Transaction].DebitAmount AS Withdrawal, 'Paypal Withdrawal ' AS [Transaction], AspNetUsers.Id AS userId, 
                         [Transaction].AccountBalance AS AccountBalance, NULL AS CurentBalance, AdCampaign.CampaignName as VoucherTitle
FROM            [Transaction] 
       JOIN Account ON Account.AccountId = [Transaction].AccountID 
       JOIN AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
       LEFT JOIN CouponCodes ON [Transaction].CouponCodeId = CouponCodes.CodeId
       JOIN AdCampaign ON CouponCodes.CampaignId = AdCampaign.CampaignID
WHERE        [Transaction].DebitAmount IS NOT NULL AND Account.AccountType = 2

GO


/****** Object:  View [dbo].[vw_PublisherTransaction]    Script Date: 5/6/2016 4:42:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_PublisherTransaction]
AS
SELECT        dbo.[Transaction].TxID AS tid, dbo.[Transaction].TransactionDate AS TDate, NULL AS Deposit, dbo.[Transaction].DebitAmount AS Withdrawal, 'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], 
                         dbo.AspNetUsers.Id AS userId, dbo.AdCampaign.CompanyId AS ownerCompanyId, dbo.Account.AccountBalance AS CurentBalance, dbo.AdCampaign.CampaignID, dbo.AspNetUsers.Email
FROM            dbo.[Transaction] INNER JOIN
                         dbo.AdCampaign ON dbo.[Transaction].AdCampaignID = dbo.AdCampaign.CampaignID INNER JOIN
                         dbo.Account ON dbo.Account.AccountId = dbo.[Transaction].AccountID INNER JOIN
                         dbo.AspNetUsers ON dbo.Account.CompanyId = dbo.AspNetUsers.CompanyId
WHERE        (dbo.[Transaction].DebitAmount IS NOT NULL) AND (dbo.Account.AccountType = 4)

GO



/****** Object:  View [dbo].[vw_Cash4AdsReport]    Script Date: 5/6/2016 5:28:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_Cash4AdsReport]
AS
SELECT        [Transaction].txId AS tid, 
[Transaction].TransactionDate AS TDate, 
NULL AS Deposit, 
[Transaction].DebitAmount AS Withdrawal,
 'Paypal Withdrawal ' AS [Transaction], 
 AspNetUsers.Id AS userId, 
                         [Transaction].AccountBalance AS AccountBalance, 
						 NULL AS CurentBalance, 
						  dbo.AdCampaign.CampaignID,
						 AdCampaign.CampaignName as VoucherTitle,
						 dbo.AspNetUsers.Email as Email
FROM            [Transaction] 
						 JOIN Account ON Account.AccountId = [Transaction].AccountID 
						 JOIN AspNetUsers ON Account.CompanyId = AspNetUsers.CompanyId
						 LEFT JOIN CouponCodes ON [Transaction].CouponCodeId = CouponCodes.CodeId
						 JOIN AdCampaign ON CouponCodes.CampaignId = AdCampaign.CampaignID
WHERE        [Transaction].DebitAmount IS NOT NULL AND Account.AccountType = 2

UNION


SELECT        dbo.[Transaction].TxID AS tid,
 dbo.[Transaction].TransactionDate AS TDate,
  NULL AS Deposit,
   dbo.[Transaction].DebitAmount AS Withdrawal, 
   'Viewed - ' + dbo.AdCampaign.DisplayTitle AS [Transaction], 
                         dbo.AspNetUsers.Id AS userId,
						  dbo.AdCampaign.CompanyId AS ownerCompanyId, 
						  dbo.Account.AccountBalance AS CurentBalance,
						   dbo.AdCampaign.CampaignID,
						   '' as VoucherTitle,
						   dbo.AspNetUsers.Email as Email
FROM            dbo.[Transaction] INNER JOIN
                         dbo.AdCampaign ON dbo.[Transaction].AdCampaignID = dbo.AdCampaign.CampaignID INNER JOIN
                         dbo.Account ON dbo.Account.AccountId = dbo.[Transaction].AccountID INNER JOIN
                         dbo.AspNetUsers ON dbo.Account.CompanyId = dbo.AspNetUsers.CompanyId
WHERE        (dbo.[Transaction].DebitAmount IS NOT NULL) AND (dbo.Account.AccountType = 4)


GO


/**************5-31-2016*************/
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	IsUseFilter bit NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	LogoUrl nvarchar(200) NULL,
	VoucherAdditionalInfo nvarchar(MAX) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	CouponId bigint NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products ADD
	VoucherClausePrice float(53) NULL
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	IsShowVoucherSetting bit NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	VideoLink2 nvarchar(MAX) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	VoucherHighlightLine1 nvarchar(500) NULL,
	VoucherHighlightLine2 nvarchar(500) NULL,
	VoucherHighlightLine3 nvarchar(500) NULL,
	VoucherHighlightLine4 nvarchar(500) NULL,
	VoucherHighlightLine5 nvarchar(500) NULL,
	VoucherFinePrintLine1 nvarchar(500) NULL,
	VoucherFinePrintLine2 nvarchar(500) NULL,
	VoucherFinePrintLine3 nvarchar(500) NULL,
	VoucherFinePrintLine4 nvarchar(500) NULL,
	VoucherFinePrintLine5 nvarchar(500) NULL,
	VoucherLocationLine1 nvarchar(500) NULL,
	VoucherLocationLine2 nvarchar(500) NULL,
	VoucherLocationLine3 nvarchar(500) NULL,
	VoucherLocationLine4 nvarchar(500) NULL,
	VoucherLocationLine5 nvarchar(500) NULL,
	VoucherHowToRedeemLine1 nvarchar(500) NULL,
	VoucherHowToRedeemLine2 nvarchar(500) NULL,
	VoucherHowToRedeemLine3 nvarchar(500) NULL,
	VoucherHowToRedeemLine4 nvarchar(500) NULL,
	VoucherHowToRedeemLine5 nvarchar(500) NULL,
	VoucherRedemptionPhone nvarchar(500) NULL,
	VoucherLocationLAT nvarchar(50) NULL,
	VoucherLocationLON nchar(10) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCodes ADD
	IsUsed bit NULL,
	UsedDateTime datetime NULL,
	UsageInfo nvarchar(500) NULL
GO
ALTER TABLE dbo.CouponCodes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	VoucherSecretKey nvarchar(MAX) NULL
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	IsSavedCoupon bit NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
/****** Object:  Table [dbo].[UserFavouriteCoupon]    Script Date: 6/24/2016 11:24:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserFavouriteCoupon](
	[FavouriteCouponId] [bigint] IDENTITY(1,1) NOT NULL,
	[CouponId] [bigint] NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_UserFavouriteCoupon] PRIMARY KEY CLUSTERED 
(
	[FavouriteCouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




GO
/****** Object:  StoredProcedure [dbo].[GetCoupons]    Script Date: 7/19/2016 4:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId = 2,		@type = 1,		@keywords = N'1',		@distance = 1,		@Lat = N'1',		@Lon = N'1',		@UserId = N'1',		@FromRow = 1,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN
	DECLARE @dob AS DateTime
	DECLARE @age AS INT
	DECLARE @gender AS INT
	DECLARE @currentDate AS DateTime
	DECLARE @countryId AS INT
	DECLARE @cityId AS INT
	DECLARE @industryId AS INT
	SELECT @dob = DOB FROM AspNetUsers where id=@UserID
	SELECT @gender = gender FROM AspNetUsers where id=@UserID
	SET @currentDate = getDate()
	SET @age = DATEDIFF(year, @dob, @currentDate)
	SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
	SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
	SELECT @industryId = industryId FROM AspNetUsers where id=@UserID


	select *, COUNT(*) OVER() AS TotalItems
	from (


	select vchr.campaignid as CouponId, CampaignName as CouponName, DisplayTitle as CouponTitle,
	[Description]  as  Firstline, CampaignDescription as SecondLine, 
	
	'http://manage.cash4ads.com/' + ImagePath as  CouponImage,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN c.Logo is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as AdvertisersLogoPath,
	CouponSwapValue, CouponActualValue,CompanyId
	
	from adcampaign vchr
	inner join CampaignCategories cc on cc.CampaignId = vchr.CampaignID and cc.CategoryId = @categoryId
	where (
		((@age is null) or (vchr.AgeRangeEnd >= @age and  @age >= vchr.AgeRangeStart))
		and
		((@gender is null) or (vchr.Gender = @gender))
		and
		((@industryId is null) or ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			 where MyCampaignCrit.CampaignID = vchr.CampaignID and 
			 MyCampaignCrit.IndustryID=@industryId) > 0 ))
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		and
		(vchr.Approved = 1)
		and
		(vchr.Type = 5) -- coupon
		
		and
		((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			 where MyCampaignLoc.CampaignID=vchr.CampaignID and MyCampaignLoc.CountryID=@countryId and
			 MyCampaignLoc.CityID=@cityId) > 0))
		)
		group by vchr.campaignid, CampaignName,DisplayTitle,[Description],CampaignDescription,vchr.ImagePath,LogoUrl,CouponSwapValue,CouponActualValue,CompanyId
		)as items
	order by CouponActualValue
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END




GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 7/18/2016 2:37:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetProducts] 


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
        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		     SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
		   SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select *, COUNT(*) OVER() AS TotalItems
from
(	select adcampaign.campaignid as ItemId, adcampaign.DisplayTitle ItemName, 'Ad' Type, 
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
	Case 
	    when adcampaign.Type = 4  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when adcampaign.Type != 4
		THEN
		    NULL
	END as GameUrl, 
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

	((row_number() over (order by ABS(CHECKSUM(NewId())) % 100 desc) * 100) + 1) Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage,
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
	(select count(*) from adcampaign v where AdCampaign.CompanyId = v.CompanyId and v.Type = 5 and v.Status = 3) as VoucherCount,
	AdCampaign.CompanyId
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
	null as CompanyId
	  from [GetUserSurveys](@UserID)
	  
	  
	  
	  -------------------profile questions
	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1LinkedQ3, PQA1LinkedQ4, PQA1LinkedQ5,PQA1LinkedQ6, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2LinkedQ3, PQA2LinkedQ4, PQA2LinkedQ5,PQA2LinkedQ6, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3LinkedQ3, PQA3LinkedQ4, PQA3LinkedQ5,PQA3LinkedQ6, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4LinkedQ3, PQA4LinkedQ4, PQA4LinkedQ5,PQA4LinkedQ6, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5LinkedQ3, PQA5LinkedQ4, PQA5LinkedQ5,PQA5LinkedQ6, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6LinkedQ3, PQA6LinkedQ4, PQA6LinkedQ5,PQA6LinkedQ6, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
	NULL as AdvertisersLogoPath ,
	null as LandingPageUrl,
	null as BuyItLine1,
	null as BuyItLine2,
	 null as BuyItLine3,
	null as BuyItButtonText, 
	null as BuyItImageUrl,
	null as VoucherImagePath,
	null as VoucherCount,
	null as CompanyId
	from [GetUserProfileQuestions](@UserID, @countryId)
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





GO
/****** Object:  StoredProcedure [dbo].[GetCoupons]    Script Date: 7/19/2016 4:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId = 12,		@type = 1,		@keywords = N'1',		@distance = 1,		@Lat = N'1',		@Lon = N'1',		@UserId = N'1',		@FromRow = 1,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN
	DECLARE @dob AS DateTime
	DECLARE @age AS INT
	DECLARE @gender AS INT
	DECLARE @currentDate AS DateTime
	DECLARE @countryId AS INT
	DECLARE @cityId AS INT
	DECLARE @industryId AS INT
	SELECT @dob = DOB FROM AspNetUsers where id=@UserID
	SELECT @gender = gender FROM AspNetUsers where id=@UserID
	SET @currentDate = getDate()
	SET @age = DATEDIFF(year, @dob, @currentDate)
	SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
	SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
	SELECT @industryId = industryId FROM AspNetUsers where id=@UserID


	select *, COUNT(*) OVER() AS TotalItems
	from (


	select vchr.campaignid as CouponId, CampaignName as CouponName, DisplayTitle as CouponTitle,
	[Description]  as  Firstline, CampaignDescription as SecondLine, 
	
	ImagePath as  CouponImage,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN c.Logo is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as AdvertisersLogoPath,
	CouponSwapValue, CouponActualValue,CompanyId
	
	from adcampaign vchr
	inner join CampaignCategories cc on cc.CampaignId = vchr.CampaignID and cc.CategoryId = @categoryId
	where (
		((@age is null) or (vchr.AgeRangeEnd >= @age and  @age >= vchr.AgeRangeStart))
		and
		((@gender is null) or (vchr.Gender = @gender))
		and
		((@industryId is null) or ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			 where MyCampaignCrit.CampaignID = vchr.CampaignID and 
			 MyCampaignCrit.IndustryID=@industryId) > 0 ))
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		and
		(vchr.Approved = 1)
		and
		(vchr.Type = 5) -- coupon
		
		and
		((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			 where MyCampaignLoc.CampaignID=vchr.CampaignID and MyCampaignLoc.CountryID=@countryId and
			 MyCampaignLoc.CityID=@cityId) > 0))
		)
		group by vchr.campaignid, CampaignName,DisplayTitle,[Description],CampaignDescription,vchr.ImagePath,LogoUrl,CouponSwapValue,CouponActualValue,CompanyId
		)as items
	order by CouponActualValue
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END




/****** Object:  StoredProcedure [dbo].[GetCouponsByCompanyId]    Script Date: 7/20/2016 1:09:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetCouponsByCompanyId]
--  exec [GetCouponsByCompanyId] 165

	-- Add the parameters for the stored procedure here
	@CompanyId nvarchar(128) = 0 
AS
BEGIN
	DECLARE @dob AS DateTime
	DECLARE @age AS INT
	DECLARE @gender AS INT
	DECLARE @currentDate AS DateTime
	DECLARE @countryId AS INT
	DECLARE @cityId AS INT
	DECLARE @industryId AS INT
	--SELECT @dob = DOB FROM AspNetUsers where id=@UserID
	--SELECT @gender = gender FROM AspNetUsers where id=@UserID
	SET @currentDate = getDate()
	SET @age = DATEDIFF(year, @dob, @currentDate)
	SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
	SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
	--SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
	select campaignid as CouponId, CampaignName as CouponName, DisplayTitle as CouponTitle,
	[Description]  as  Firstline, CampaignDescription as SecondLine, ImagePath as  CouponImage,
	CouponSwapValue, CouponActualValue,
		(select 
	CASE
		WHEN adcampaign.LogoUrl is null or adcampaign.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN c.Logo is not null
		THEN 'http://manage.cash4ads.com/' + adcampaign.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = adcampaign.CompanyId) as AdvertisersLogoPath
	
	from adcampaign
	where (
		((@age is null) or (adcampaign.AgeRangeEnd >= @age and  @age >= adcampaign.AgeRangeStart))
		and
		((@gender is null) or (adcampaign.Gender = @gender))
		and
		((@industryId is null) or ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			 where MyCampaignCrit.CampaignID = adcampaign.CampaignID and 
			 MyCampaignCrit.IndustryID=@industryId) > 0 ))
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		and
		(adcampaign.Approved = 1)
		and
		(adcampaign.Type = 5) -- coupon
		and
		((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			 where MyCampaignLoc.CampaignID=adcampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
			 MyCampaignLoc.CityID=@cityId) > 0))
		)
END



USE [SMDv2]
GO

/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 7/22/2016 10:29:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[GetProducts] 


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
        -- Setting local variables
		   SELECT @dob = DOB FROM AspNetUsers where id=@UserID
		   SELECT @gender = gender FROM AspNetUsers where id=@UserID
		     SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
		   SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
		   SELECT @languageId = LanguageID FROM AspNetUsers where id=@UserID
		   SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
		   SET @currentDate = getDate()
		   SET @age = DATEDIFF(year, @age, @currentDate)

select *, COUNT(*) OVER() AS TotalItems
from
(	select adcampaign.campaignid as ItemId, adcampaign.DisplayTitle ItemName, 'Ad' Type, 
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
	Case 
	    when adcampaign.Type = 4  -- Game
		THEN
		    (select top 1 GameUrl from Game ORDER BY NEWID())
		when adcampaign.Type != 4
		THEN
		    NULL
	END as GameUrl, 
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
	(select count(*) from adcampaign v where AdCampaign.CompanyId = v.CompanyId and v.Type = 5 and v.Status = 3) as VoucherCount,
	AdCampaign.CompanyId,VideoLink2,IsShowVoucherSetting
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
	null as IsShowVoucherSetting
	  from [GetUserSurveys](@UserID)
	  
	  
	  
	  -------------------profile questions
	union
	select pqid, question, 'Question', 
	NULL, Type QuestionType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, NULL,NULL, NULL, 
	PQAnswerID1, PQAnswer1, PQA1LinkedQ1, PQA1LinkedQ2, PQA1LinkedQ3, PQA1LinkedQ4, PQA1LinkedQ5,PQA1LinkedQ6, PQA1Type, PQA1SortOrder, PQA1ImagePath,
	PQAnswerID2, PQAnswer2, PQA2LinkedQ1, PQA2LinkedQ2, PQA2LinkedQ3, PQA2LinkedQ4, PQA2LinkedQ5,PQA2LinkedQ6, PQA2Type, PQA2SortOrder, PQA2ImagePath,
	PQAnswerID3, PQAnswer3, PQA3LinkedQ1, PQA3LinkedQ2, PQA3LinkedQ3, PQA3LinkedQ4, PQA3LinkedQ5,PQA3LinkedQ6, PQA3Type, PQA3SortOrder, PQA3ImagePath,
	PQAnswerID4, PQAnswer4, PQA4LinkedQ1, PQA4LinkedQ2, PQA4LinkedQ3, PQA4LinkedQ4, PQA4LinkedQ5,PQA4LinkedQ6, PQA4Type, PQA4SortOrder, PQA4ImagePath,
	PQAnswerID5, PQAnswer5, PQA5LinkedQ1, PQA5LinkedQ2, PQA5LinkedQ3, PQA5LinkedQ4, PQA5LinkedQ5,PQA5LinkedQ6, PQA5Type, PQA5SortOrder, PQA5ImagePath,
	PQAnswerID6, PQAnswer6, PQA6LinkedQ1, PQA6LinkedQ2, PQA6LinkedQ3, PQA6LinkedQ4, PQA6LinkedQ5,PQA6LinkedQ6, PQA6Type, PQA6SortOrder, PQA6ImagePath,
	Weightage,
	NULL as SqLeftImagePercentage, NULL as SqRightImagePercentage, 
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
	null as IsShowVoucherSetting
	from [GetUserProfileQuestions](@UserID, @countryId)
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

GO






------------new tables


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetRoles SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.CompaniesAspNetUsers
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	CompanyId int NULL,
	UserId nvarchar(128) NULL,
	CreatedOn datetime NULL,
	Status int NULL,
	InvitationCode nvarchar(500) NULL,
	RoleId nvarchar(128) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD CONSTRAINT
	PK_CompaniesAspNetUsers PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD CONSTRAINT
	FK_CompaniesAspNetUsers_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD CONSTRAINT
	FK_CompaniesAspNetUsers_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD CONSTRAINT
	FK_CompaniesAspNetUsers_AspNetRoles FOREIGN KEY
	(
	RoleId
	) REFERENCES dbo.AspNetRoles
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompaniesAspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



GO



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD
	InvitationEmail nvarchar(500) NULL
GO
ALTER TABLE dbo.CompaniesAspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

/****** Object:  View [dbo].[vw_CompanyUsers]    Script Date: 7/27/2016 4:02:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[vw_CompanyUsers]
AS

select cu.id,u.id as UserId, 

(case when u.email is null then cu.InvitationEmail else u.email end) as email


, u.FullName, r.Name as RoleName, cu.CreatedOn, (case  when cu.status = 1 then 'Invitation Sent' when cu.status = 2 then 'Active' end) status, cu.companyid , c.CompanyName, cu.RoleId
from dbo.CompaniesAspNetUsers  cu
inner join   dbo.AspNetRoles r on r.Id = roleid
left outer join AspNetUsers u on u.Id = cu.userid
inner join Company c on cu.CompanyId = c.CompanyId




GO









/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCategory ADD
	ImagePath nvarchar(500) NULL
GO
ALTER TABLE dbo.CouponCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCategory ADD
	SortOrder int NULL
GO
ALTER TABLE dbo.CouponCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetCouponsByCompanyId]    Script Date: 7/28/2016 7:53:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetCouponsByCompanyId]
	-- Add the parameters for the stored procedure here
	-- [GetCouponsByCompanyId] 93
	@CompanyId nvarchar(128) = 0 
AS
BEGIN
	DECLARE @dob AS DateTime
	DECLARE @age AS INT
	DECLARE @gender AS INT
	DECLARE @currentDate AS DateTime
	DECLARE @countryId AS INT
	DECLARE @cityId AS INT
	DECLARE @industryId AS INT
	--SELECT @dob = DOB FROM AspNetUsers where id=@UserID
	--SELECT @gender = gender FROM AspNetUsers where id=@UserID
	SET @currentDate = getDate()
	SET @age = DATEDIFF(year, @dob, @currentDate)
	SELECT @countryId = NULL--countryId FROM Company where @companyId=@companyId
	SELECT @cityId = NULL--cityId FROM Company where @companyId=@companyId
	--SELECT @industryId = industryId FROM AspNetUsers where id=@UserID
	select campaignid as CouponId, CampaignName as CouponName, DisplayTitle as CouponTitle,
	[Description]  as  Firstline, CampaignDescription as SecondLine, 'http://manage.cash4ads.com/' + ImagePath as  CouponImage,
	CouponSwapValue, CouponActualValue, 
	(select 
	CASE
		WHEN adcampaign.LogoUrl is null or adcampaign.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + usr.Logo
		WHEN usr.Logo is not null
		THEN 'http://manage.cash4ads.com/' + adcampaign.LogoUrl
	END as AdvertisersLogoPath from company usr where usr.CompanyId = adcampaign.CompanyId) as AdvertisersLogoPath
	
	from adcampaign
	where (
		((@age is null) or (adcampaign.AgeRangeEnd >= @age and  @age >= adcampaign.AgeRangeStart))
		and
		((@gender is null) or (adcampaign.Gender = @gender))
		and
		((@industryId is null) or ((select count(*) from AdCampaignTargetCriteria MyCampaignCrit
			 where MyCampaignCrit.CampaignID = adcampaign.CampaignID and 
			 MyCampaignCrit.IndustryID=@industryId) > 0 ))
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		and
		(adcampaign.Approved = 1)
		and
		(adcampaign.Type = 5) -- coupon
		and
		((@countryId is null or @cityId is null) or ((select count(*) from AdCampaignTargetLocation MyCampaignLoc
			 where MyCampaignLoc.CampaignID=adcampaign.CampaignID and MyCampaignLoc.CountryID=@countryId and
			 MyCampaignLoc.CityID=@cityId) > 0))
		)
		and AdCampaign.companyid = @companyid
END







GO

/****** Object:  StoredProcedure [dbo].[SearchCoupons]    Script Date: 7/29/2016 6:15:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId = 8,		@type = 1,		@keywords = N'1',		@distance = 1,		@Lat = N'1',		@Lon = N'1',		@UserId = N'1',		@FromRow = 1,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN

	select *, COUNT(*) OVER() AS TotalItems
	from (
	
	select vchr.campaignid as CouponId, CampaignName as CouponName, DisplayTitle as CouponTitle,
	[Description]  as  Firstline, CampaignDescription as SecondLine, 
	
	ImagePath as  CouponImage,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN c.Logo is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as AdvertisersLogoPath,
	CouponSwapValue, CouponActualValue,CompanyId
	
	from adcampaign vchr
	inner join CampaignCategories cc on cc.CampaignId = vchr.CampaignID and cc.CategoryId = @categoryId
	where (
		
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		
		(vchr.Approved = 1) and status = 3
		and
		(vchr.Type = 5) -- coupon
		
		)
		group by vchr.campaignid, CampaignName,DisplayTitle,[Description],CampaignDescription,vchr.ImagePath,LogoUrl,CouponSwapValue,CouponActualValue,CompanyId
		)as items
	order by CouponActualValue
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END

GO






GO

/****** Object:  Table [dbo].[Coupon]    Script Date: 7/29/2016 12:17:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Coupon](
	[CouponID] [bigint] IDENTITY(1,1) NOT NULL,
	[LanguageID] [int] NULL,
	[UserID] [nvarchar](128) NULL,

	[CouponTitle] [nvarchar](200) NULL,
	[SearchKeywords] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[Archived] [bit] NULL,
	[Approved] [bit] NULL,
	[ApprovedBy] [nvarchar](128) NULL,
	[ApprovalDateTime] [datetime] NULL,

	[CreatedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDateTime] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	
	[RejectedReason] [nvarchar](1000) NULL,
	[Rejecteddatetime] [datetime] NULL,
	[RejectedBy] [nvarchar](100) NULL,
	
	[CurrencyId] [Int] NULL,
	[Price] [float] NULL,
	[Savings] [float] NULL,
	[SwapCost] [float] NULL,

	[CouponViewCount] [int] NULL,
	
	[CouponIssuedCount] [int] NULL,
	[CouponRedeemedCount] [int] NULL,


	[CouponQtyPerUser] [int] NULL,
	[CouponListingMode] [int] NULL,
	[CompanyId] [int] NULL,
	[CouponActiveMonth] [int] NULL,
	[CouponActiveYear] [int] NULL,
	[CouponExpirydate] [datetime] NULL,
	[couponImage1] [nvarchar](max) NULL,
	[CouponImage2] [nvarchar](max) NULL,
	[CouponImage3] [nvarchar](max) NULL,
	
	
	
	[LogoUrl] [nvarchar](200) NULL,
	
	[HighlightLine1] [nvarchar](100) NULL,
	[HighlightLine2] [nvarchar](100) NULL,
	[HighlightLine3] [nvarchar](100) NULL,
	[HighlightLine4] [nvarchar](100) NULL,
	[HighlightLine5] [nvarchar](100) NULL,

	[FinePrintLine1] [nvarchar](100) NULL,
	[FinePrintLine2] [nvarchar](100) NULL,
	[FinePrintLine3] [nvarchar](100) NULL,
	[FinePrintLine4] [nvarchar](100) NULL,
	[FinePrintLine5] [nvarchar](100) NULL,

	[LocationBranchId] int NULL,
	[LocationTitle] [nvarchar](500) NULL,
	[LocationLine1] [nvarchar](500) NULL,
	[LocationLine2] [nvarchar](500) NULL,
	[LocationCity] [nvarchar](500) NULL,
	[LocationState] [nvarchar](500) NULL,
	[LocationZipCode] [nvarchar](500) NULL,
	[LocationLAT] [nvarchar](50) NULL,
	[LocationLON] [nvarchar](50) NULL,
	[LocationPhone] [nvarchar](200) NULL,

	GeographyColumn as geography::STGeomFromText('POINT('+convert(varchar(20),LocationLON)+' '+convert(varchar(20),LocationLAT)+')',4326),


	[HowToRedeemLine1] [nvarchar](500) NULL,
	[HowToRedeemLine2] [nvarchar](500) NULL,
	[HowToRedeemLine3] [nvarchar](500) NULL,
	[HowToRedeemLine4] [nvarchar](500) NULL,
	[HowToRedeemLine5] [nvarchar](500) NULL,


 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Coupon]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO

ALTER TABLE [dbo].[Coupon] CHECK CONSTRAINT [FK_Coupon_Company]
GO

ALTER TABLE [dbo].[Coupon]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Language] ([LanguageID])
GO

ALTER TABLE [dbo].[Coupon] CHECK CONSTRAINT [FK_Coupon_Language]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1- Draft , 2 - Submitted for Approval, 3 - Live, 4 Paused, 5 - Completed, 6 - Approval Rejected' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon', @level2type=N'COLUMN',@level2name=N'Status'
GO



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon
	DROP CONSTRAINT FK_Coupon_Language
GO
ALTER TABLE dbo.Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon
	DROP CONSTRAINT FK_Coupon_Company
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.BranchCategory
	(
	BranchCategoryId bigint NOT NULL IDENTITY (1, 1),
	BranchCategoryName nvarchar(100) NULL,
	CompanyId int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.BranchCategory ADD CONSTRAINT
	PK_BranchCategory PRIMARY KEY CLUSTERED 
	(
	BranchCategoryId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BranchCategory ADD CONSTRAINT
	FK_BranchCategory_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BranchCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.CompanyBranch
	(
	BranchId bigint NOT NULL IDENTITY (1, 1),
	BranchTitle nvarchar(100) NULL,
	BranchAddressLine1 nvarchar(100) NULL,
	BranchAddressLine2 nvarchar(100) NULL,
	BranchCity nvarchar(50) NULL,
	BranchState nvarchar(50) NULL,
	BranchZipCode nvarchar(50) NULL,
	BranchPhone nvarchar(50) NULL,
	BranchLocationLat nvarchar(50) NULL,
	BranchLocationLong nvarchar(50) NULL,
	BranchCategoryId bigint NULL,
	CompanyId int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CompanyBranch ADD CONSTRAINT
	PK_CompanyBranch PRIMARY KEY CLUSTERED 
	(
	BranchId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CompanyBranch ADD CONSTRAINT
	FK_CompanyBranch_BranchCategory FOREIGN KEY
	(
	BranchCategoryId
	) REFERENCES dbo.BranchCategory
	(
	BranchCategoryId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompanyBranch ADD CONSTRAINT
	FK_CompanyBranch_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompanyBranch SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Coupon
	(
	CouponId bigint NOT NULL IDENTITY (1, 1),
	LanguageId int NULL,
	UserId nvarchar(128) NULL,
	CouponTitle nvarchar(200) NULL,
	SearchKeywords nvarchar(MAX) NULL,
	Status int NULL,
	Archived bit NULL,
	Approved bit NULL,
	ApprovedBy nvarchar(128) NULL,
	ApprovalDateTime datetime NULL,
	CreatedDateTime datetime NULL,
	CreatedBy nvarchar(100) NULL,
	ModifiedDateTime datetime NULL,
	ModifiedBy nvarchar(100) NULL,
	RejectedReason nvarchar(1000) NULL,
	Rejecteddatetime datetime NULL,
	RejectedBy nvarchar(100) NULL,
	CurrencyId int NULL,
	Price float(53) NULL,
	Savings float(53) NULL,
	SwapCost float(53) NULL,
	CouponViewCount int NULL,
	CouponIssuedCount int NULL,
	CouponRedeemedCount int NULL,
	CouponQtyPerUser int NULL,
	CouponListingMode int NULL,
	CompanyId int NULL,
	CouponActiveMonth int NULL,
	CouponActiveYear int NULL,
	CouponExpirydate datetime NULL,
	couponImage1 nvarchar(MAX) NULL,
	CouponImage2 nvarchar(MAX) NULL,
	CouponImage3 nvarchar(MAX) NULL,
	LogoUrl nvarchar(200) NULL,
	HighlightLine1 nvarchar(100) NULL,
	HighlightLine2 nvarchar(100) NULL,
	HighlightLine3 nvarchar(100) NULL,
	HighlightLine4 nvarchar(100) NULL,
	HighlightLine5 nvarchar(100) NULL,
	FinePrintLine1 nvarchar(100) NULL,
	FinePrintLine2 nvarchar(100) NULL,
	FinePrintLine3 nvarchar(100) NULL,
	FinePrintLine4 nvarchar(100) NULL,
	FinePrintLine5 nvarchar(100) NULL,
	LocationBranchId bigint NULL,
	LocationTitle nvarchar(500) NULL,
	LocationLine1 nvarchar(500) NULL,
	LocationLine2 nvarchar(500) NULL,
	LocationCity nvarchar(500) NULL,
	LocationState nvarchar(500) NULL,
	LocationZipCode nvarchar(500) NULL,
	LocationLAT nvarchar(50) NULL,
	LocationLON nvarchar(50) NULL,
	LocationPhone nvarchar(200) NULL,
	GeographyColumn  AS ([geography]::STGeomFromText(((('POINT('+CONVERT([varchar](20),[LocationLON]))+' ')+CONVERT([varchar](20),[LocationLAT]))+')',(4326))),
	HowToRedeemLine1 nvarchar(500) NULL,
	HowToRedeemLine2 nvarchar(500) NULL,
	HowToRedeemLine3 nvarchar(500) NULL,
	HowToRedeemLine4 nvarchar(500) NULL,
	HowToRedeemLine5 nvarchar(500) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Coupon SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'1- Draft , 2 - Submitted for Approval, 3 - Live, 4 Paused, 5 - Completed, 6 - Approval Rejected'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Coupon', N'COLUMN', N'Status'
GO
SET IDENTITY_INSERT dbo.Tmp_Coupon ON
GO
IF EXISTS(SELECT * FROM dbo.Coupon)
	 EXEC('INSERT INTO dbo.Tmp_Coupon (CouponId, LanguageId, UserId, CouponTitle, SearchKeywords, Status, Archived, Approved, ApprovedBy, ApprovalDateTime, CreatedDateTime, CreatedBy, ModifiedDateTime, ModifiedBy, RejectedReason, Rejecteddatetime, RejectedBy, CurrencyId, Price, Savings, SwapCost, CouponViewCount, CouponIssuedCount, CouponRedeemedCount, CouponQtyPerUser, CouponListingMode, CompanyId, CouponActiveMonth, CouponActiveYear, CouponExpirydate, couponImage1, CouponImage2, CouponImage3, LogoUrl, HighlightLine1, HighlightLine2, HighlightLine3, HighlightLine4, HighlightLine5, FinePrintLine1, FinePrintLine2, FinePrintLine3, FinePrintLine4, FinePrintLine5, LocationBranchId, LocationTitle, LocationLine1, LocationLine2, LocationCity, LocationState, LocationZipCode, LocationLAT, LocationLON, LocationPhone, HowToRedeemLine1, HowToRedeemLine2, HowToRedeemLine3, HowToRedeemLine4, HowToRedeemLine5)
		SELECT CouponID, LanguageID, UserID, CouponTitle, SearchKeywords, Status, Archived, Approved, ApprovedBy, ApprovalDateTime, CreatedDateTime, CreatedBy, ModifiedDateTime, ModifiedBy, RejectedReason, Rejecteddatetime, RejectedBy, CurrencyId, Price, Savings, SwapCost, CouponViewCount, CouponIssuedCount, CouponRedeemedCount, CouponQtyPerUser, CouponListingMode, CompanyId, CouponActiveMonth, CouponActiveYear, CouponExpirydate, couponImage1, CouponImage2, CouponImage3, LogoUrl, HighlightLine1, HighlightLine2, HighlightLine3, HighlightLine4, HighlightLine5, FinePrintLine1, FinePrintLine2, FinePrintLine3, FinePrintLine4, FinePrintLine5, CONVERT(bigint, LocationBranchId), LocationTitle, LocationLine1, LocationLine2, LocationCity, LocationState, LocationZipCode, LocationLAT, LocationLON, LocationPhone, HowToRedeemLine1, HowToRedeemLine2, HowToRedeemLine3, HowToRedeemLine4, HowToRedeemLine5 FROM dbo.Coupon WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Coupon OFF
GO
DROP TABLE dbo.Coupon
GO
EXECUTE sp_rename N'dbo.Tmp_Coupon', N'Coupon', 'OBJECT' 
GO
ALTER TABLE dbo.Coupon ADD CONSTRAINT
	PK_Coupon PRIMARY KEY CLUSTERED 
	(
	CouponId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Coupon ADD CONSTRAINT
	FK_Coupon_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Coupon ADD CONSTRAINT
	FK_Coupon_Language FOREIGN KEY
	(
	LanguageId
	) REFERENCES dbo.Language
	(
	LanguageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Coupon ADD CONSTRAINT
	FK_Coupon_CompanyBranch FOREIGN KEY
	(
	LocationBranchId
	) REFERENCES dbo.CompanyBranch
	(
	BranchId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.CouponCategories
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	CategoryId int NULL,
	CouponId bigint NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CouponCategories ADD CONSTRAINT
	PK_CouponCategories PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CouponCategories ADD CONSTRAINT
	FK_CouponCategories_CouponCategory FOREIGN KEY
	(
	CategoryId
	) REFERENCES dbo.CouponCategory
	(
	CategoryId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CouponCategories ADD CONSTRAINT
	FK_CouponCategories_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CouponCategories SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignResponse ADD
	UserLocationLat nvarchar(50) NULL,
	UserLocationLong nvarchar(50) NULL,
	UserLocationCity nvarchar(100) NULL,
	UserLocationCountry nvarchar(100) NULL,
	UserLocationAddress nvarchar(200) NULL
GO
ALTER TABLE dbo.AdCampaignResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





--migration script

USE [SMDv2]
GO


UPDATE adcampaign
set
 CouponActualValue  =REPLACE(CouponActualValue, '£', '')
	, CouponDiscountValue = REPLACE(CouponDiscountValue, '£', '')
	,CouponSwapValue 		=REPLACE(CouponSwapValue, '£', '')

	
SET ANSI_WARNINGS  OFF;
INSERT INTO [dbo].[Coupon]
           ([LanguageId]
           ,[UserId]
           ,[CouponTitle]
           ,[SearchKeywords]
           ,[Status]
           ,[Archived]
           ,[Approved]
           ,[ApprovedBy]
           ,[ApprovalDateTime]
           ,[CreatedDateTime]
           ,[CreatedBy]
           ,[ModifiedDateTime]
           ,[ModifiedBy]
           ,[RejectedReason]
           ,[Rejecteddatetime]
           ,[RejectedBy]
           ,[CurrencyId]
           ,[Price]
           ,[Savings]
           ,[SwapCost]
           ,[CouponViewCount]
           ,[CouponIssuedCount]
           ,[CouponRedeemedCount]
           ,[CouponQtyPerUser]
           ,[CouponListingMode]
           ,[CompanyId]
           ,[CouponActiveMonth]
           ,[CouponActiveYear]
           ,[CouponExpirydate]
           ,[couponImage1]
           ,[CouponImage2]
           ,[CouponImage3]
           ,[LogoUrl]
           ,[HighlightLine1]
           ,[HighlightLine2]
           ,[HighlightLine3]
           ,[HighlightLine4]
           ,[HighlightLine5]
           ,[FinePrintLine1]
           ,[FinePrintLine2]
           ,[FinePrintLine3]
           ,[FinePrintLine4]
           ,[FinePrintLine5]
           ,[LocationBranchId]
           ,[LocationTitle]
           ,[LocationLine1]
           ,[LocationLine2]
           ,[LocationCity]
           ,[LocationState]
           ,[LocationZipCode]
           ,[LocationLAT]
           ,[LocationLON]
           ,[LocationPhone]
           ,[HowToRedeemLine1]
           ,[HowToRedeemLine2]
           ,[HowToRedeemLine3]
           ,[HowToRedeemLine4]
           ,[HowToRedeemLine5])
    


	select    
	
	languageid,userid,DisplayTitle,'',
	3,0,1,'',getdate(),getdate(),'insertqry',
	getdate(),'','no reson',getdate(),null,
	1 --currencyid
	, cast(CouponActualValue as float)  --price
	, cast(CouponDiscountValue as float) --[Savings]
	,cast (CouponSwapValue as float)		-- swapcpost



	,0,0,0,10,1,companyid,1,6,'2016-12-31'
	,ImagePath, couponImage2,couponImage3, LogoUrl,
	
	VoucherHighlightLine1
	

           ,[VoucherHighlightLine2]
           ,[VoucherHighlightLine3]
           ,[VoucherHighlightLine4]
           ,[VoucherHighlightLine5]
           ,[VoucherFinePrintLine1]
           ,[VoucherFinePrintLine2]
           ,[VoucherFinePrintLine3]
           ,[VoucherFinePrintLine4]
           ,[VoucherFinePrintLine5]
           ,null -- branch
           ,VoucherLocationLine1
           ,[VoucherLocationLine1]
           ,[VoucherLocationLine2]
           ,[VoucherLocationLine3]
           ,[VoucherLocationLine4]
           ,[VoucherLocationLine5]
           ,[VoucherLocationLAT]
           ,[VoucherLocationLON]
           ,VoucherRedemptionPhone
           ,[VoucherHowToRedeemLine1]
           ,[VoucherHowToRedeemLine2]
           ,[VoucherHowToRedeemLine3]
           ,[VoucherHowToRedeemLine4]
           ,[VoucherHowToRedeemLine5]
		   
		   from AdCampaign a where type = 5 and status = 3 order by campaignid
SET ANSI_WARNINGS ON;






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Section](
 [SectionId] [int] NOT NULL  IDENTITY (1, 1),
 [SectionName] [varchar](100) NOT NULL,
 [SecOrder] [int] NOT NULL
 
 
 CONSTRAINT [PK_section] PRIMARY KEY CLUSTERED 
(
 [SectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO



CREATE TABLE [dbo].[Phrase](
 [PhraseId] [int] IDENTITY(1,1) NOT NULL,
 [PhraseName] [varchar](50) NULL,
 [SectionId] [int] NULL,
 [SortOrder] [int] NULL
 
 CONSTRAINT [PK_Phrase_fields] PRIMARY KEY CLUSTERED 
(
 [PhraseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Phrase]  WITH CHECK ADD  CONSTRAINT [fk_sectionId] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([SectionId])
GO



--data fix query for coupons.
INSERT INTO [dbo].[CouponCategories]
           ([CategoryId]
           ,[CouponId])


select distinct cc.CategoryId,c.couponid from AdCampaign a
inner join CampaignCategories cc on a.CampaignID = cc.CampaignId
inner join Coupon c on a.DisplayTitle = c.CouponTitle






GO
/****** Object:  StoredProcedure [dbo].[SearchCoupons]    Script Date: 8/2/2016 6:12:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId = 8,		@type = 1,		@keywords = N'1',		@distance = 1,		@Lat = N'1',		@Lon = N'1',		@UserId = N'1',		@FromRow = 1,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN

	select *, COUNT(*) OVER() AS TotalItems
	from (
	
	select vchr.CouponId as CouponId, CouponTitle,
	
	
	CouponImage1,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN c.Logo is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as LogoUrl,
	Price, Savings,SwapCost, CompanyId, CouponActiveMonth,CouponActiveYear
	
	from Coupon vchr
	inner join CouponCategories cc on cc.CouponId = vchr.CouponId and cc.CategoryId = @categoryId
	where (
		
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		
		(vchr.Approved = 1) and status = 3
	
		
		)
		group by vchr.CouponId, CouponTitle,vchr.CouponImage1,LogoUrl,Price, Savings,SwapCost,CompanyId,CouponActiveMonth,CouponActiveYear
		)as items
	order by Savings
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END




GO

/****** Object:  Table [dbo].[UserPurchasedCoupon]    Script Date: 8/3/2016 2:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserPurchasedCoupon](
	[CouponPurchaseId] [bigint] IDENTITY(1,1) NOT NULL,
	[CouponId] [bigint] NULL,
	[PurchaseDateTime] [datetime] NULL,
	[PurchaseAmount] [float] NULL,
	[UserId] [nvarchar](128) NULL,
	[IsRedeemed] [bit] NULL,
	[RedemptionDateTime] [datetime] NULL,
	[RedemptionOperator] [nvarchar](500) NULL,
 CONSTRAINT [PK_UserPurchasedCoupon] PRIMARY KEY CLUSTERED 
(
	[CouponPurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserPurchasedCoupon]  WITH CHECK ADD  CONSTRAINT [FK_UserPurchasedCoupon_UserPurchasedCoupon] FOREIGN KEY([CouponPurchaseId])
REFERENCES [dbo].[UserPurchasedCoupon] ([CouponPurchaseId])
GO

ALTER TABLE [dbo].[UserPurchasedCoupon] CHECK CONSTRAINT [FK_UserPurchasedCoupon_UserPurchasedCoupon]
GO



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserPurchasedCoupon
	DROP CONSTRAINT FK_UserPurchasedCoupon_UserPurchasedCoupon
GO
ALTER TABLE dbo.UserPurchasedCoupon ADD CONSTRAINT
	FK_UserPurchasedCoupon_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserPurchasedCoupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign ADD
	MaxDailyBudget float(53) NULL
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




GO
/****** Object:  StoredProcedure [dbo].[SearchCoupons]    Script Date: 8/12/2016 11:53:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId =2,		@type = 1,		@keywords = N'romantic',		@distance = 1,		@Lat = N'1',		@Lon = N'1',		@UserId = N'7e166b59-f848-4290-9d86-96207dcf1227',		@FromRow = 0,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN

declare @currentDate datetime
set @currentDate =  getdate()

	select *, COUNT(*) OVER() AS TotalItems
	from (
	
	select vchr.CouponId as CouponId, CouponTitle,
	
	
	CouponImage1,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN vchr.LogoUrl is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as LogoUrl,
	Price, Savings,SwapCost, CompanyId, CouponActiveMonth,CouponActiveYear
	,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))) eod,
	DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) strt

	
	from Coupon vchr
	inner join CouponCategories cc on cc.CouponId = vchr.CouponId and cc.CategoryId = @categoryId
	--left outer join [dbo].[UserPurchasedCoupon] upc on upc.couponID = vchr.couponid
	where (
		
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		
		(vchr.Approved = 1) and vchr.status = 3

		
		and 
		(--unlimited listing 
		( CouponListingMode = 2 and @currentDate between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1))))
			and  vchr.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = vchr.couponid and upc.userId = @UserId) 
		)
			--
		or
		--free more
		(
			CouponListingMode = 1 and @currentDate  between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,5))
			and vchr.CouponIssuedCount < 10
		)
		and -- keyword search
		(
			@keywords IS NULL OR (vchr.SearchKeywords like '%'+@keywords+'%' or vchr.CouponTitle like '%'+@keywords+'%' or LocationTitle like '%'+@keywords+'%' or LocationLine1 like '%'+@keywords+'%')
		)
		

		
		)
		group by vchr.CouponId, CouponTitle,vchr.CouponImage1,LogoUrl,Price, Savings,SwapCost,CompanyId,CouponActiveMonth,CouponActiveYear
		)as items
	order by Savings
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END







GO
/****** Object:  StoredProcedure [dbo].[SearchCampaigns]    Script Date: 8/12/2016 11:28:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 8 dec 2015
-- Description:	
-- =============================================
create PROCEDURE [dbo].[SearchCampaigns] 
--  exec [SearchCampaigns] 0,null,0,0,10,0
	-- Add the parameters for the stored procedure here
	@Status int,
	@keyword nvarchar(100),
	@companyId int,
	@fromRoww int,
	@toRow int,
	@adminMode bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--select @toRow, @fromRow
	--return


	select *, COUNT(*) OVER() AS TotalItems
	from (

    -- Insert statements for procedure here
	select  a.CampaignID, a.CampaignName,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) ) viewCountToday,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date)) viewCountYesterday,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid) viewCountAllTime,
	left(( select
			stuff((
					select ', ' + c1.CountryName, ', ' + ci.CityName
					from [AdCampaignTargetLocation] l1 
					left outer join country c1 on l1.countryid = c1.countryid
					left outer join city ci on l1.CityID = ci.CityID
					where l1.campaignid = l.campaignid
					order by c1.CountryName
					for xml path('')
				),1,1,'') as name_csv
			from [AdCampaignTargetLocation] l 
			where CampaignID = a.CampaignID
			group by l.campaignid  
		),30) +'..' Locationss,
		
		 StartDateTime, MaxBudget, MaxDailyBudget,AmountSpent,
		Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority
		
		
		
	
	 from AdCampaign a
	 
	--inner join AspNetUsers u on 
	
	
	--left outer join AdCampaignResponse aResp on a.CampaignID = aResp.CampaignID 
	where 
	(
		(@keyword is null or (a.CampaignName like '%'+ @keyword +'%' or a.DisplayTitle like '%'+ @keyword +'%'  ))
		and 
		( @Status = 0 or a.[status] = @Status)

	)
	


	group by CampaignID, CampaignName,StartDateTime,MaxBudget,MaxDailyBudget,AmountSpent,Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority
		)as items
	order by priority desc
	OFFSET @fromRoww ROWS
	FETCH NEXT @toRow ROWS ONLY
	
END


alter table Company
ADD 
BillingAddressLine1 nvarchar(150) NULL,
	BillingAddressLine2 nvarchar(150) NULL,
	BillingState nvarchar(50) NULL,
	BillingCountryId int NULL,
	BillingCityId int NULL,
	BillingZipCode nvarchar(50) NULL,
	BillingPhone nvarchar(50) NULL,
	BillingEmail nvarchar(200) NULL



	GO

	ALTER TABLE dbo.Company ADD
	TwitterHandle nvarchar(200) NULL,
	FacebookHandle nvarchar(200) NULL,
	InstagramHandle nvarchar(200) NULL,
	PinterestHandle nvarchar(200) NULL




	/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION


ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.DamImages
	(
	ImageId bigint NOT NULL IDENTITY (1, 1),
	ImageTitle nvarchar(500) NULL,
	ImageFileName nvarchar(500) NULL,
	ImageCategory int NULL,
	CreatedDateTime nchar(10) NULL,
	CompanyId int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.DamImages ADD CONSTRAINT
	PK_DamImages PRIMARY KEY CLUSTERED 
	(
	ImageId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.DamImages ADD CONSTRAINT
	FK_DamImages_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.DamImages SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProfileQuestion ADD
	CompanyId int NULL,
	AnswerNeeded int NULL,
	AsnswerCount int NULL
GO
ALTER TABLE dbo.ProfileQuestion ADD CONSTRAINT
	FK_ProfileQuestion_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProfileQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion ADD
	AnswerNeeded int NULL
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



GO

ALTER TABLE [dbo].Company
ALTER COLUMN ReferralCode [nvarchar](128) NULL




INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('Franchise_Account_Manager','Franchise Account Manager')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('Franchise_Creative','Franchise Creative')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('Franchise_Admin','Franchise Admin')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('Franchise_Approvers ','Franchise Approver')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('EndUser_Accounts','EndUser Accounts')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('EndUser_Creative','EndUser Creative')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('EndUser_Admin','EndUser Admin')
INSERT INTO [dbo].[AspNetRoles]            ([Id]           ,[Name])     VALUES           ('Supernova_Admin','Supernova Admin')




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion ADD
	AmountCharged float(53) NULL
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
