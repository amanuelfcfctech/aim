
CREATE PROCEDURE [dbo].[usp_CFCUser_SEL_LastActiveDataMap]
	 @CFCUserID						INT		= NULL

AS

--exec usp_CFCUser_SEL_LastActiveDataMap 2


BEGIN
SET NOCOUNT ON;

	SELECT
		C.LastActiveDataMapID
	FROM
		CFCUser.CFCUser C WITH(NOLOCK)
		
	WHERE
		C.CFCUserID = @CFCUserID


END

