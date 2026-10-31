CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroupDetail_DEL_SampleTestTypeGroupDetailID]
	  @SampleTestTypeGroupDetailID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.SampleTestTypeGroupDetail	
WHERE 
	SampleTestTypeGroupDetailID = @SampleTestTypeGroupDetailID
	
	
END