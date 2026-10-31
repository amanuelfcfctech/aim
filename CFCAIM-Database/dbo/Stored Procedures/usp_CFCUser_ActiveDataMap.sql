CREATE PROCEDURE [dbo].[usp_CFCUser_ActiveDataMap]
			  @CFCUserID						INT		= NULL
			, @LastActiveDataMapID				INT		= NULL
AS

BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		CFCUser.CFCUser WITH(ROWLOCK)
			SET 
					LastActiveDataMapID = ISNULL(@LastActiveDataMapID, LastActiveDataMapID)					
			WHERE 
				CFCUserID = @CFCUserID
		SET @RecordID = @@IDENTITY 
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

