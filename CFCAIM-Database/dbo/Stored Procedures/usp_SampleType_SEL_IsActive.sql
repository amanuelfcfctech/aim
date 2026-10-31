CREATE PROCEDURE [dbo].[usp_SampleType_SEL_IsActive]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_SampleType_SEL_IsActive true

SELECT 
		  ST.*
		, U.UserName

	FROM 
		dbo.SampleType ST WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON ST.CFCUserID = U.CFCUserID		

	WHERE
		ST.IsActive = @IsActive 
		-----and DST.IsUserDefined = 0

ORDER BY SampleTypeName

END