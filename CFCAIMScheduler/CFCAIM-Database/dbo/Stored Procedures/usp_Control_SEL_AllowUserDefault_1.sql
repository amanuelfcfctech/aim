CREATE PROCEDURE [dbo].[usp_Control_SEL_AllowUserDefault]
		 @AllowUserDefault			BIT
		  

AS
BEGIN
SET NOCOUNT ON;

SELECT
	 C.*
FROM 
	dbo.[Control] C WITH(NOLOCK)
	
WHERE 
   C.AllowUserDefault = @AllowUserDefault


END