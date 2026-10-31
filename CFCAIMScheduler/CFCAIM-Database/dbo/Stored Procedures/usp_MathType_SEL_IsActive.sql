CREATE PROCEDURE [dbo].[usp_MathType_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  LD.MathTypeID
	, LD.MathTypeName
	, LD.IsActive
	, LD.UpdateStamp
	, LD.CFCUserID
	, LD.IsSystem
	, LD.Equation
	, U.UserName

FROM 
	dbo.MathType LD WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON LD.CFCUserID = U.CFCUserID

WHERE 
	LD.IsActive = @IsActive
END
