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
	
GO
COMMIT
