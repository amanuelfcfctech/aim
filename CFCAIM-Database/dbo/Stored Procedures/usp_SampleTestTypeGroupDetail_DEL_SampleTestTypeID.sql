CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroupDetail_DEL_SampleTestTypeID]
	  @SampleTestTypeID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.SampleTestTypeGroupDetail	
WHERE 
	SampleTestTypeID = @SampleTestTypeID 
	
	
END