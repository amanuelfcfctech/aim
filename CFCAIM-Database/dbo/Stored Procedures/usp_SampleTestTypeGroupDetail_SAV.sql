CREATE PROCEDURE  [dbo].[usp_SampleTestTypeGroupDetail_SAV]
			  @SampleTestTypeGroupDetailID		INT
			, @SampleTestTypeGroupID			INT
			, @SampleTestTypeID					INT
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SampleTestTypeGroupDetailID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SampleTestTypeGroupDetailID					
				FROM 
					dbo.SampleTestTypeGroupDetail STTGD
				WHERE 
						STTGD.SampleTestTypeGroupID = @SampleTestTypeGroupID and
						STTGD.SampleTestTypeID = @SampleTestTypeID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Test Type already exists in the group. Information is not saved.'
				SET @RecordID = (SELECT 
									SampleTestTypeGroupDetailID 
								FROM 
									dbo.SampleTestTypeGroupDetail STTGD
								WHERE 
									STTGD.SampleTestTypeGroupID = @SampleTestTypeGroupID and
									STTGD.SampleTestTypeID = @SampleTestTypeID
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SampleTestTypeGroupDetail
						(																					
						SampleTestTypeGroupID												
						, SampleTestTypeID												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						@SampleTestTypeGroupID
						, @SampleTestTypeID
						, GETDATE()
						, @CFCUserID						
						, 1
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
		BEGIN
			UPDATE
				dbo.SampleTestTypeGroupDetail WITH(ROWLOCK)
					SET 
						  SampleTestTypeGroupID = ISNULL(@SampleTestTypeGroupID, SampleTestTypeGroupID)						
						, SampleTestTypeID = ISNULL(@SampleTestTypeID, SampleTestTypeID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SampleTestTypeGroupDetailID = @SampleTestTypeGroupDetailID

			SET @RecordID = @SampleTestTypeGroupDetailID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END