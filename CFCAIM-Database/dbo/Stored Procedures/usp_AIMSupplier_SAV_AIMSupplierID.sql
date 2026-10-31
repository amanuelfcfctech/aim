CREATE PROCEDURE [dbo].[usp_AIMSupplier_SAV_AIMSupplierID]
			  @AIMSupplierID					INT			
			, @SupplierCode					NVARCHAR(20)
			, @SupplierName					VARCHAR(50)
			, @IsActive						BIT
			, @CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

DECLARE @SampleCodeList VARCHAR(1000) = ''
DECLARE @SampleCode VARCHAR (20) = ''
DECLARE @ID INT

IF @AIMSupplierID = 0
	BEGIN
		IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE SourceName = @SupplierName)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Supplier Name already exists. Please select a different name. Information is not saved!'
				SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE SourceName = @SupplierName)
			END

		ELSE IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE AIMSupplierCode = @SupplierName)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Supplier Code already exists. Please select a different code. Information is not saved!'
				SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE AIMSupplierCode = @SupplierName)
			END

		ELSE
			BEGIN
				INSERT
					dbo.AIMSource 
						(	
						    AIMSupplierCode						
						  , SourceName
						  , CFCUserID
						)
				VALUES
						(
							@SupplierCode
						  , @SupplierName
						  , @CFCUserID
						)
			END
	END
ELSE
	BEGIN

		IF @IsActive = 1
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE SourceName = @SupplierName and AIMSourceID <> @AIMSupplierID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Supplier Name already exists. Please select a different name. Information is not saved!'
						SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE SourceName = @SupplierName)
					END
				ELSE IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE AIMSupplierCode = @SupplierCode and AIMSourceID <> @AIMSupplierID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Supplier Code already exists. Please select a different code. Information is not saved!'
						SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE AIMSupplierCode = @SupplierCode)
					END
			END

		IF @IsActive = 0
			BEGIN
				
				IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.Sample S WITH(NOLOCK) 
							WHERE S.AIMSourceID = @AIMSupplierID and
								  S.IsActive = 1)
					BEGIN

						-- get list of sampleCodes
						declare tempcur scroll cursor for
						Select case when s.SampleCode is null then s.SampleID 
							else case when s.SampleCode = '' then s.SampleID 
							else s.SampleCode end end as SampleCode 
						from dbo.Sample s with (nolock) 
							where s.AIMSourceID = @AIMSupplierID and S.IsActive = 1
						Open tempcur
						Fetch next from tempcur into @SampleCode
						While @@FETCH_STATUS = 0
							Begin
								SET @SampleCodeList = @SampleCodeList + @SampleCode + ', '
								Fetch next from tempcur into @SampleCode
							End
						Close tempcur
						Deallocate tempcur
						IF RIGHT(@SampleCodeList,2) = ', '
							SET @SampleCodeList = left(@SampleCodeList, len(@SampleCodeList) - 1)

						SET @ErrorNumber = 2
						SET @ErrorMessage = 'Unable to inactivate supplier because it is being used in the sample table (Samples: ' + @SampleCodeList + ')' 
					END

				If @ErrorNumber = 0
					
					-- 10/13/17
					--IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.AIMSourceLocation ASL WITH(NOLOCK) 
					--	WHERE ASL.AIMSourceID = @AIMSupplierID and
					--		  ASL.IsActive = 1)
					--BEGIN
					--	SET @ErrorNumber = 1
					--	SET @ErrorMessage = 'Unable to inactivate supplier because it is used in the Supplier Location table.'
					--END
					UPDATE
						dbo.AIMSourceLocation with (rowlock)
					SET 
						IsActive = 0
						, UpdateStamp = GETDATE()
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID) 
					WHERE
						AIMSourceID = @AIMSupplierID

			END
	END
IF @ErrorNumber = 0
	BEGIN
		UPDATE
			dbo.AIMSource WITH(ROWLOCK)
				SET 
					  AIMSupplierCode = ISNULL(@SupplierCode, AIMSupplierCode) 
					, SourceName = ISNULL(@SupplierName, SourceName)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					AIMSourceID = @AIMSupplierID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue as ReturnIntegerValue

END