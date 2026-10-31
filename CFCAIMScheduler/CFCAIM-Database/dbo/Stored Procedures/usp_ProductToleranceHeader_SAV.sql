CREATE PROCEDURE [dbo].[usp_ProductToleranceHeader_SAV]
			  @ProductToleranceHeaderID			INT			
			, @ProductToleranceHeaderName		NVARCHAR(50)	= NULL				
			, @AIMMaterialID					INT				= NULL
			, @AIMMaterialCode					NVARCHAR(20)	= NULL
			, @CFCUserID						INT				= NULL		
			, @UpdateStamp						DATETIME		= NULL
			, @SpecFrom							INT				= NULL   --1=FRM, 2=AIM
			

			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ProductToleranceHeaderID = 0
	BEGIN
		INSERT
			dbo.ProductToleranceHeader
				(
				  ProductToleranceHeaderName					
				, AIMMaterialID		
				, AIMMaterialCode
				, CFCUserID
				, UpdateStamp
				, SpecFrom
				)
		VALUES
				(
				  @ProductToleranceHeaderName						
				, @AIMMaterialID
				, @AIMMaterialCode
				, @CFCUserID
				, GetDate()
				, @SpecFrom
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.ProductToleranceHeader 
				SET 
					  ProductToleranceHeaderName = ISNULL(@ProductToleranceHeaderName, ProductToleranceHeaderName)					
					, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
					, AIMMaterialCode = ISNULL(@AIMMaterialCode, AIMMaterialCode)
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, SpecFrom = ISNULL(@AIMMaterialCode, SpecFrom)
					
					
				WHERE 
					ProductToleranceHeaderID = @ProductToleranceHeaderID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END