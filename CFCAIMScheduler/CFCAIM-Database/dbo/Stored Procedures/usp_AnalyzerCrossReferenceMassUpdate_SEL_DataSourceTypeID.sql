CREATE PROCEDURE [dbo].[usp_AnalyzerCrossReferenceMassUpdate_SEL_DataSourceTypeID]
		 @DataSourceTypeID		INT
		, @AIMRequiredField		NVARCHAR(20)

AS
BEGIN
SET NOCOUNT ON;

-- EXEC usp_AnalyzerCrossReferenceMassUpdate_SEL_DataSourceTypeID 13008, 'SUPPLIERLOCATION'

--------------------------------------------------------
IF @AIMRequiredField = 'MATERIAL'
	Begin
		SELECT
			ISNULL(X.AIMMaterialRefAnalyzerMaterialID, 0) AS AnalyzerCrossReferenceMassUpdateID	  
		  , ANM.AnalyzerMaterialID	as AnalyzerID  	 	 
		  , ANM.AnalyzerMaterialName as AnalyzerName
		  , ISNULL(A.AIMMaterialID, 0) as AIMID
		  , A.AIMMaterialName as AIMName
		FROM dbo.AnalyzerMaterial ANM WITH(NOLOCK)
		LEFT OUTER JOIN	dbo.AIMMaterialRefAnalyzerMaterial X with (nolock)
			ON ANM.AnalyzerMaterialID = X.AnalyzerMaterialID
		LEFT OUTER JOIN dbo.AIMMaterial A
			ON X.AIMMaterialID = A.AIMMaterialID
		WHERE 
			ANM.DataSourceTypeID = @DataSourceTypeID
			and ANM.IsActive = 1
			and A.AIMMaterialID IS NULL
		ORDER BY ANM.AnalyzerMaterialName
	End

--------------------------------------------------------
IF @AIMRequiredField = 'OWNER'
	Begin
		SELECT
			ISNULL(X.AIMOwnerRefAnalyzerOwnerID, 0) AS AnalyzerCrossReferenceMassUpdateID	  
		  , ANO.AnalyzerOwnerID	as AnalyzerID  	 	 
		  , ANO.AnalyzerOwnerName as AnalyzerName
		  , ISNULL(A.AIMOwnerID, 0) as AIMID
		  , A.OwnerName as AIMName
		FROM dbo.AnalyzerOwner ANO WITH(NOLOCK)
		LEFT OUTER JOIN	dbo.AIMOwnerRefAnalyzerOwner X with (nolock)
			ON ANO.AnalyzerOwnerID = X.AnalyzerOwnerID
		LEFT OUTER JOIN dbo.AIMOwner A
			ON X.AIMOwnerID = A.AIMOwnerID
		WHERE 
			ANO.DataSourceTypeID = @DataSourceTypeID
			and ANO.IsActive = 1
			and A.AIMOwnerID IS NULL
		ORDER BY ANO.AnalyzerOwnerName
	End

--------------------------------------------------------
IF @AIMRequiredField = 'OWNERLOCATION'
	Begin
		SELECT
			ISNULL(X.AIMOwnerLocationRefAnalyzerOwnerLocationID, 0) AS AnalyzerCrossReferenceMassUpdateID	  
		  , ANOL.AnalyzerOwnerLocationID	as AnalyzerID  	 	 
		  , ANOL.AnalyzerOwnerLocationName as AnalyzerName
		  , ISNULL(A.AIMOwnerLocationID, 0) as AIMID
		  , ISNULL(AMO.AIMOwnerID, 0) as AIMIDSecond
		  , A.LocationName as AIMName
		  , ISNULL(ANOL.AnalyzerOwnerID, 0) as AnalyzerIDSecond
		FROM dbo.AnalyzerOwnerLocation ANOL WITH(NOLOCK)
		LEFT OUTER JOIN	dbo.AIMOwnerLocationRefAnalyzerOwnerLocation X with (nolock)
			ON ANOL.AnalyzerOwnerLocationID = X.AnalyzerOwnerLocationID
		LEFT OUTER JOIN dbo.AIMOwnerLocation A
			ON X.AIMOwnerLocationID = A.AIMOwnerLocationID
		LEFT JOIN dbo.AIMOwner AMO
			ON A.AIMOwnerID = AMO.AIMOwnerID
		WHERE 
			ANOL.DataSourceTypeID = @DataSourceTypeID
			and ANOL.IsActive = 1
			and A.AIMOwnerLocationID IS NULL
		ORDER BY ANOL.AnalyzerOwnerLocationName
	End

	--------------------------------------------------------
IF @AIMRequiredField = 'SUPPLIER'
	Begin
		SELECT
			ISNULL(X.AIMSupplierRefAnalyzerSupplierID, 0) AS AnalyzerCrossReferenceMassUpdateID	  
		  , ANS.AnalyzerSupplierID	as AnalyzerID  	 	 
		  , ANS.AnalyzerSupplierName as AnalyzerName
		  , ISNULL(A.AIMSourceID, 0) as AIMID
		  , A.SourceName as AIMName
		FROM dbo.AnalyzerSupplier ANS WITH(NOLOCK)
		LEFT OUTER JOIN	dbo.AIMSupplierRefAnalyzerSupplier X with (nolock)
			ON ANS.AnalyzerSupplierID = X.AnalyzerSupplierID
		LEFT OUTER JOIN dbo.AIMSource A
			ON X.AIMSupplierID = A.AIMSourceID
		WHERE 
			ANS.DataSourceTypeID = @DataSourceTypeID
			and ANS.IsActive = 1
			and A.AIMSourceID IS NULL
		ORDER BY ANS.AnalyzerSupplierName
	End

--------------------------------------------------------
IF @AIMRequiredField = 'SUPPLIERLOCATION'
	Begin
		SELECT
			ISNULL(X.AIMSupplierLocationRefAnalyzerSupplierLocationID, 0) AS AnalyzerCrossReferenceMassUpdateID	  
		  , ANSL.AnalyzerSupplierLocationID	as AnalyzerID  	 	 
		  , ANSL.AnalyzerSupplierLocationName as AnalyzerName
		  , ISNULL(A.AIMSourceLocationID, 0) as AIMID
		  , ISNULL(AMS.AIMSourceID, 0) as AIMIDSecond
		  , A.SourceLocationName as AIMName
		  , ISNULL(ANSL.AnalyzerSupplierID, 0) as AnalyzerIDSecond
		FROM dbo.AnalyzerSupplierLocation ANSL WITH(NOLOCK)
		LEFT OUTER JOIN	dbo.AIMSupplierLocationRefAnalyzerSupplierLocation X with (nolock)
			ON ANSL.AnalyzerSupplierLocationID = X.AnalyzerSupplierLocationID
		LEFT OUTER JOIN dbo.AIMSourceLocation A
			ON X.AIMSupplierLocationID = A.AIMSourceLocationID
		LEFT JOIN dbo.AIMSource AMS
			ON A.AIMSourceID = AMS.AIMSourceID
		WHERE 
			ANSL.DataSourceTypeID = @DataSourceTypeID
			and ANSL.IsActive = 1
			and A.AIMSourceLocationID IS NULL
		ORDER BY ANSL.AnalyzerSupplierLocationName
	End


END