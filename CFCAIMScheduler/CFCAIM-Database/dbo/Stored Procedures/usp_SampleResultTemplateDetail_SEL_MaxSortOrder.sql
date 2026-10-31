CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_SEL_MaxSortOrder]			  
			 @SampleResultTemplateID	INT					
	
-- exec 	[usp_SampleResultTemplateDetail_SEL_MaxSortOrder] 14	
				
AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @RecordID = (SELECT 
					ISNULL(Max(SortOrder),0) as MaxSortOrder 
				FROM 
					dbo.SampleResultTemplateDetail SRTD
				WHERE 							
					SRTD.SampleResultTemplateID = @SampleResultTemplateID)
	

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END