CREATE PROCEDURE [dbo].[usp_Labels_SAV_LabelProdFields]
				@LabelID					INT
			  , @LabelField 				INT				-- 1, 2, 3, 4 or 5
			  , @LabelValue					BIT	  
			  , @LabelBold					BIT				
			  , @CFCUserID					INT = NULL					  



AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
	
BEGIN

	IF @LabelField = 1
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelProdFieldLine1 = 0 	
					, ProdLine1Bold = 0				
					, CFCUserID = @CFCUserID					
		UPDATE
			dbo.Labels
				SET 
					LabelProdFieldLine1 = @LabelValue					
					, ProdLine1Bold = @LabelBold
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
					
	End

	----------------------------------------------------
	--6/23/21 - lines 2, 3 and 4 added for Vitapro
	IF @LabelField = 2
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelProdFieldLine2 = 0 
					, ProdLine2Bold = 0						
					, CFCUserID = @CFCUserID					
		UPDATE
			dbo.Labels
				SET 
					LabelProdFieldLine2 = @LabelValue	
					, ProdLine2Bold = @LabelBold				
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	IF @LabelField = 3
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelProdFieldLine3 = 0 
					, ProdLine3Bold = 0						
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelProdFieldLine3 = @LabelValue	
					, ProdLine3Bold = @LabelBold				
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	IF @LabelField = 4
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelProdFieldLine4 = 0 
					, ProdLine4Bold = 0						
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelProdFieldLine4 = @LabelValue					
					, ProdLine4Bold = @LabelBold
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	----------------------------------------------------

	IF @LabelField = 5

	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelProdFieldLine5 = 0 	
					, ProdLine5Bold = 0					
					, CFCUserID = @CFCUserID						
		UPDATE
			dbo.Labels
				SET 
					LabelProdFieldLine5 = @LabelValue	
					, ProdLine5Bold = @LabelBold				
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID				
	End

END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END