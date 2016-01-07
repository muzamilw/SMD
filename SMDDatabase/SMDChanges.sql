
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

-- 07-Jan-16 by Baqer STARTS

GO

/****** Object:  Table [dbo].[TransactionLog]    Script Date: 07-Jan-16 7:20:30 PM ******/
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

