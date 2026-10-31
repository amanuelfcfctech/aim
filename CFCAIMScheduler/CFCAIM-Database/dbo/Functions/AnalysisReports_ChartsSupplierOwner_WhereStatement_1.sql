
CREATE FUNCTION [dbo].[AnalysisReports_ChartsSupplierOwner_WhereStatement]
(
	   @AIMSourceID				INT
	   ,@AIMSourceID2			INT
	   ,@AIMSourceID3			INT
	   ,@AIMSourceID4			INT
	   ,@AIMSourceLocationID	INT
	   ,@AIMSourceLocationID2	INT
	   ,@AIMSourceLocationID3	INT
	   ,@AIMSourceLocationID4	INT
	   ,@AIMOwnerID				INT
	   ,@AIMOwnerID2			INT
	   ,@AIMOwnerID3			INT
	   ,@AIMOwnerID4			INT
	   ,@AIMOwnerLocationID	INT
	   ,@AIMOwnerLocationID2	INT
	   ,@AIMOwnerLocationID3	INT
	   ,@AIMOwnerLocationID4	INT
	   ,@SupplierOwnerOption	INT			-- 1 = SupplierOnly, 2 = OwnerOnly 3 = Both Supplier and Owner

	 
)
RETURNS VARCHAR(1000)
AS
BEGIN	

-- 6/30/21 - added for Randy at Perdue

DECLARE @Where				VARCHAR(1000) = ''
DECLARE @Or					Bit
SET @Or = 0

IF @AIMSourceID > 0 or 	@AIMSourceID2 > 0 or @AIMSourceID3 > 0 or @AIMSourceID4 > 0 or @AIMOwnerID > 0 or @AIMOwnerID2 > 0 or @AIMOwnerID3 > 0 or @AIMOwnerID4 > 0
	SET @Where = ' AND ('

-- Supplier 1
IF @AIMSourceID > 0 
	Begin
		SET @Where += '(S.AIMSourceID = ' + CAST(@AIMSourceID AS VARCHAR) 
		IF @AIMSourceLocationID > 0
			SET @Where += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID AS Varchar)
			IF @AIMOwnerID > 0 
				Begin
					SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '
					If @AIMOwnerLocationID > 0
						SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ''
				End
		SET @Where += ')'
		SET @Or = 1
	End

--Supplier 2
IF @AIMSourceID2 > 0 
	Begin
		IF @Or = 1
			SET @Where += ' OR '
		SET @Where += '(S.AIMSourceID = ' + CAST(@AIMSourceID2 AS VARCHAR) 
		IF @AIMSourceLocationID2 > 0
			SET @Where += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID2 AS Varchar)
			IF @AIMOwnerID2 > 0 
				Begin
					SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID2 as varchar) + ' '
					If @AIMOwnerLocationID2 > 0
						SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID2 as varchar) + ''
				End
		SET @Where += ')'
		SET @Or = 1
	End

--Supplier 3
IF @AIMSourceID3 > 0 
	Begin
		IF @Or = 1
			SET @Where += ' OR '
		SET @Where += '(S.AIMSourceID = ' + CAST(@AIMSourceID3 AS VARCHAR) 
		IF @AIMSourceLocationID3 > 0
			SET @Where += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID3 AS Varchar)
			IF @AIMOwnerID3 > 0 
				Begin
					SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID3 as varchar) + ' '
					If @AIMOwnerLocationID3 > 0
						SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID3 as varchar) + ''
				End
		SET @Where += ')'
		SET @Or = 1
	End

--Supplier 4
IF @AIMSourceID4 > 0 
	Begin
		IF @Or = 1
			SET @Where += ' OR '
		SET @Where += '(S.AIMSourceID = ' + CAST(@AIMSourceID4 AS VARCHAR) 
		IF @AIMSourceLocationID4 > 0
			SET @Where += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID4 AS Varchar)
			IF @AIMOwnerID4 > 0 
				Begin
					SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID4 as varchar) + ' '
					If @AIMOwnerLocationID4 > 0
						SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID4 as varchar) + ''
				End
		SET @Where += ')'
		SET @Or = 1
	End

if @SupplierOwnerOption = 2
	Begin
		SET @Or = 0

		-- Owner 1
		IF @AIMOwnerID > 0 
			Begin
				SET @Where += '(S.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR) 
				IF @AIMOwnerLocationID > 0
					SET @Where += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS Varchar)					
				SET @Where += ')'
				SET @Or = 1
			End

		--Owner 2
		IF @AIMOwnerID2 > 0 
			Begin
				IF @Or = 1
					SET @Where += ' OR '
				SET @Where += '(S.AIMOwnerID = ' + CAST(@AIMOwnerID2 AS VARCHAR) 
				IF @AIMOwnerLocationID2 > 0
					SET @Where += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID2 AS Varchar)					
				SET @Where += ')'
				SET @Or = 1
			End

		--Owner 3
		IF @AIMOwnerID3 > 0 
			Begin
				IF @Or = 1
					SET @Where += ' OR '
				SET @Where += '(S.AIMOwnerID = ' + CAST(@AIMOwnerID3 AS VARCHAR) 
				IF @AIMOwnerLocationID3 > 0
					SET @Where += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID3 AS Varchar)					
				SET @Where += ')'
				SET @Or = 1
			End

		--Owner 4
		IF @AIMOwnerID4 > 0 
			Begin
				IF @Or = 1
					SET @Where += ' OR '
				SET @Where += '(S.AIMOwnerID = ' + CAST(@AIMOwnerID4 AS VARCHAR) 
				IF @AIMOwnerLocationID4 > 0
					SET @Where += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID4 AS Varchar)					
				SET @Where += ')'
				SET @Or = 1
			End
	End

IF @AIMSourceID > 0 or 	@AIMSourceID2 > 0 or @AIMSourceID3 > 0 or @AIMSourceID4 > 0 or @AIMOwnerID > 0 or @AIMOwnerID2 > 0 or @AIMOwnerID3 > 0 or @AIMOwnerID4 > 0
	SET @Where += ')'				

RETURN @Where

END