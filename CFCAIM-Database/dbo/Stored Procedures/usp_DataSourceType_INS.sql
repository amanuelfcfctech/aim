CREATE PROCEDURE [dbo].[usp_DataSourceType_INS]
		  @DataSourceTypeID				INT
		, @Name							Nvarchar(50)
		, @DataSourceTypeCode			NVARCHAR(20)
		, @IsActive						BIT
		, @IsFurtherAnalyses			BIT
		, @IsSQLDatabase				BIT
		, @CFCUserID					INT
		, @IsUserDefined				BIT
AS
BEGIN
SET NOCOUNT ON;
SET IDENTITY_INSERT dbo.DataSourceType ON;

DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''
DECLARE @ReturnIntegerValue			INT

INSERT INTO dbo.DataSourceType
(
	DataSourceTypeID
  , Name
  , DataSourceTypeCode
  , IsActive
  , IsFurtherAnalyses
  , IsSQLDatabase
  , CFCUserID
  , IsUserDefined
)
VALUES
(
	@DataSourceTypeID
  , @Name
  , @DataSourceTypeCode
  , @IsActive
  , @IsFurtherAnalyses
  , @IsSQLDatabase
  , @CFCUserID
  , @IsUserDefined
)

SET IDENTITY_INSERT dbo.DataSourceType OFF;
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue
END