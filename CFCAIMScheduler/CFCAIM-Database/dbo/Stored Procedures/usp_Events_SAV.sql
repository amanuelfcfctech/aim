
CREATE PROCEDURE [dbo].[usp_Events_SAV]
			  @EventID								INT
			, @EventName							NVARCHAR(50) = NULL
			, @EventDescription						VARCHAR(100) = NULL
			, @EventRunTypeID						INT = NULL
			, @EventTypeID							INT = NULL
			, @StartDateTime						DATETIME = NULL
			, @LastRunDateTime						DATETIME = NULL
			, @EventStatus							BIT = NULL
			, @TemplateID							INT = NULL			
			, @CFCUserID							INT = NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventID = 0
	BEGIN
		INSERT
			dbo.Event
				(
				  EventName
				, EventDescription
				, EventRunTypeID
				, EventTypeID
				, StartDateTime
				, LastRunDateTime
				, TemplateID	
				, EventStatus				
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @EventName
				, @EventDescription
				, @EventRunTypeID
				, @EventTypeID
				, @StartDateTime
				, @LastRunDateTime
				, @TemplateID						
				, @EventStatus
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.Event WITH(ROWLOCK)
				SET 
					  EventName = ISNULL(@EventName, EventName)
					, EventDescription = ISNULL(@EventDescription, EventDescription)
					, EventRunTypeID = ISNULL(@EventRunTypeID, EventRunTypeID)
					, EventTypeID = ISNULL(@EventTypeID, EventTypeID)
					, StartDateTime = ISNULL(@StartDateTime, StartDateTime)
					, LastRunDateTime = ISNULL(@LastRunDateTime, LastRunDateTime)
					, TemplateID = ISNULL(@TemplateID, TemplateID)			
					, EventStatus = ISNULL(@EventStatus, EventStatus)							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventID = @EventID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END