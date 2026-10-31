CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_ALL_IsActive]
		@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;


--exec usp_AIMSourceLocation_SEL_ALL_IsActive 1


SELECT
	  	ROW_NUMBER() OVER(ORDER BY AIMSupplierLocationCode) AS ID
	  , S.SourceLocationName as SupplierLocationName	  
	  , S.AIMSupplierLocationCode

FROM 
	dbo.AIMSourceLocation S WITH(NOLOCK)

WHERE 
	S.IsActive = @IsActive

GROUP BY 
		S.AIMSupplierLocationCode
		, S.SourceLocationName

END