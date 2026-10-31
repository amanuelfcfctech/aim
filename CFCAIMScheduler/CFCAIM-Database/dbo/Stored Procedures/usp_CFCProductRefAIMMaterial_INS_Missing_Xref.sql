CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_INS_Missing_Xref]

	@CFCProductID			INT
	, @CFCProductCode		VARCHAR(20)
	, @AIMOwnerLocationID	INT
	, @CFCUserID			INT

--exec usp_CFCProductRefAIMMaterial_INS_Missing_Xref 3991, '00-7800', 8, 1
	
AS
BEGIN

DECLARE @AIMMaterialID as INT
DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''

IF EXISTS (SELECT TOP 1 AIMMaterialID 
		   FROM dbo.AIMMaterial 
		   WHERE AIMMaterialCode = @CFCProductCode AND
				 AIMOwnerLocationID = @AIMOwnerLocationID)
		
	Begin
		SET @AIMMaterialID = (
			SELECT TOP 1 AIMMaterialID 
			FROM dbo.AIMMaterial 
			WHERE AIMMaterialCode = @CFCProductCode AND
				 AIMOwnerLocationID = @AIMOwnerLocationID)	
	End
ELSE
	SET @ResultNumber = 1

IF EXISTS (SELECT TOP 1 X.AIMMaterialID 
		   FROM dbo.AIMMaterial AM
		   INNER JOIN dbo.CFCProductRefAIMMaterial X on AM.AIMMaterialID = X.AIMMaterialID
		   WHERE AM.AIMMaterialCode = @CFCProductCode AND
				AM.MaterialTypeID = 2 and
				AM.AIMOwnerLocationID <> @AIMOwnerLocationID)
	SET @ResultNumber = 2	


--ResultNumber 1 = Product does not exist in AimMaterial table
--ResultNumber 2 = Product is cross-referenced .. but for a different plant

IF @ResultNumber = 0
	Begin
		INSERT INTO CFCProductRefAIMMaterial (CFCProductID, AIMMaterialID, UpdateStamp, CFCUserID)
		SELECT @CFCProductID, @AIMMaterialID, Getdate(), @CFCUserID
	End


SELECT 
	 
	  @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END