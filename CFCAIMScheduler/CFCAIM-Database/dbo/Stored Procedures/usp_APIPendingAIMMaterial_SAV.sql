create PROCEDURE [dbo].[usp_APIPendingAIMMaterial_SAV]
			  @APIPendingAIMMaterialID	INT
			, @AIMMaterialID			INT		= NULL
			, @DataSourceTypeID			INT		= NULL
			, @IsActive					BIT		= NULL
			, @CFCUserID				INT		= NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''
DECLARE @IsUpdate			BIT = 0

IF @APIPendingAIMMaterialID > 0 
	BEGIN
		SET @IsUpdate = 1
	END
ELSE
	BEGIN
		IF NOT EXISTS(SELECT 
				APIPendingAIMMaterialID
			FROM
				dbo.APIPendingAIMMaterial
			WHERE
				AIMMaterialID = @AIMMaterialID
				AND DataSourceTypeID = @DataSourceTypeID
				AND IsActive = 1)
			BEGIN
				INSERT INTO 
					dbo.APIPendingAIMMaterial
						(
						  AIMMaterialID
						, DataSourceTypeID
						, IsActive
						, UpdateStamp
						, CFCUserID
						)
				VALUES
						(
						  @AIMMaterialID
						, @DataSourceTypeID
						, @IsActive
						, GETDATE()
						, @CFCUserID
						)
				SET @APIPendingAIMMaterialID = @@IDENTITY 
			END
		ELSE
			BEGIN
				SET @IsUpdate = 1
			END
	END

IF @IsUpdate = 1
	BEGIN
		UPDATE 
			dbo.APIPendingAIMMaterial
		SET 
			  AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
			, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
			, IsActive = ISNULL(@IsActive, IsActive)
			, UpdateStamp = GETDATE()
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
		WHERE
			APIPendingAIMMaterialID = @APIPendingAIMMaterialID
	END

END

SELECT 
	  @APIPendingAIMMaterialID AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage