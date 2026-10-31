CREATE PROCEDURE [dbo].[usp_APIPendingAIMMaterial_SEL_AIMMaterialIDDataSourceTypeIDIsActive]
		  @AIMMaterialID			INT
		, @DataSourceTypeID			INT
		, @IsActive					BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	  APAM.APIPendingAIMMaterialID
    , APAM.AIMMaterialID
    , APAM.DataSourceTypeID
    , APAM.IsActive
    , APAM.UpdateStamp
    , APAM.CFCUserID
FROM 
	dbo.APIPendingAIMMaterial APAM
WHERE 
	APAM.AIMMaterialID = @AIMMaterialID
	AND APAM.DataSourceTypeID = @DataSourceTypeID
	AND APAM.IsActive = @IsActive



END