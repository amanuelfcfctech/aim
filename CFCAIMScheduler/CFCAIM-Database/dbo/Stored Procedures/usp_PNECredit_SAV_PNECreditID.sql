CREATE PROCEDURE [dbo].[usp_PNECredit_SAV_PNECreditID]
			  @PNECreditID					INT			
		    , @PNEKey						VARCHAR(40)
			, @PNEDescription				VARCHAR(50)
			, @Credit						INT
			, @CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @PNECreditID = 0
	BEGIN
		INSERT
			dbo.PNECredit
				(
					  PNEKey	
					, PNEDescription
					, Credit
					, CFCUserID
				)
		VALUES
				(
					  @PNEKey
					, @PNEDescription
					, @Credit
					, @CFCUserID
				)
		SET @RecordID = @@IDENTITY
	END
ELSE
	BEGIN
		UPDATE
			dbo.PNECredit 
				SET 
					  PNEKey = ISNULL(@PNEKey, PNEKey)
					, PNEDescription = ISNULL(@PNEDescription, PNEDescription)
					, Credit = ISNULL(@Credit, Credit)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					PNECreditID = @PNECreditID

				SET @RecordID = @PNECreditID
	END

SELECT 
	  @RecordID AS RecordID
	, @@ERROR AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END