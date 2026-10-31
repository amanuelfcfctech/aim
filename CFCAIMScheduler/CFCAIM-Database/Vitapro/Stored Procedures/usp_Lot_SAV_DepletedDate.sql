CREATE PROCEDURE [Vitapro].[usp_Lot_SAV_DepletedDate]
			  @SampleID					INT
			, @CFCUserID				INT
			, @ClearDepletedDate		BIT = NULL
AS

--exec Vitapro.usp_Lot_SAV_DepletedDate 1234, 1

BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @Sql			VARCHAR(1000)
DECLARE @DateString	  VARCHAR(10)

BEGIN

	SET @Sql = 'UPDATE	Vitapro.Lot WITH(ROWLOCK) SET '

	If @ClearDepletedDate = 1 
		SET @Sql = @Sql + 'DepletedDate = ''' +  CONVERT(VARCHAR, getdate(), 121) + ''''			
	Else
		SET @Sql = @Sql + 'DepletedDate = NULL'
	
	SET @Sql = @Sql + ', CFCUserID = ' + cast(@CFCUserID as varchar) 
	SET @Sql = @Sql + ', UpdateStamp = ''' +  CONVERT(VARCHAR, getdate(), 121) + ''''
	SET @Sql = @Sql + '	WHERE SampleID = ' + cast(@SampleID as varchar)

	EXEC (@Sql)
	--print @Sql

	SET @RecordID = @SampleID	

END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END