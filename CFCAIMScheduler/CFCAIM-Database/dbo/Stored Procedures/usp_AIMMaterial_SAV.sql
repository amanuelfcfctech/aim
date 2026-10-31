CREATE PROCEDURE [dbo].[usp_AIMMaterial_SAV]
			  @AIMMaterialID					INT
			, @MaterialTypeID					INT
			, @AIMMaterialCode					NVARCHAR(20)  = NULL
			, @AIMMaterialName					NVARCHAR(30)  = NULL
			, @IsActive							BIT
			, @CFCUserID						INT
			, @InitialLoad_Sw					BIT = NULL
			, @AIMOwnerLocationID				INT = NULL

	
	
-- EXEC usp_AIMMaterial_SAV 0, 1, 'POULTRY MEAL 2/3/16','POULTRY MEAL 2/3/16', 0, 2 		
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

-- 8/26/20
DECLARE @ActiveStatus BIT = 0

IF @AIMMaterialID = 0
	BEGIN
		IF LEN(@AIMMaterialCode) = 0  --> added because Perdue had blank material-codes in Concept5
			Begin
				IF NOT EXISTS(SELECT 
							AIMMaterialID 
						FROM 
							dbo.AIMMaterial AM
						WHERE 
								AM.AIMMaterialCode = @AIMMaterialCode and 
								AM.MaterialTypeID = @MaterialTypeID)
					BEGIN
						INSERT
						dbo.AIMMaterial 
							(
								MaterialTypeID
							, AIMMaterialCode
							, AIMMaterialName							
							, UpdateStamp
							, CFCUserID
							, AIMOwnerLocationID
							)
						VALUES
							(
								@MaterialTypeID
							, @AIMMaterialCode
							, @AIMMaterialName							 
							, GETDATE()
							, @CFCUserID
							, @AIMOwnerLocationID
							)
						SET @RecordID = @@IDENTITY
					END				
			End
		ELSE
			Begin
				IF EXISTS(SELECT 
							AIMMaterialID 
						FROM 
							dbo.AIMMaterial AM
						WHERE 
								AM.AIMMaterialCode = @AIMMaterialCode and 
								AM.MaterialTypeID = @MaterialTypeID and
								AM.AIMOwnerLocationID = @AIMOwnerLocationID)
					BEGIN

						If @InitialLoad_Sw = 1
							SET @ErrorMessage = 2

						Else
							Begin													

								SET @ErrorNumber = 1
								SET @ErrorMessage = 'This Material code already exists. Please select a different code. Information is not saved!'

								SET @ReturnIntegerValue = (SELECT top 1
									AIMMaterialID 
								FROM 
									dbo.AIMMaterial AM
								WHERE 
									AM.AIMMaterialCode = @AIMMaterialCode and 
									AM.MaterialTypeID = @MaterialTypeID and
									AM.AIMOwnerLocationID = @AIMOwnerLocationID)


								--8/26/20
								SET @ActiveStatus = (SELECT top 1 IsActive FROM dbo.AIMMaterial AM WHERE AM.AIMMaterialID = @ReturnIntegerValue)
								If @ActiveStatus = 0									
									SET @ErrorMessage = 'This Material code already exists and is INACTIVE.  Please reactivate or select a different code.'
									
									
							End
					END

				ELSE
					BEGIN
						INSERT
							dbo.AIMMaterial 
								(
									MaterialTypeID
								, AIMMaterialCode
								, AIMMaterialName							
								, UpdateStamp
								, CFCUserID
								, AIMOwnerLocationID
								)
						VALUES
								(
									@MaterialTypeID
								, @AIMMaterialCode
								, @AIMMaterialName							 
								, GETDATE()
								, @CFCUserID
								, @AIMOwnerLocationID
								)
						SET @RecordID = @@IDENTITY
					END
			End
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			BEGIN

				UPDATE
					dbo.AIMMaterial WITH(ROWLOCK)
						SET 
							  MaterialTypeID = ISNULL(@MaterialTypeID, MaterialTypeID)
							, AIMMaterialCode = ISNULL(@AIMMaterialCode, AIMMaterialCode)
							, AIMMaterialName = ISNULL(@AIMMaterialName, AIMMaterialName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)
						WHERE 
							AIMMaterialID = @AIMMaterialID
				SET @RecordID = @AIMMaterialID 
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ReturnIntegerValue as ReturnIntegerValue
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
