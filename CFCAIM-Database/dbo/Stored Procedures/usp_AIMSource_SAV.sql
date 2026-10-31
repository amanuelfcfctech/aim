CREATE PROCEDURE [dbo].[usp_AIMSource_SAV]
			  @AIMSourceID					INT
			, @SourceName					VARCHAR(50)
			, @IsActive						BIT
			, @CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

IF @AIMSourceID = 0
	BEGIN
		INSERT		
			dbo.AIMSource 
				(
				    SourceName
				  , CFCUserID
				)
		VALUES
				(
				    @SourceName
				  , @CFCUserID
				)
	END
ELSE
	BEGIN
		IF @IsActive = 0
			BEGIN
				IF EXISTS(SELECT TOP 1 SL.AIMSourceID FROM dbo.AIMSourceLocation SL WITH(NOLOCK)
							WHERE SL.AIMSourceID = @AIMSourceID and 
								  SL.IsActive = 1)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to inactivate Supplier because an AIM Supplier Location is assigned to it. You must first inactivate the supplier-location(s).'
						SET @ReturnIntegerValue = (SELECT TOP 1 SL.AIMSourceID FROM dbo.AIMSourceLocation SL WITH(NOLOCK)
							WHERE SL.AIMSourceID = @AIMSourceID and 
								  SL.IsActive = 1)
					END			
				
			END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AIMSource WITH(ROWLOCK)
						SET 
							  SourceName = ISNULL(@SourceName, SourceName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
						WHERE 
							AIMSourceID = @AIMSourceID
			END
	END

SELECT
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue as ReturnIntegerValue

END