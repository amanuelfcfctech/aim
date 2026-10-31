CREATE PROCEDURE [dbo].[usp_Labels_SAV_LabelFields]
				@LabelID					INT
			  , @LabelField 				INT				-- 1, 2, 3, 4 or 5
			  , @LabelValue					BIT	  
			  , @LabelBold					BIT				-- added 6/29/21	
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
					LabelFieldFirst = 0 	
					, Line1Bold = 0				
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelFieldFirst = @LabelValue					
					, Line1Bold = @LabelBold
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
					LabelFieldLine2 = 0 
					, Line2Bold = 0						
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelFieldLine2 = @LabelValue	
					, Line2Bold = @LabelBold				
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	IF @LabelField = 3
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelFieldLine3 = 0 
					, Line3Bold = 0						
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelFieldLine3 = @LabelValue	
					, Line3Bold = @LabelBold				
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	IF @LabelField = 4
	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelFieldLine4 = 0 
					, Line4Bold = 0						
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelFieldLine4 = @LabelValue					
					, Line4Bold = @LabelBold
					, CFCUserID = @CFCUserID
				WHERE
					LabelsID = @LabelID
	End
	----------------------------------------------------

	--IF @LabelField =2
	IF @LabelField = 5

	Begin
		UPDATE 
			dbo.Labels 
				SET 
					LabelFieldLast = 0 	
					, Line5Bold = 0					
					, CFCUserID = @CFCUserID				
		UPDATE
			dbo.Labels
				SET 
					LabelFieldLast = @LabelValue	
					, Line5Bold = @LabelBold				
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