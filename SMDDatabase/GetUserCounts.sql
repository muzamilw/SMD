GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserCounts] 
	
AS
BEGIN
	
	select title, (case when [7] is not null then [7] else 0 end ) '7Days', (case when [14] is not null then [14] else 0 end ) '14Days' , (case when [30] is not null then [30] else 0 end ) '30Days', (case when [60] is not null then [60] else 0 end ) '60Days'  from (
	Select count(u.id) stats, '7' Granual , 'Last Log in Date' title
	from AspNetUsers u
	where u.LastLoginTime > getdate()-7
	union
	Select count(us.id) stats , '7' Granual , 'Registration Date' title
	from AspNetUsers us 
	where us.CreatedDateTime > getdate()-7 
	union
	Select count(c.CompanyId) stats, '7' Granual , 'Deleted Account Date' title
	from Company c 
	where c.DeleteDate > getdate()-7
	union

	Select count(u.id) stats, '14' Granual , 'Last Log in Date' title
	from AspNetUsers u
	where u.LastLoginTime > getdate()-14
	union
	Select count(us.id) stats , '14' Granual , 'Registration Date' title
	from AspNetUsers us 
	where us.CreatedDateTime > getdate()-14 
	union
	Select count(c.CompanyId) stats, '14' Granual , 'Deleted Account Date' title
	from Company c 
	where c.DeleteDate > getdate()-14
	union

	Select count(u.id) stats, '30' Granual , 'Last Log in Date' title
	from AspNetUsers u
	where u.LastLoginTime > getdate()-30
	union
	Select count(us.id) stats , '30' Granual , 'Registration Date' title
	from AspNetUsers us 
	where us.CreatedDateTime > getdate()-30 
	union
	Select count(c.CompanyId) stats, '30' Granual , 'Deleted Account Date' title
	from Company c 
	where c.DeleteDate > getdate()-30

	union

	Select count(u.id) stats, '60' Granual , 'Last Log in Date' title
	from AspNetUsers u
	where u.LastLoginTime > getdate()-60
	union
	Select count(us.id) stats , '60' Granual , 'Registration Date' title
	from AspNetUsers us 
	where us.CreatedDateTime > getdate()-60
	union
	Select count(c.CompanyId) stats, '60' Granual , 'Deleted Account Date' title
	from Company c 
	where c.DeleteDate > getdate()-60
	
	
	) as rec
	pivot
	(
	 max(stats)
    for Granual in ([7], [14],[30], [60]) 
	) piv

	
END

--EXEC GetUserCounts

