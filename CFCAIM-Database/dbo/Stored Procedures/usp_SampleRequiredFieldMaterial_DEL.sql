CREATE PROCEDURE [dbo].[usp_SampleRequiredFieldMaterial_DEL]
		@SampleRequiredFieldMaterialID		INT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_SampleRequiredFieldMaterial_DEL 1

DELETE
	dbo.SampleRequiredFieldMaterial
WHERE
	SampleRequiredFieldMaterialID = @SampleRequiredFieldMaterialID

END