CREATE PROCEDURE [dbo].[usp_MaterialType_SEL_IsActive]
			@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	MT.*

FROM 
	dbo.MaterialType MT
WHERE 
	MT.IsActive = @IsActive
END
