CREATE PROCEDURE [dbo].[usp_IngredientSpecTemplate_DEL_Code]	
					@Code		NVARCHAR(8)	
AS


DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

declare @CodeExists as bit
declare @SepCount as int
Declare @ID INT
Declare @arrayCode varchar(max);  
Declare @array1 varchar(max);
Declare @array2 varchar(max);
Declare @array3 varchar(max);
Declare @array4 varchar(max);
Declare @newarrayCode varchar(max);  
Declare @newarray1 varchar(max);
Declare @newarray2 varchar(max);
Declare @newarray3 varchar(max);
Declare @newarray4 varchar(max);

BEGIN

  
DECLARE @RowCount INT = (SELECT COUNT(*) FROM dbo.IngredientSpecTemplate);    
WHILE @RowCount > 0 
BEGIN  
	SELECT @ID=IngredientSpecRequirementID, @arrayCode=ISICode, @array1=ISSup, @array2=ISMin, @array3=ISMax, @array4=ISMaxCh  
	FROM dbo.IngredientSpecTemplate   
	ORDER BY IngredientSpecRequirementID DESC OFFSET @RowCount - 1 ROWS FETCH NEXT 1 ROWS ONLY;  
    
	SET @CodeExists = (Select dbo.fn_Array_FindCode(@arrayCode, @Code))
	IF @CodeExists = 1 
		Begin				

			-- update Code array
			set @newarrayCode = (Select dbo.fn_Array_ReturnNewCodeString(@arrayCode, @Code))								
			UPDATE dbo.IngredientSpecTemplate SET ISIcode = @newarrayCode

			-- get separator count
			set @SepCount = (Select dbo.fn_Array_GetSeparatorCount(@arrayCode, @Code))			
			
			-- update rest of arrays
			set @newarray1 = (Select dbo.fn_Array_ReturnNewAmountString(@array1, @SepCount))			
			UPDATE dbo.IngredientSpecTemplate SET ISSup = @newarray1

			set @newarray2 = (Select dbo.fn_Array_ReturnNewAmountString(@array2, @SepCount))			
			UPDATE dbo.IngredientSpecTemplate SET ISMin = @newarray2

			set @newarray3 = (Select dbo.fn_Array_ReturnNewAmountString(@array3, @SepCount))					
			UPDATE dbo.IngredientSpecTemplate SET ISMax = @newarray3
			
			set @newarray4 = (Select dbo.fn_Array_ReturnNewAmountString(@array4, @SepCount))				
			UPDATE dbo.IngredientSpecTemplate SET ISMaxCh = @newarray4

		End
   SET @RowCount -= 1;  

	END  

END