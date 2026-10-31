
-- task 1035 8/10/26 cursor change - old version kept for reference:  ONE combined list parameter
--									 'AIMSourceID,AIMSourceLocationID,AIMOwnerID,AIMOwnerLocationID;...'
/*
CREATE FUNCTION [dbo].[AnalysisReports_ChartsSupplierOwnerList_WhereStatement]
(
	@PairList		VARCHAR(MAX)	-- 'AIMSourceID,AIMSourceLocationID,AIMOwnerID,AIMOwnerLocationID;...'  (0 = not selected)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
DECLARE @Where					VARCHAR(MAX) = ''
DECLARE @Block					VARCHAR(1000) = ''
DECLARE @Pair					VARCHAR(200) = ''
DECLARE @PairDotted				VARCHAR(200) = ''
DECLARE @Remaining				VARCHAR(MAX) = ISNULL(@PairList, '')
DECLARE @Pos					INT = 0
DECLARE @AIMSourceID			INT = 0
DECLARE @AIMSourceLocationID	INT = 0
DECLARE @AIMOwnerID				INT = 0
DECLARE @AIMOwnerLocationID		INT = 0
DECLARE @Or						BIT = 0

WHILE LEN(@Remaining) > 0
	BEGIN
		SET @Pos = CHARINDEX(';', @Remaining)
		IF @Pos > 0
			BEGIN
				SET @Pair = LEFT(@Remaining, @Pos - 1)
				SET @Remaining = SUBSTRING(@Remaining, @Pos + 1, LEN(@Remaining))
			END
		ELSE
			BEGIN
				SET @Pair = @Remaining
				SET @Remaining = ''
			END

		IF LEN(@Pair) > 0
			BEGIN
				SET @PairDotted = REPLACE(@Pair, ',', '.')
				SET @AIMSourceID			= CAST(ISNULL(PARSENAME(@PairDotted, 4), '0') AS INT)
				SET @AIMSourceLocationID	= CAST(ISNULL(PARSENAME(@PairDotted, 3), '0') AS INT)
				SET @AIMOwnerID				= CAST(ISNULL(PARSENAME(@PairDotted, 2), '0') AS INT)
				SET @AIMOwnerLocationID		= CAST(ISNULL(PARSENAME(@PairDotted, 1), '0') AS INT)

				SET @Block = ''
				IF @AIMSourceID > 0
					BEGIN
						SET @Block = 'S.AIMSourceID = ' + CAST(@AIMSourceID AS VARCHAR)
						IF @AIMSourceLocationID > 0
							SET @Block += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID AS VARCHAR)
						IF @AIMOwnerID > 0
							BEGIN
								SET @Block += ' AND S.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR)
								IF @AIMOwnerLocationID > 0
									SET @Block += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS VARCHAR)
							END
					END
				ELSE IF @AIMOwnerID > 0
					BEGIN
						SET @Block = 'S.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR)
						IF @AIMOwnerLocationID > 0
							SET @Block += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS VARCHAR)
					END

				IF LEN(@Block) > 0
					BEGIN
						IF @Or = 1
							SET @Where += ' OR '
						SET @Where += '(' + @Block + ')'
						SET @Or = 1
					END
			END
	END

IF LEN(@Where) > 0
	SET @Where = ' AND (' + @Where + ')'

RETURN @Where

END
*/

CREATE FUNCTION [dbo].[AnalysisReports_ChartsSupplierOwnerList_WhereStatement]
(
	@SupplierPairList	VARCHAR(MAX),	-- 'AIMSourceID,AIMSourceLocationID;...'  (0 = not selected, empty = supplier not used)
	@OwnerPairList		VARCHAR(MAX)	-- 'AIMOwnerID,AIMOwnerLocationID;...'    (0 = not selected, empty = owner not used)
)
RETURNS VARCHAR(MAX)
AS
BEGIN

-- task 1035 8/10/26 cursor change - supplier rows and owner rows are two SEPARATE lists now (they are different
--									 selections on the Charts screen).  The lists are walked in parallel, so entry 1
--									 of the supplier list runs with entry 1 of the owner list ('Both' stays paired by
--									 position, same behavior as the old fixed 4-slot parameters).
--									 Supplier only  -->  @OwnerPairList is empty (or all '0,0')
--									 Owner only     -->  @SupplierPairList is empty (or all '0,0')
--									 Example:  @SupplierPairList = '8,3;5,0'   @OwnerPairList = '2,0;0,0'
--									   -->  ' AND ((S.AIMSourceID = 8 AND S.AIMSourceLocationID = 3 AND S.AIMOwnerID = 2) OR (S.AIMSourceID = 5))'

