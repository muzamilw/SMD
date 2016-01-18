
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

/* Added By Khurram (14 Jan 2016) - Ends */

/* Added By Khurram (18 Jan 2016) - Start (Need to update on live db server) */

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
	'', sq.LeftPicturePath as SqLeftImagePath, sq.RightPicturePath as SqRightImagePath, '', 
	NULL, '', NULL, '', NULL, '',NULL, '', NULL, '', NULL, '', -- PQAnswers
	(((row_number() over (order by sq.sqid) * 10) + 2) + ISNULL(sq.priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)

	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, sqp.LeftPicturePath as SqLeftImagePath, sqp.RightPicturePath as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	(select (ISNULL(weightage, 0) + ISNULL(sq.priority,0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	   and sq.Status = 3 -- live
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
	OFFSET @FromRow ROWS -- skip 10 rows
	FETCH NEXT @ToRow ROWS ONLY -- take 10 rows
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
	(((row_number() over (order by sq.sqid) * 10) + 2) + isnull(sq.Priority, 0)) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)

	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	NULL, 
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
	END as SqRightImagePath, NULL, 
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -- PQAnswers
	(select (isnull(weightage, 0) + isnull(sq.Priority, 0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage
	from surveyquestion sq	
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID) = 0)
	  and
	  sq.Status = 3 -- live
    ) 
	

	select * from surveyquestions
)
GO
/* Added By Khurram (18 Jan 2016) - End */
