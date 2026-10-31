CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_SampleTestTypeGroupID]
	@SampleTestTypeGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	SampleTestTypeGroupID = @Value
WHERE 
	SampleTestTypeGroupID = @SampleTestTypeGroupID


END