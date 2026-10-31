CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SEL_QueryTemplateID_DataSourceComparison]
				@QueryTemplateID		INT
			  , @QueryFieldID			INT
	
				AS
BEGIN
SET NOCOUNT ON;

-- exec usp_QueryTemplateDetail_SEL_QueryTemplateID_DataSourceComparison 15, 13


-- Variables for Cursor
DECLARE @QueryTemplateDetailID INT
DECLARE @DataSourceTypeID INT
DECLARE @DataSourceName nvarchar(30)
DECLARE @AIMNutrientID INT 
DECLARE @AIMNutrientCode nvarchar(30)
DECLARE @AIMNutrientName nvarchar(30)
DECLARE @saveDetailID int
DECLARE @saveDST int
DECLARE @saveNut int
DECLARE @saveNutCode nvarchar(30)
DECLARE @saveNutName nvarchar(30)
DECLARE @saveDST2 int
DECLARE @saveDSTName2 nvarchar(30)
DECLARE @saveNut2 int
DECLARE @saveNutCode2 nvarchar(30)
DECLARE @saveNutName2 nvarchar(30)

SET @saveDetailID = 0
SET @saveDST = 0
SET @saveNut = 0
SET @saveNutCode = ''
SET @saveNutName = ''
SET @saveDST2 = 0
SET @saveDSTName2 = ''
SET @saveNut2 = 0
SET @saveNutCode2 = ''
SET @saveNutName2 = ''

IF OBJECT_ID('tempdb..#tempTable') IS NOT NULL DROP TABLE #tempTable	
CREATE TABLE #tempTable
  ( 
    QueryTemplateDetailID INT,
    DS1 INT,
	Nut1 INT,
	Nut1Code nvarchar(30),
	Nut1Name nvarchar(30),
	DS2 INT,
	DS2_Name nvarchar(30),
	Nut2 INT,
	Nut2Code nvarchar(30),
	Nut2Name nvarchar(30)
  ) 

DECLARE tempcur SCROLL CURSOR FOR
	SELECT N.QueryTemplateDetailID, N.DataSourceTypeID , N.AIMNutrientID, AMN.AIMNutrientCode, AMN.AIMNutrientName, D.Name 
	FROM dbo.QueryTemplateDetailNutrient N
	INNER JOIN dbo.QueryTemplateDetail Q on N.QueryTemplateDetailID = Q.QueryTemplateDetailID	
	INNER JOIN dbo.AIMNutrient amn on N.AIMNutrientID = amn.AIMNutrientID
	INNER JOIN dbo.DataSourceType D on N.DataSourceTypeID = D.DataSourceTypeID
	WHERE Q.QueryTemplateID = @QueryTemplateID and Q.QueryFieldID = @QueryFieldID
	ORDER BY N.QueryTemplateDetailID, N.QueryTemplateDetailNutrientID
OPEN tempcur
FETCH NEXT FROM tempcur INTO @QueryTemplateDetailID, @DataSourceTypeID, @AIMNutrientID, @AIMNutrientCode, @AIMNutrientName, @DataSourceName
WHILE @@FETCH_STATUS = 0
	begin		
		If @saveDetailID = 0 
			Begin				
				set @saveDetailID = @QueryTemplateDetailID
				set @saveDST = @DataSourceTypeID				
				set @saveNut = @AIMNutrientID
				set @saveNutCode = @AIMNutrientCode
				set @saveNutName = @AIMNutrientName
			end 
		else if @saveDetailID = @QueryTemplateDetailID
			Begin				
				set @saveDST2 = @DataSourceTypeID
				set @saveDSTName2 = @DataSourceName
				set @saveNut2 = @AIMNutrientID
				set @saveNutCode2 = @AIMNutrientCode
				set @saveNutName2 = @AIMNutrientName
				
			End
		else if @saveDetailID <> @QueryTemplateDetailID
			Begin	
				
				INSERT INTO #tempTable (QueryTemplateDetailID, DS1, Nut1, Nut1Code, Nut1Name, DS2, DS2_Name, Nut2, Nut2Code, Nut2Name)
				SELECT @saveDetailID, @saveDST, @saveNut, @saveNutCode, @saveNutName, @saveDST2, @saveDSTName2, @saveNut2, @saveNutCode2, @saveNutName2
				SET @saveDetailID = @QueryTemplateDetailID
				SET @saveDST = @DataSourceTypeID
				SET @saveNut = @AIMNutrientID
				SET @saveNutCode = @AIMNutrientCode
				SET @saveNutName = @AIMNutrientName
				SET @saveDST2 = 0
				SET @saveNut2 = 0				
			End

		FETCH NEXT FROM tempcur INTO @QueryTemplateDetailID, @DataSourceTypeID, @AIMNutrientID, @AIMNutrientCode, @AIMNutrientName, @DataSourceName
	end
	INSERT INTO #tempTable (QueryTemplateDetailID, DS1, Nut1, Nut1Code, Nut1Name, DS2, DS2_Name, Nut2, Nut2Code, Nut2Name)
	SELECT @saveDetailID, @saveDST, @saveNut, @saveNutCode, @saveNutName, @saveDST2, @saveDSTName2, @saveNut2, @saveNutCode2, @saveNutName2
CLOSE tempcur
DEALLOCATE tempcur

SELECT 
	DS1 as DataSourceTypeID1, 
	DST.Name as DataSourceTypeName1, 	
	Nut1 as DS1_NutID, 
	Nut1Code as DS1_NutCode, 
	Nut1Name as DS1_NutName, 

	DS2 as DataSourceTypeID2,
	DS2_Name as DataSourceTypeName2,
	Nut2 as DS2_NutID,
	Nut2Code as DS2_NutCode,
	Nut2Name as DS2_NutName,

	D.QueryTemplateID, 
	D.QueryTemplateDetailID, 
	D.CFCUserID, 
	D.OrderBy, 
	0 as IsActive
FROM dbo.QueryTemplateDetail D
LEFT OUTER JOIN dbo.DataSourceType DST 
	ON D.Value = DST.DataSourceTypeID 
LEFT OUTER JOIN dbo.QueryTemplateDetailNutrient N 
	ON D.QueryTemplateDetailID = N.QueryTemplateDetailID 
	and D.Value = N.DataSourceTypeID 
LEFT OUTER JOIN #tempTable 
	ON N.QueryTemplateDetailID = #tempTable.QueryTemplateDetailID

WHERE D.QueryTemplateID = @QueryTemplateID and
      D.QueryFieldID = @QueryFieldID


END