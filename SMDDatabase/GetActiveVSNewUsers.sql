
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetActiveVSNewUsers 
	
AS
BEGIN
	DECLARE @T1 TABLE (
ActiveStats Bigint,
Granual  VARCHAR(200)

)


insert into @T1 
Select count(u.id) ActiveStats, '1 Day' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-1
union
Select count(u.id) ActiveStats, '7 Days' Granual
from AspNetUsers u
where u.LastLoginTime > getdate()-7
union
Select count(u.id) ActiveStats, '14 Days' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-14
union
Select count(u.id) ActiveStats, '1 Month' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-30
union
Select count(u.id) ActiveStats, '3 Months' Granual 
from AspNetUsers u
where u.LastLoginTime > getdate()-90


DECLARE @T2 TABLE (
NewStats Bigint,
Granual  VARCHAR(200)

)


insert into @T2 
Select count(u.id) NewStats, '1 Day' Granual 
from AspNetUsers u
where u.CreatedDateTime > getdate()-1
union
Select count(u.id) NewStats, '7 Days' Granual
from AspNetUsers u
where u.CreatedDateTime > getdate()-7
union
Select count(u.id) NewStats, '14 Days' Granual 
from AspNetUsers u
where u.CreatedDateTime > getdate()-14
union
Select count(u.id) NewStats, '1 Month' Granual 
from AspNetUsers u
where u.CreatedDateTime > getdate()-30
union
Select count(u.id) NewStats, '3 Months' Granual 
from AspNetUsers u
where u.CreatedDateTime > getdate()-90


select t.ActiveStats, t2.* from  @T1 t
inner join @T2 t2 on t.Granual = t2.Granual

 
END
GO

--EXEC GetActiveVSNewUsers