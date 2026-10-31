CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SAV]
			  @QueryTemplateDetailID		INT
			, @QueryTemplateID				INT		= NULL
			, @QueryFieldID					INT		= NULL
			, @QueryOperationID				INT		= NULL
			, @IsAnd						BIT		= NULL
			, @Value						INT		= NULL
			, @SecondValue					INT		= NULL
			, @DateValue					DATE	= NULL
			, @SecondDate					DATE	= NULL
			, @CFCUserID					INT		= NULL
			
			--6/1/22
			, @OrderBy						INT		= NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @QueryTemplateDetailID = 0
	BEGIN
		INSERT
			dbo.QueryTemplateDetail 
				(
				  QueryTemplateID
				, QueryFieldID
				, QueryOperationID
				, IsAnd
				, Value
				, SecondValue
				, DateValue
				, SecondDate
				, UpdateStamp
				, CFCUserID

				--6/1/22
				, OrderBy

				)
		VALUES
				(
				  @QueryTemplateID
				, @QueryFieldID
				, @QueryOperationID
				, @IsAnd
				, @Value
				, @SecondValue
				, @DateValue
				, @SecondDate
				, GETDATE()
				, @CFCUserID

				--6/1/22
				, @OrderBy

				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.QueryTemplateDetail WITH(ROWLOCK)
				SET 
					  QueryTemplateID = ISNULL(@QueryTemplateID, QueryTemplateID)
					, QueryFieldID = ISNULL(@QueryFieldID, QueryFieldID)
					, QueryOperationID = ISNULL(@QueryOperationID, QueryOperationID)
					, IsAnd = ISNULL(@IsAnd, IsAnd)
					, Value = ISNULL(@Value, Value)
					, SecondValue = ISNULL(@SecondValue, SecondValue)
					, DateValue = ISNULL(@DateValue, DateValue)
					, SecondDate = ISNULL(@SecondDate, SecondDate)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)

					--6/1/22
					, OrderBy = ISNULL(@OrderBy, OrderBy)

				WHERE 
					QueryTemplateDetailID = @QueryTemplateDetailID

		-----------------------------------------------------------						
		
		------IF EXISTS(SELECT TOP 1
		------	Q.OrderBy
		------			FROM
		------				dbo.QueryTemplateDetail Q WITH(NOLOCK)
		------			WHERE 
		------				QueryTemplateID = @QueryTemplateID
		------			GROUP BY
		------				Q.OrderBy
		------			HAVING 
		------				COUNT(*) > 1)
		------	BEGIN
		------		UPDATE
		------			dbo.QueryTemplateDetail WITH(ROWLOCK)
		------				SET
		------					  OrderBy = OrderBy + 1
		------					, UpdateStamp = GETDATE()
		------					, CFCUserID = @CFCUserID
		------				WHERE
		------						QueryTemplateID = @QueryTemplateID
		------					AND QueryTemplateDetailID != @QueryTemplateDetailID
		------					AND (OrderBy >= @OrderBy) 
		------					AND OrderBy != 0
		------	END
			-----------------------------------------------------------
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
