CREATE PROCEDURE [dbo].[usp_NutrientGroupDetail_SAV]
			  @NutrientGroupDetailID	INT
			, @NutrientGroupID			INT				= NULL
			, @AIMNutrientID				INT				= NULL
			, @SortOrder				INT				= NULL
			, @CFCUserID				INT				= NULL
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @ErrorNumber INT = 0
	DECLARE @RecordID INT = 0 
	DECLARE @ErrorMessage VARCHAR(200) = ''

	IF @NutrientGroupDetailID = 0
	BEGIN
		INSERT INTO dbo.NutrientGroupDetail
				(
				  NutrientGroupID
				, AIMNutrientID
				, SortOrder
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @NutrientGroupID
				, @AIMNutrientID
				, @SortOrder
				, GETDATE()
				, @CFCUserID
				)
		SET @RecordID = @@IDENTITY 
	END
	ELSE
	BEGIN
		UPDATE dbo.NutrientGroupDetail WITH (ROWLOCK)
			SET 
				  NutrientGroupID = ISNULL(@NutrientGroupID, NutrientGroupID)
				, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)
				, SortOrder = ISNULL(@SortOrder, SortOrder)
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			WHERE 
				NutrientGroupDetailID = @NutrientGroupDetailID

		SET @RecordID = @NutrientGroupDetailID
	END

	SELECT 
		  @RecordID AS RecordID
		, @ErrorNumber AS ErrorNumber
		, @ErrorMessage AS ErrorMessage

END