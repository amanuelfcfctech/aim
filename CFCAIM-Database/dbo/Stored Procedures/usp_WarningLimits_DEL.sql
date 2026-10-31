create PROCEDURE [dbo].[usp_WarningLimits_DEL]
				@WarningLimitsID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.WarningLimits WITH(ROWLOCK)
WHERE
	WarningLimitsID = @WarningLimitsID

END
