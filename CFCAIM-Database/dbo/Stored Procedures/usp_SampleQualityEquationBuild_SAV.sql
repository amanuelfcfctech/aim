CREATE PROCEDURE [dbo].[usp_SampleQualityEquationBuild_SAV]
			  @SampleQualityEquationBuildID			INT
			, @SampleQualityEquationID				INT			
			, @SampleQualityParameterID				INT
			, @ValueString							VARCHAR(50) = NULL		
			, @Status								BIT			= NULL			
			, @CFCUserID							INT			= NULL			
			


			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleQualityEquationBuildID = 0
	BEGIN
		INSERT
			dbo.SampleQualityEquationBuild
				(
				  SampleQualityEquationID
				, SampleQualityParameterID
				, ValueString						
				, [Status]
				, CFCUserID
				, UpdateStamp				
							
				)
		VALUES
				(
				  @SampleQualityEquationID	
				, @SampleQualityParameterID			
				, @ValueString							
				, @Status
				, @CFCUserID
				, GETDATE()				
				
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleQualityEquationBuild WITH(ROWLOCK)
				SET 
					  SampleQualityEquationID = ISNULL(@SampleQualityEquationID, SampleQualityEquationID)					
					, SampleQualityParameterID = ISNULL(@SampleQualityParameterID, SampleQualityParameterID)					
					, ValueString = ISNULL(@ValueString, ValueString)															
					, [Status] = ISNULL(@Status, [Status])															
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()					
					
					
				WHERE 
					SampleQualityEquationBuildID = @SampleQualityEquationBuildID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END