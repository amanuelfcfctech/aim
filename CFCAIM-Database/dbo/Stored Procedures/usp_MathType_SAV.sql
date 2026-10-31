CREATE PROCEDURE [dbo].[usp_MathType_SAV]
			  @MathTypeID			INT
			, @MathTypeName			VARCHAR(50) = NULL			
			, @IsActive				BIT			= NULL												
			, @CFCUserID			INT			= NULL
			, @IsSystem				BIT			= NULL
			, @Equation				VARCHAR(200) = NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @MathTypeID = 0
	BEGIN
		INSERT
			dbo.MathType
				(
				  MathTypeName			
				, IsActive				
				, CFCUserID
				, UpdateStamp
				, IsSystem	
				, Equation			
				)
		VALUES
				(
				  @MathTypeName				
				, @IsActive				
				, @CFCUserID
				, GETDATE()
				, @IsSystem
				, @Equation
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.MathType WITH(ROWLOCK)
				SET 
					  MathTypeName = ISNULL(@MathTypeName, MathTypeName)					
					, IsActive = ISNULL(@IsActive, IsActive)											
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, IsSystem = ISNULL(@IsSystem, IsSystem)			
					, Equation = ISNULL(@Equation, Equation)			
					
				WHERE 
					MathTypeID = @MathTypeID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END