CREATE PROCEDURE [dbo].[usp_WebService_SAV]
			  @WebServiceID	INT			
			, @WebAddress		nVarchar(50)	= NULL
			, @Token			nVarchar(50)	= NULL			
			, @Name				nVarchar(50)	= NULL	
			, @CFCUserID		INT				= NULL
			, @DataSourceTypeID	INT				= NULL				
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @WebServiceID = 0
	BEGIN
		INSERT
			dbo.WebService 
				(
				 WebAddress
				, Token				
				, Name
				, CFCUserID
				, UpdateStamp				
				, DataSourceTypeID				
				)
		VALUES
				(
				  @WebAddress
				, @Token													
				, @Name	
				, @CFCUserID
				, GETDATE()
				, @DataSourceTypeID
				
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.WebService WITH(ROWLOCK)
				SET 
					  WebAddress = ISNULL(@WebAddress, WebAddress)
					, Token = ISNULL(@Token, Token)												
					, Name = ISNULL(@Name, Name)		
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)					
					
				WHERE 
					WebServiceID = @WebServiceID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

