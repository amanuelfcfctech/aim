CREATE PROCEDURE [dbo].[usp_QueryTemplateDetailNutrient_UPD_DriverAIMNutrientID]
			  @QueryTemplateID				INT
			, @DriverAIMNutrientID			INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


BEGIN
	
	UPDATE	
		dbo.QueryTemplateDetailNutrient
	SET
		DriverAIMNutrientID = @DriverAIMNutrientID

	FROM
		dbo.QueryTemplateDetail Q
	INNER JOIN
		dbo.QueryTemplateDetailNutrient N ON
			Q.QueryTemplateDetailID = N.QueryTemplateDetailID
	WHERE
		Q.QueryTemplateID = @QueryTemplateID
		

END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END