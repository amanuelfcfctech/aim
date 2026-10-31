create PROCEDURE [dbo].[usp_QueryField_SAV]
			  @QueryFieldID					INT
			, @Field						NVARCHAR(20)	= NULL
			, @IsActive						BIT				= NULL
			, @CFCUserID					INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @QueryFieldID = 0
	BEGIN
		INSERT
			dbo.QueryField 
				(
				  Field
				, IsActive
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @Field
				, @IsActive
				, GETDATE()
				, @CFCUserID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.QueryField WITH(ROWLOCK)
				SET 
					  Field = ISNULL(@Field, Field)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					QueryFieldID = @QueryFieldID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
