CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_INS_Missing_Xref]

	@CFCIngredientID			INT
	, @CFCIngredientCode		VARCHAR(20)	
	, @CFCIngredientName		VARCHAR(40)
	, @CFCUserID			INT

--exec usp_CFCIngredientRefAIMMaterial_INS_Missing_Xref 371, '1190','TEST', 1 
		
AS
BEGIN

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @AIMID INT = 0


-- Before adding x-ref, make sure ingredient exists in AIM. If it does not exist, add it.	

SET @AIMID = (SELECT TOP 1 AIMMaterialID 
		   FROM dbo.AIMMaterial 
		   WHERE AIMMaterialCode = @CFCIngredientCode and			   		   
		   MaterialTypeID = 1)

If @AIMID IS NULL
	BEGIN
			INSERT INTO dbo.AIMMaterial (MaterialTypeID, AIMMaterialCode, AIMMaterialName, IsActive, UpdateStamp, CFCUserID) 
			SELECT 1, @CFCIngredientCode, SUBSTRING(@CFCIngredientName,1,30), 1, getdate(), 1

			SET @AIMID = (SELECT TOP 1 AIMMaterialID from dbo.AIMMaterial WHERE					
					AIMMaterialCode = @CFCIngredientCode and 
					MaterialTypeID = 1)	
	END

INSERT INTO dbo.CFCIngredientRefAIMMaterial (CFCIngredientID, AIMMaterialID, UpdateStamp, CFCUserID)
SELECT @CFCIngredientID, @AIMID, Getdate(), @CFCUserID



SELECT 
	 
	  @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END