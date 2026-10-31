CREATE PROCEDURE [dbo].[usp_SampleTestTypeDetail_SEL_MaxSortOrder]			  
			 @SampleTestTypeID	INT					
					
AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @RecordID = (SELECT 
					ISNULL(Max(SortOrder),0) as MaxSortOrder 
				FROM 
					dbo.SampleTestTypeDetail STTD
				WHERE 							
					STTD.SampleTestTypeID = @SampleTestTypeID)
	

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END