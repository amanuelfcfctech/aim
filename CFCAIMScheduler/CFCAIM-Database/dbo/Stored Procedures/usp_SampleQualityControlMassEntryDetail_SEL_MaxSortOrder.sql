CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_SEL_MaxSortOrder]			  
			 @SampleQualityControlMassEntryID	INT					
	

				
AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @RecordID = (SELECT 
					ISNULL(Max(SortOrder),0) as MaxSortOrder 
				FROM 
					dbo.SampleQualityControlMassEntryDetail MED
				WHERE 							
					MED.SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID)
	

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END