CREATE PROCEDURE [dbo].[usp_Control_SEL_ControlID]
		    @ControlID			INT
		  
-- exec usp_Control_SEL_ControlID 12

AS
BEGIN
SET NOCOUNT ON;

SELECT
	 C.*

FROM 
	dbo.[Control] C WITH(NOLOCK)
	
WHERE 
   C.ControlID = @ControlID


END