CREATE PROCEDURE [dbo].[usp_AIMOwner_SAV_AIMOwnerID]
			  @AIMOwnerID					INT
			, @OwnerCode					VARCHAR(10)
			, @OwnerName					VARCHAR(30)
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

DECLARE @Status BIT = 0;
DECLARE @StatusText VARCHAR(10) = ''

IF @AIMOwnerID = 0
	BEGIN
		IF EXISTS(SELECT TOP 1 AIMOwnerID FROM AIMOwner WHERE OwnerCode = @OwnerCode)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwner
									WHERE	AIMOwnerID = @AIMOwnerID )
				IF @Status = 0
					SET @ErrorMessage = 'Owner code already exists. (Owner code is inactive.) Please enter a different code. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'This Owner Code already exists. Please select a different code. Information is not saved!'

				SET @ErrorMessage = 'This Owner Code already exists. Please select a different code. Information is not saved!'
				SET @ReturnIntegerValue = (SELECT top 1 AIMOwnerID FROM dbo.AIMOwner WHERE OwnerCode = @OwnerCode)
			END

		IF EXISTS(SELECT TOP 1 AIMOwnerID FROM AIMOwner WHERE OwnerName = @OwnerName)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwner
									WHERE	AIMOwnerID = @AIMOwnerID )
				IF @Status = 0
					SET @ErrorMessage = 'Owner name already exists. (Owner is inactive.) Please enter a different name. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'This Owner Name already exists. Please select a different name. Information is not saved!'

				SET @ErrorMessage = 'This Owner Name already exists. Please select a different name. Information is not saved!'
				SET @ReturnIntegerValue = (SELECT top 1 AIMOwnerID FROM dbo.AIMOwner WHERE OwnerName = @OwnerName)
			END

		ELSE
			BEGIN
				INSERT
					dbo.AIMOwner 
						(
							OwnerCode
						  , OwnerName
						  , CFCUserID
						)
				VALUES
						(
							@OwnerCode
						  , @OwnerName
						  , @CFCUserID
						)
			END
	END
ELSE
	BEGIN

		IF @IsActive = 1
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMOwnerID FROM AIMOwner WHERE OwnerName = @OwnerName and AIMOwnerID <> @AIMOwnerID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Owner Name already exists. Please select a different name. Information is not saved!'
						SET @ReturnIntegerValue = (SELECT top 1 AIMOwnerID FROM dbo.AIMOwner WHERE OwnerName = @OwnerName)
					END
				ELSE IF EXISTS(SELECT TOP 1 AIMOwnerID FROM AIMOwner WHERE OwnerCode = @OwnerCode and AIMOwnerID <> @AIMOwnerID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Owner Code already exists. Please select a different code. Information is not saved!'
						SET @ReturnIntegerValue = (SELECT top 1 AIMOwnerID FROM dbo.AIMOwner WHERE OwnerCode = @OwnerCode)
					END
			END

		IF @IsActive = 0
			BEGIN
				
				IF EXISTS(SELECT TOP 1 AIMOwnerID FROM dbo.Sample S WITH(NOLOCK) 
							WHERE S.AIMOwnerID = @AIMOwnerID and
								  S.IsActive = 1)
					BEGIN

						-- get list of sampleCodes
						declare tempcur scroll cursor for
						Select case when s.SampleCode is null then s.SampleID 
							else case when s.SampleCode = '' then s.SampleID 
							else s.SampleCode end end as SampleCode 
						from dbo.Sample s with (nolock) 
							where s.AIMOwnerID = @AIMOwnerID and S.IsActive = 1
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
						SET @ErrorMessage = 'Unable to inactivate Owner because it is being used in the sample table (Samples: ' + @SampleCodeList + ')' 
					END

				If @ErrorNumber = 0
				
					-- 10/13/17
					--IF EXISTS(SELECT TOP 1 AIMOwnerID FROM dbo.AIMOwnerLocation AOL WITH(NOLOCK) 
					--		WHERE AOL.AIMOwnerID = @AIMOwnerID and
					--			  AOL.IsActive = 1)
					--BEGIN
					--	SET @ErrorNumber = 1
					--	SET @ErrorMessage = 'Unable to inactivate Owner because an Owner Location is assigned to it. You must first inactivate the owner-location(s).'										

					--END
					UPDATE
						dbo.AIMOwnerLocation with (rowlock)
					SET 
						IsActive = 0
						, UpdateStamp = GETDATE()
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID) 
					WHERE
						AIMOwnerID = @AIMOwnerID

			END
	END

IF @ErrorNumber = 0
	BEGIN
		UPDATE
			dbo.AIMOwner WITH(ROWLOCK)
				SET 
					  OwnerCode = ISNULL(@OwnerCode, OwnerCode)
					, OwnerName = ISNULL(@OwnerName, OwnerName)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					AIMOwnerID = @AIMOwnerID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue as ReturnIntegerValue

END
