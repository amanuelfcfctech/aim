CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_INS_Missing_Xref]

	@CFCProductID			INT
	, @CFCProductCode		VARCHAR(20)
	, @CFCProductName		VARCHAR(40)	
	, @AIMOwnerLocationID	INT
	, @CFCUserID			INT

--exec usp_CFCProductRefAIMMaterial_INS_Missing_Xref 3991, '00-7800', 'TEST', 8, 1
	
AS
BEGIN


DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @AIMID INT = 0


SET @AIMID = (SELECT TOP 1 AIMMaterialID 
		   FROM dbo.AIMMaterial 
		   WHERE AIMMaterialCode = @CFCProductCode AND
				 AIMOwnerLocationID = @AIMOwnerLocationID AND
				 MaterialTypeID = 2)
		
IF @AIMID IS NULL
	BEGIN
		INSERT INTO dbo.AIMMaterial (MaterialTypeID, AIMMaterialCode, AIMMaterialName, IsActive, UpdateStamp, CFCUserID, AIMOwnerLocationID) 
		SELECT 2, @CFCProductCode, SUBSTRING(@CFCProductName, 30,1), 1, getdate(), 1, @AIMOwnerLocationID

			Set @AIMID = (SELECT TOP 1 AIMMaterialID from dbo.AIMMaterial WHERE
					AIMOwnerLocationID = @AIMOwnerLocationID and
					AIMMaterialCode = @CFCProductCode and 
					MaterialTypeID = 2)

	END

	
INSERT INTO dbo.CFCProductRefAIMMaterial (CFCProductID, AIMMaterialID, UpdateStamp, CFCUserID)
SELECT @CFCProductID, @AIMID, Getdate(), @CFCUserID
	


SELECT 
	 
	  @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END