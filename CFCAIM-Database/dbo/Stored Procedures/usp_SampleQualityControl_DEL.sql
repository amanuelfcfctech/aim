CREATE PROCEDURE [dbo].[usp_SampleQualityControl_DEL]
	  @SampleQualityControlID		INT
	
AS
BEGIN
	SET NOCOUNT ON;			

	DELETE 
	
	FROM 
		dbo.SampleQualityControl with (rowlock) 
	
	WHERE 
		SampleQualityControlID = @SampleQualityControlID

END