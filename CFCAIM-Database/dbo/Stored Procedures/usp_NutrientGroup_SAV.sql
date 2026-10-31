CREATE PROCEDURE [dbo].[usp_NutrientGroup_SAV]
			  @NutrientGroupID		INT
			, @NutrientGroupName	NVARCHAR(50)	= NULL
			, @IsActive				BIT				= NULL
			, @CFCUserID			INT				= NULL
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @ErrorNumber INT = 0
	DECLARE @RecordID INT = 0 
	DECLARE @ErrorMessage VARCHAR(200) = ''

	IF @NutrientGroupID = 0
	BEGIN
		INSERT INTO dbo.NutrientGroup
				(
				  NutrientGroupName
				, IsActive
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @NutrientGroupName
				, @IsActive
				, GETDATE()
				, @CFCUserID
				)
		SET @RecordID = @@IDENTITY 
	END
	ELSE
	BEGIN
		UPDATE dbo.NutrientGroup WITH (ROWLOCK)
			SET 
				  NutrientGroupName = ISNULL(@NutrientGroupName, NutrientGroupName)
				, IsActive = ISNULL(@IsActive, IsActive)
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			WHERE 
				NutrientGroupID = @NutrientGroupID

		SET @RecordID = @@IDENTITY 
	END

	SELECT 
		  @RecordID AS RecordID
		, @ErrorNumber AS ErrorNumber
		, @ErrorMessage AS ErrorMessage

END