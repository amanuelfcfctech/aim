CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SAV_AIMOwnerID]
			  @AIMOwnerLocationID					INT
			, @AIMOwnerID							INT
			, @LocationCode							VARCHAR(20)
			, @LocationName							VARCHAR(50)			
			, @IsActive								BIT
			, @CFCUserID							INT
			, @IsFormulationPlant					BIT
AS

-- EXEC usp_AIMOwnerLocation_SAV_AIMOwnerID 68, 1, '7 bad', '7 BAD', 0, 1

BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

DECLARE @SampleCodeList VARCHAR(1000) = ''
DECLARE @SampleCode VARCHAR (20) = ''
DECLARE @ID INT

IF @AIMOwnerLocationID = 0
	BEGIN
		IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationCode = @LocationCode)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Owner Location code already exists. Please enter a different code. Information is not saved!'

				END
		ELSE IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationName = @LocationName)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Owner Location name already exists. Please enter a different name. Information is not saved!'

				END
		ELSE
			BEGIN
				INSERT
					dbo.AIMOwnerLocation
						(
							AIMOwnerID
							, LocationCode
							, LocationName
							, CFCUserID
							, IsFormulationPlant
						)
				VALUES
						(
							@AIMOwnerID
							, @LocationCode
							, @LocationName
							, @CFCUserID
							, @IsFormulationPlant
						)
			END
	END
ELSE
	BEGIN

		IF @IsActive = 1
			BEGIN

				IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationCode = @LocationCode)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Owner Location code already exists for this owner. Please enter a different code. Information is not saved!'
					END

				ELSE IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationName = @LocationName)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Owner Location name already exists for this owner. Please enter a different name. Information is not saved!'
					END

				ELSE IF EXISTS(SELECT TOP 1
						AO.AIMOwnerID
					FROM
						dbo.AIMOwner AO WITH(NOLOCK)
					WHERE
						AO.AIMOwnerID = @AIMOwnerID
						AND AO.IsActive = 0)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to reactivate Owner Location because the Owner is not active.'
					END
			END

		IF @IsActive = 0
			BEGIN

				SET @ID = (SELECT TOP 1 AIMOwnerLocationID FROM dbo.Sample S WITH(NOLOCK) 
							WHERE S.AIMOwnerLocationID = @AIMOwnerLocationID
											and S.AIMOwnerID = @AIMOwnerID
											and S.IsActive = 1)
				IF Len(@ID) > 0
					BEGIN
						-- get list of sampleCodes
						declare tempcur scroll cursor for
						Select case when s.SampleCode is null then CONVERT(VARCHAR,s.SampleID)
							else case when s.SampleCode = '' then CONVERT(VARCHAR,s.SampleID) 
							else s.SampleCode end end as SampleCode  
						from dbo.Sample s with (nolock) 
							where s.AIMOwnerLocationID = @AIMOwnerLocationID and
								  s.AIMOwnerID = @AIMOwnerID and
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
						SET @ErrorMessage = 'Unable to inactivate owner location because it is being used in sample table (Samples: ' + @SampleCodeList + ')' 
					END
			END
	END

IF @ErrorNumber = 0
	BEGIN
		UPDATE
			dbo.AIMOwnerLocation WITH(ROWLOCK)
				SET 
					  AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID)
					, LocationCode = ISNULL(@LocationCode, LocationCode)
					, LocationName = ISNULL(@LocationName, LocationName)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, IsFormulationPlant = ISNULL(@IsFormulationPlant, IsFormulationPlant)
				WHERE 
					AIMOwnerLocationID = @AIMOwnerLocationID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