DECLARE @Where					VARCHAR(MAX) = ''
DECLARE @Block					VARCHAR(1000) = ''
DECLARE @SupplierPair			VARCHAR(200) = ''
DECLARE @OwnerPair				VARCHAR(200) = ''
DECLARE @SupplierRemaining		VARCHAR(MAX) = ISNULL(@SupplierPairList, '')
DECLARE @OwnerRemaining			VARCHAR(MAX) = ISNULL(@OwnerPairList, '')
DECLARE @Pos					INT = 0
DECLARE @AIMSourceID			INT = 0
DECLARE @AIMSourceLocationID	INT = 0
DECLARE @AIMOwnerID				INT = 0
DECLARE @AIMOwnerLocationID		INT = 0
DECLARE @Or						BIT = 0

WHILE LEN(@SupplierRemaining) > 0 OR LEN(@OwnerRemaining) > 0
	BEGIN
		-- next supplier entry (stays empty when the supplier list is shorter or not used)
		SET @SupplierPair = ''
		IF LEN(@SupplierRemaining) > 0
			BEGIN
				SET @Pos = CHARINDEX(';', @SupplierRemaining)
				IF @Pos > 0
					BEGIN
						SET @SupplierPair = LEFT(@SupplierRemaining, @Pos - 1)
						SET @SupplierRemaining = SUBSTRING(@SupplierRemaining, @Pos + 1, LEN(@SupplierRemaining))
					END
				ELSE
					BEGIN
						SET @SupplierPair = @SupplierRemaining
						SET @SupplierRemaining = ''
					END
			END

		-- next owner entry (stays empty when the owner list is shorter or not used)
		SET @OwnerPair = ''
		IF LEN(@OwnerRemaining) > 0
			BEGIN
				SET @Pos = CHARINDEX(';', @OwnerRemaining)
				IF @Pos > 0
					BEGIN
						SET @OwnerPair = LEFT(@OwnerRemaining, @Pos - 1)
						SET @OwnerRemaining = SUBSTRING(@OwnerRemaining, @Pos + 1, LEN(@OwnerRemaining))
					END
				ELSE
					BEGIN
						SET @OwnerPair = @OwnerRemaining
						SET @OwnerRemaining = ''
					END
			END

		-- the two values in one supplier entry:  SourceID, SourceLocationID
		SET @AIMSourceID = 0
		SET @AIMSourceLocationID = 0
		IF LEN(@SupplierPair) > 0
			BEGIN
				SET @AIMSourceID			= CAST(ISNULL(PARSENAME(REPLACE(@SupplierPair, ',', '.'), 2), '0') AS INT)
				SET @AIMSourceLocationID	= CAST(ISNULL(PARSENAME(REPLACE(@SupplierPair, ',', '.'), 1), '0') AS INT)
			END

		-- the two values in one owner entry:  OwnerID, OwnerLocationID
		SET @AIMOwnerID = 0
		SET @AIMOwnerLocationID = 0
		IF LEN(@OwnerPair) > 0
			BEGIN
				SET @AIMOwnerID				= CAST(ISNULL(PARSENAME(REPLACE(@OwnerPair, ',', '.'), 2), '0') AS INT)
				SET @AIMOwnerLocationID		= CAST(ISNULL(PARSENAME(REPLACE(@OwnerPair, ',', '.'), 1), '0') AS INT)
			END

		SET @Block = ''

		-- supplier entry (owner from the same position nested inside, same as the original function)
		IF @AIMSourceID > 0
			BEGIN
				SET @Block = 'S.AIMSourceID = ' + CAST(@AIMSourceID AS VARCHAR)
				IF @AIMSourceLocationID > 0
					SET @Block += ' AND S.AIMSourceLocationID = ' + CAST(@AIMSourceLocationID AS VARCHAR)
				IF @AIMOwnerID > 0
					BEGIN
						SET @Block += ' AND S.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR)
						IF @AIMOwnerLocationID > 0
							SET @Block += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS VARCHAR)
					END
			END

		-- owner-only entry (no supplier at this position)
		ELSE IF @AIMOwnerID > 0
			BEGIN
				SET @Block = 'S.AIMOwnerID = ' + CAST(@AIMOwnerID AS VARCHAR)
				IF @AIMOwnerLocationID > 0
					SET @Block += ' AND S.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS VARCHAR)
			END

		IF LEN(@Block) > 0
			BEGIN
				IF @Or = 1
					SET @Where += ' OR '
				SET @Where += '(' + @Block + ')'
				SET @Or = 1
			END
	END

IF LEN(@Where) > 0
	SET @Where = ' AND (' + @Where + ')'

RETURN @Where

END