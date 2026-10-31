CREATE PROCEDURE [Vitapro].[usp_Lot_SAV_ProductionLine_Status]
			  @SampleID					INT
			, @LineNumber				INT
			, @ProductionStatus			INT
			, @CFCUserID				INT
AS

--exec Vitapro.usp_Lot_SAV_ProductionLine_Status 1234, 1, 4, 1

BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @Sql			VARCHAR(1000)
DECLARE @DateString	  VARCHAR(10)

BEGIN

	SET @Sql = 'UPDATE	Vitapro.Lot WITH(ROWLOCK) SET '

	IF @LineNumber = 1 
		SET @Sql = @Sql + ' 
					ProductionStatusLine1 = ' + cast(@ProductionStatus as varchar)
	IF @LineNumber = 2 
		SET @Sql = @Sql + ' 
					ProductionStatusLine2 = ' + cast(@ProductionStatus as varchar)
	IF @LineNumber = 3 
		SET @Sql = @Sql + ' 
					ProductionStatusLine3 = ' + cast(@ProductionStatus as varchar)
	IF @LineNumber = 4 
		SET @Sql = @Sql + ' 	
					ProductionStatusLine4 = ' + cast(@ProductionStatus as varchar)


	SET @Sql = @Sql + ' , CFCUserID = ' + cast(@CFCUserID as varchar) 
	SET @Sql = @Sql + ' , UpdateStamp = ''' +  CONVERT(VARCHAR, getdate(), 121) + ''''
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