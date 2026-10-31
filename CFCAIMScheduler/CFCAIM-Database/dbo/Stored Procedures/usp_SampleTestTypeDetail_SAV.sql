CREATE PROCEDURE [dbo].[usp_SampleTestTypeDetail_SAV]

			  @SampleTestTypeDetailID				INT
			, @SampleTestTypeID						INT				= NULL
			, @DataSourceTypeID						INT				= NULL
			, @SortOrder							INT				= NULL			
			, @UpdateStamp							DATETIME		= NULL
			, @CFCUserID							INT				= NULL		
			, @IsActive								BIT				= NULL
	
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleTestTypeDetailID = 0
	BEGIN

		IF EXISTS(SELECT 
						SampleTestTypeDetailID 
				FROM 
						dbo.SampleTestTypeDetail SLLD
				WHERE 							
						SLLD.SampleTestTypeID = @SampleTestTypeID and 
						SLLD.DataSourceTypeID = @DataSourceTypeID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This data source already exists on the lab list.'
			END
		ELSE
			BEGIN			

				INSERT
					dbo.SampleTestTypeDetail
						(
						SampleTestTypeID
						, DataSourceTypeID
						, SortOrder
						, CFCUserID
						, UpdateStamp	
						, IsActive
						)
				VALUES
						(
						@SampleTestTypeID
						, @DataSourceTypeID
						, @SortOrder									
						, @CFCUserID
						, GetDate()
						, @IsActive
						)
				SET @RecordID = @@IDENTITY 
			END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			UPDATE
				dbo.SampleTestTypeDetail WITH(ROWLOCK)
					SET 
						  SampleTestTypeID = ISNULL(@SampleTestTypeID, SampleTestTypeID)
						, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)					
						, SortOrder = ISNULL(@SortOrder, SortOrder)														
						, UpdateStamp = GETDATE()							
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)											
						, IsActive = ISNULL(@IsActive, IsActive)											
					WHERE 
						SampleTestTypeDetailID = @SampleTestTypeDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END