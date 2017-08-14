-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE GetRandomGameByUser 
	-- Add the parameters for the stored procedure here
	@ExistingGameId bigint = 0, 
	@UserId nvarchar(128) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


SELECT [GameId]
      ,[GameName]
      ,[Status]
      ,[AgeRangeStart]
      ,[AgeRangeEnd]
      ,[GameType]
      ,[Complexity]
      ,[GameUrl]
      ,[GameInstructions]
      ,[GameSmallImage]
      ,[GameLargeImage]
	   ,[PlayTime]
      ,[Score]
      ,[Accuracy]

from
(SELECT TOP 1 * FROM game where status=1 and gameid <> @ExistingGameId ORDER BY NEWID()) g
 left outer join
			
				(
					select gameid as rgameid,Score,PlayTime,Accuracy, row_number() over (partition by GameId order by ResponseDateTime desc) as rn
					from UserGameResponse gr  --where userid = @UserID 
				
					group by gr.GameId,gr.score,gr.ResponseDateTime,gr.PlayTime,gr.Accuracy
						

				) dans on dans.rn = 1  and g.gameid = dans.rgameid

END
GO
