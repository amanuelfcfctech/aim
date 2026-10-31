CREATE PROCEDURE [dbo].[usp_CFCAIM_INS]
AS
BEGIN
SET NOCOUNT ON;

TRUNCATE TABLE
	dbo.DataSourceType 

INSERT INTO 
	dbo.DataSourceType
        (
		  DataSourceTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  1
		, 'Foss'
		, 1
		)

INSERT INTO 
	dbo.DataSourceType
        (
		  DataSourceTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  2
		, 'Adisseo'
		, 1
		)

INSERT INTO 
	dbo.DataSourceType
        (
		  DataSourceTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  3
		, 'Evonik'
		, 1
		)

INSERT INTO 
	dbo.DataSourceType
        (
		  DataSourceTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  4
		, 'CFCTech'
		, 1
		)

INSERT INTO 
	dbo.DataSourceType
        (
		  DataSourceTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  5
		, 'External'
		, 1
		)


TRUNCATE TABLE
	dbo.FileType

INSERT INTO 
	dbo.FileType
        (
		  FileTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  1
		, 'CSV'
		, 1
		)

INSERT INTO 
	dbo.FileType
        (
		  FileTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  2
		, 'XML'
		, 1
		)

INSERT INTO 
	dbo.FileType
        (
		  FileTypeID
        , Name
        , IsActive
		)
     VALUES
		(
		  3
		, 'TXT'
		, 1
		)

END