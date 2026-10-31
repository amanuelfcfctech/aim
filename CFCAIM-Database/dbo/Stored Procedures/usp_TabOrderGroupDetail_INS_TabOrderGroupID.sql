CREATE PROCEDURE [dbo].[usp_TabOrderGroupDetail_INS_TabOrderGroupID]
				 @TabOrderGroupID			INT
				,@CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

INSERT INTO
	dbo.TabOrderGroupDetail 
	(
		TabOrderGroupID
		,ControlID
		,IsVisible
		,TabOrder
		,UpdateStamp
		,CFCUserID
	)
SELECT 
	 @TabOrderGroupID
	,T.ControlID
	,0
	,T.TabOrder
	,GETDATE()
	,@CFCUserID
FROM
	dbo.TabOrder T
	WHERE
		UserID = 1
	
	
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END