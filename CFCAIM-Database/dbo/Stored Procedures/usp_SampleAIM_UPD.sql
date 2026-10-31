CREATE PROCEDURE [dbo].[usp_SampleAIM_UPD]
			@SampleGroupByUpdateID	INT
			, @IsActive			BIT		= NULL
			, @CFCUserID			INT		= NULL

			, @SampleID				INT		= NULL	  -- from sample table
			, @AIMMaterialID		INT		= NULL	  -- from sample table
			, @SampleDate			DATE	= NULL	  -- from sample table
			, @EndDate				DATE	= NULL 
			, @OwnerLocationID		INT		= NULL	  -- from sample table

			, @AIMNutrientID		INT		= NULL    -- aimSample table
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

-- 1 = SampleID selected
-- 2 = Just Date selected
-- 3 = Date and Owner selected
-- 4 = Just Owner selected
-- 5 = Nothing selected

IF @SampleGroupByUpdateID = 1			--- (@SampleID IS NOT NULL)
	BEGIN
		UPDATE 
			dbo.SampleAIM WITH(ROWLOCK)
		SET
			  IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
		WHERE 
			SampleID = @SampleID		
			AND AIMNutrientID = @AIMNutrientID
	END
ELSE IF @SampleGroupByUpdateID = 2		--- (@OwnerLocationID is null)
	BEGIN

		UPDATE 
			dbo.SampleAIM with (rowlock)
		SET
			  IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
		FROM 
			dbo.Sample S
		INNER JOIN 
			SampleAIM SA
				ON SA.SampleID = S.SampleID
		WHERE 		
			S.AIMMaterialID = @AIMMaterialID
			AND sa.AIMNutrientID = @AIMNutrientID
			AND s.SampleDate = @SampleDate
	END
ELSE IF @SampleGroupByUpdateID = 3		--- (BOTH @OwnerLocationID and @Sample date NOT null)
	BEGIN

		UPDATE 
			dbo.SampleAIM with (rowlock)
		SET
			  IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
		FROM 
			dbo.Sample S
		INNER JOIN 
			SampleAIM SA
				ON SA.SampleID = S.SampleID
		WHERE 		
			S.AIMMaterialID = @AIMMaterialID
			AND sa.AIMNutrientID = @AIMNutrientID
			AND s.SampleDate = @SampleDate
			AND s.AIMOwnerLocationID = @OwnerLocationID
	END
ELSE IF @SampleGroupByUpdateID = 4		--- (@SampleDate is null)
	BEGIN

		UPDATE 
			dbo.SampleAIM with (rowlock)
		SET
			  IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
		FROM 
			dbo.Sample S
		INNER JOIN 
			SampleAIM SA
				ON SA.SampleID = S.SampleID
		WHERE 		
			S.AIMMaterialID = @AIMMaterialID
			AND sa.AIMNutrientID = @AIMNutrientID
			AND s.SampleDate BETWEEN @SampleDate and @EndDate 
			AND s.AIMOwnerLocationID = @OwnerLocationID
	END		
ELSE
	BEGIN
		UPDATE 
			dbo.SampleAIM with (rowlock) 
		SET
			  IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
		FROM 
			Sample S
		INNER JOIN 
			SampleAIM SA
				ON SA.SampleID = S.SampleID
		WHERE 
			S.AIMMaterialID = @AIMMaterialID
			AND sa.AIMNutrientID = @AIMNutrientID
			AND s.SampleDate BETWEEN @SampleDate and @EndDate 
	END
	
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
