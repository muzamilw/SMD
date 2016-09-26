-- updated on 26/9/2016

USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetActiveVSNewUsers]    Script Date: 9/26/2016 1:38:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetActiveVSNewUsers] 
	
AS
BEGIN
	
Select count(u.id) ActiveStats, (Select count(us.id) from AspNetUsers us where us.CreatedDateTime > getdate()-1) as NewStats, '1 Day' Granual
from AspNetUsers u
where u.LastLoginTime > getdate()-1
union
Select count(u.id) ActiveStats, (Select count(us.id) from AspNetUsers us where us.CreatedDateTime > getdate()-7) as NewStats, '7 Days' Granual
from AspNetUsers u
where u.LastLoginTime > getdate()-7
union
Select count(u.id) ActiveStats, (Select count(us.id) from AspNetUsers us where us.CreatedDateTime > getdate()-14) as NewStats, '14 Days' Granual
from AspNetUsers u
where u.LastLoginTime > getdate()-14
union
Select count(u.id) ActiveStats, (Select count(us.id) from AspNetUsers us where us.CreatedDateTime > getdate()-30) as NewStats, '1 Month' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-30
union
Select count(u.id) ActiveStats, (Select count(us.id) from AspNetUsers us where us.CreatedDateTime > getdate()-90) as NewStats, '3 Months' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-90


END

--EXEC GetActiveVSNewUsers