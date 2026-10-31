CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SAV_AIMSourceLocationID]
			  @AIMSourceID							INT
			, @AIMSourceLocationID					INT
			, @AIMSupplierLocationCode				NVARCHAR(20)
			, @SourceLocationName					VARCHAR(50)
			, @IsActive								BIT
			, @CFCUserID							INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

DECLARE @SampleCodeList VARCHAR(1000) = ''
DECLARE @SampleCode VARCHAR (20) = ''

IF @AIMSourceLocationID = 0
	BEGIN

		IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
						WHERE	AIMSourceID = @AIMSourceID 
								and SourceLocationName = @SourceLocationName)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'Supplier Location Name already exists for this supplier. Please select a different name. Information is not saved!'				
			END
		ELSE IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
						WHERE	AIMSourceID = @AIMSourceID 
								and AIMSupplierLocationCode = @AIMSupplierLocationCode)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'Supplier Location Code already exists for this supplier. Please select a different code. Information is not saved!'				
			END
		ELSE

			INSERT
				dbo.AIMSourceLocation 
					(
					  AIMSourceID
					, AIMSupplierLocationCode
					, SourceLocationName
					, CFCUserID
					)
			VALUES
					(
					  @AIMSourceID
					, @AIMSupplierLocationCode
					, @SourceLocationName
					, @CFCUserID
					)
	END
ELSE
	BEGIN
		IF @IsActive = 1
			BEGIN

				IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
							WHERE	AIMSourceID = @AIMSourceID 
									and SourceLocationName = @SourceLocationName)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Supplier Location Name already exists for this supplier. Please select a different name. Information is not saved!'				
					END
				ELSE IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
							WHERE	AIMSourceID = @AIMSourceID 
									and AIMSupplierLocationCode = @AIMSupplierLocationCode)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Supplier Location Code already exists for this supplier. Please select a different code. Information is not saved!'				
					END
	
				ELSE IF EXISTS(SELECT TOP 1
							S.AIMSourceID
						FROM
							dbo.AIMSource S WITH(NOLOCK)
						WHERE
								S.AIMSourceID = @AIMSourceID
							AND S.IsActive = 0)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to reactivate Source Location because the Source is not active.'
					END
			END

		IF @IsActive = 0
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM dbo.Sample S WITH(NOLOCK) 
							WHERE S.AIMSourceLocationID = @AIMSourceLocationID
											and S.AIMSourceID = @AIMSourceID
											and S.IsActive = 1)
					BEGIN
						-- get list of sampleCodes
						declare tempcur scroll cursor for
						Select case when s.SampleCode is null then s.SampleID 
							else case when s.SampleCode = '' then s.SampleID 
							else s.SampleCode end end as SampleCode 
						from dbo.Sample s with (nolock) 
							where s.AIMSourceLocationID = @AIMSourceLocationID and
								  s.AIMSourceID = AIMSourceID and
								  s.IsActive = 1
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
						SET @ErrorMessage = 'Unable to inactivate supplier location because it is being used in sample table (Samples: ' + @SampleCodeList + ')' 
					END
			END


		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AIMSourceLocation WITH(ROWLOCK)
						SET 
							  @AIMSupplierLocationCode = ISNULL(@AIMSupplierLocationCode, AIMSupplierLocationCode)
							, SourceLocationName = ISNULL(@SourceLocationName, SourceLocationName)
							, AIMSourceID = ISNULL(@AIMSourceID, AIMSourceID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
						WHERE 
							AIMSourceLocationID = @AIMSourceLocationID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

