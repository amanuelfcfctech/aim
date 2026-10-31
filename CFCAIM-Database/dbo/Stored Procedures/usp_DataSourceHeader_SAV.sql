CREATE PROCEDURE [dbo].[usp_DataSourceHeader_SAV]
			  @DataSourceHeaderID	INT
			, @DataSourceTypeID		INT				= NULL
			, @CFCUserID			INT				= NULL
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

DECLARE @HeaderExists BIT = 0 

IF EXISTS(
			SELECT 
				Hdr.DataSourceHeaderID								
			FROM
				DataSourceHeader Hdr
			WHERE
				Hdr.DataSourceTypeID = @DataSourceTypeID
				AND Hdr.DataSourceHeaderID != @DataSourceHeaderID
		)
	BEGIN
		SET @HeaderExists = 1
		SET @RecordID = (SELECT 
					Hdr.DataSourceHeaderID
				FROM
					DataSourceHeader Hdr
				WHERE
						Hdr.DataSourceTypeID = @DataSourceTypeID
					AND Hdr.DataSourceHeaderID != @DataSourceHeaderID)
	END


IF @HeaderExists = 0

	IF EXISTS(
				SELECT 
					HDR.DataSourceHeaderID
				FROM
					DataSourceHeader HDR
				WHERE
					HDR.DataSourceHeaderID = @DataSourceHeaderID
			)
		BEGIN
			UPDATE
				dbo.DataSourceHeader
					SET
						  DataSourceTypeID = @DataSourceTypeID
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
			WHERE 
				DataSourceHeaderID = @DataSourceHeaderID
			SET @RecordID = @@IDENTITY
		END
	ELSE
		BEGIN
			INSERT
			dbo.DataSourceHeader
			(
				  DataSourceTypeID
				, UpdateStamp
				, CFCUserID
			)
			VALUES
			(
				  @DataSourceTypeID
				, GETDATE()
				, @CFCUserID
			)
			SET @RecordID = @@IDENTITY
		END



SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END