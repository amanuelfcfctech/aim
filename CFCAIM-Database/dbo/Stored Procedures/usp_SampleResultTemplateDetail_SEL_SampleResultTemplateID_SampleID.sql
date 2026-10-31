
CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID_SampleID]
	  @SampleResultTemplateID INT
	, @SampleID INT
	, @AIMMaterialID INT
AS
BEGIN

--DECLARE	  @SampleResultTemplateID INT = 106 
--	, @SampleID INT = 5259
--	, @AIMMaterialID INT = 130

--exec usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID_SampleID_DataSourceTypeID 0,0,0

DECLARE @IsPrioritizeGroupTemplate BIT = 0

SET NOCOUNT ON;



CREATE TABLE #Outside (	
    AnalyzerNutrientID Int, 	 
    Message Int    )
INSERT INTO #Outside
EXEC usp_SampleResult_SEL_Messages_Outside 4, @SampleID, NULL, NULL, 1

CREATE TABLE #Inside (	
    AnalyzerNutrientID Int, 	 
    Message Int	)
INSERT INTO #Inside
EXEC usp_SampleResult_SEL_Messages_Inside 4, @SampleID, NULL, NULL, 1


--
SELECT @IsPrioritizeGroupTemplate = IsGroupTemplate
	FROM SampleResultTemplate
	WHERE SampleResultTemplateID = @SampleResultTemplateID

IF @IsPrioritizeGroupTemplate = 0
	BEGIN
		SELECT 
			SRTD.*	
			, AN.AnalyzerNutrientCode
			, AN.AnalyzerNutrientName
			, AN.AnalyzerNutrientID
			, AN.UnitType
			, AIMN.AIMNutrientName	
			, SR.ResultValue AS CurrentValue
			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--, AU.UserName
			, U.UserName	
			, WL.Min
			, WL.Max			
		FROM 
			dbo.SampleResultTemplateDetail SRTD with (nolock)
		INNER JOIN
			SampleResultTemplate ST
				ON ST.SampleResultTemplateID = SRTD.SampleResultTemplateID
		INNER JOIN
			dbo.AnalyzerNutrient AN with (nolock)
				ON SRTD.AnalyzerNutrientID = AN.AnalyzerNutrientID
		LEFT OUTER JOIN
			(SELECT 
					  AN.AnalyzerNutrientID
					, IsNull(ResultValue,0) as ResultValue
					, AN.DataSourceTypeID
				FROM 
					dbo.SampleResult SR
				INNER JOIN 
					dbo.AnalyzerNutrient AN WITH(NOLOCK)
						ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 						
						AND SR.SampleID = @SampleID
						AND AN.IsActive = 1				
						AND SR.IsActive = 1					
			 ) SR
			    ON SR.AnalyzerNutrientID = AN.AnalyzerNutrientID
				AND SR.DataSourceTypeID = ST.DataSourceTypeID
		LEFT OUTER JOIN
			dbo.AIMNutrientRefAnalyzerNutrient ANX with (nolock)
				ON AN.AnalyzerNutrientID = ANX.AnalyzerNutrientID		
		LEFT OUTER JOIN 
			dbo.AIMNutrient AIMN with (nolock)
				ON ANX.AIMNutrientID = AIMN.AIMNutrientID
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SRTD.CFCUserID = U.CFCUserID
		LEFT OUTER JOIN
			dbo.WarningLimits WL
			ON WL.AIMNutrientID = ANX.AIMNutrientID
			AND WL.AIMMaterialID = @AIMMaterialID
		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

		WHERE SRTD.SampleResultTemplateID = @SampleResultTemplateID

		ORDER BY SRTD.SortOrder 
	END
IF @IsPrioritizeGroupTemplate = 1
	BEGIN
		IF OBJECT_ID('tempdb..#GridResultIsDateFalse') IS NOT NULL
			DROP TABLE #GridResultIsDateFalse

		IF OBJECT_ID('tempdb..#GridResultIsDateTrue') IS NOT NULL
			DROP TABLE #GridResultIsDateTrue
		SELECT 
				  STG.*	
				, ANX.AnalyzerNutrientCode
				, ANX.AnalyzerNutrientName
				, ANX.AnalyzerNutrientID
				, ANX.UnitType
				, AIMN.AIMNutrientName	
				, SR.ResultValue AS CurrentValue
				, U.UserName
				, SortOrder
				, NGD.NutrientGroupDetailID
				, WL.Min
				, WL.Max
		INTO 
			#GridResultIsDateFalse
			FROM 
				SampleResultTemplateDetailGroup STG
				INNER JOIN
					NutrientGroup NG
						ON NG.NutrientGroupID = STG.NutrientGroupID
				INNER JOIN
					NutrientGroupDetail NGD
						ON NG.NutrientGroupID = NGD.NutrientGroupID
				INNER JOIN
					SampleResultTemplate ST
						ON ST.SampleResultTemplateID = STG.SampleResultTemplateID
				INNER JOIN
					dbo.AIMNutrient AIMN
						ON NGD.AIMNutrientID = AIMN.AIMNutrientID	
				LEFT OUTER JOIN
					(SELECT 
							ANX.AnalyzerNutrientID 
							,ANX.AIMNutrientID
							,AN.AnalyzerNutrientCode
							,AN.AnalyzerNutrientName
							,AN.UnitType
							,AN.DataSourceTypeID
						FROM
							dbo.AIMNutrientRefAnalyzerNutrient ANX
						INNER JOIN
							dbo.AnalyzerNutrient AN
							ON ANX.AnalyzerNutrientID = AN.AnalyzerNutrientID
						) ANX
						ON ANX.AIMNutrientID = NGD.AIMNutrientID
						AND ANX.DataSourceTypeID = ST.DataSourceTypeID
				LEFT OUTER JOIN
						(SELECT 
								AN.AnalyzerNutrientID
							, IsNull(ResultValue,0) as ResultValue
							, AN.DataSourceTypeID
						FROM 
								dbo.SampleResult SR
							INNER JOIN 
								dbo.AnalyzerNutrient AN
									ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
									AND SR.SampleID = @SampleID
									AND AN.IsActive = 1				
									AND SR.IsActive = 1									
							) SR
							ON SR.AnalyzerNutrientID = ANX.AnalyzerNutrientID
							AND SR.DataSourceTypeID = ST.DataSourceTypeID
				LEFT OUTER JOIN
					CFCUser.CFCUser U 
						ON STG.CFCUserID = U.CFCUserID
				LEFT OUTER JOIN
					dbo.WarningLimits WL
					ON WL.AIMNutrientID = ANX.AIMNutrientID
					AND WL.AIMMaterialID = @AIMMaterialID
			WHERE
				STG.SampleResultTemplateID = @SampleResultTemplateID
				AND  ST.CurrentIteration % STG.TestFrequency = 0
				AND STG.IsDateFrequency = 0
			ORDER BY
					CASE WHEN SortOrder = 0 THEN 1 ELSE 0 END,
							SortOrder
						, NGD.NutrientGroupDetailID

		SELECT 
				  STG.*	
				, ANX.AnalyzerNutrientCode
				, ANX.AnalyzerNutrientName
				, ANX.AnalyzerNutrientID
				, ANX.UnitType
				, AIMN.AIMNutrientName	
				, SR.ResultValue AS CurrentValue
				, U.UserName
				, SortOrder
				, NGD.NutrientGroupDetailID
				, WL.Min
				, WL.Max
		INTO 
			#GridResultIsDateTrue
			FROM 
				SampleResultTemplateDetailGroup STG
				INNER JOIN
					NutrientGroup NG
						ON NG.NutrientGroupID = STG.NutrientGroupID
				INNER JOIN
					NutrientGroupDetail NGD
						ON NG.NutrientGroupID = NGD.NutrientGroupID
				INNER JOIN
					SampleResultTemplate ST
						ON ST.SampleResultTemplateID = STG.SampleResultTemplateID
				INNER JOIN
					dbo.AIMNutrient AIMN
						ON NGD.AIMNutrientID = AIMN.AIMNutrientID	
				LEFT OUTER JOIN
					(SELECT 
							ANX.AnalyzerNutrientID 
							,ANX.AIMNutrientID
							,AN.AnalyzerNutrientCode
							,AN.AnalyzerNutrientName
							,AN.UnitType
							,AN.DataSourceTypeID
						FROM
							dbo.AIMNutrientRefAnalyzerNutrient ANX
						INNER JOIN
							dbo.AnalyzerNutrient AN
							ON ANX.AnalyzerNutrientID = AN.AnalyzerNutrientID
						) ANX
						ON ANX.AIMNutrientID = NGD.AIMNutrientID
						AND ANX.DataSourceTypeID = ST.DataSourceTypeID
				LEFT OUTER JOIN
						(SELECT 
								AN.AnalyzerNutrientID
							, IsNull(ResultValue,0) as ResultValue
							, AN.DataSourceTypeID
						FROM 
								dbo.SampleResult SR
							INNER JOIN 
								dbo.AnalyzerNutrient AN
									ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
									AND SR.SampleID = @SampleID
									AND AN.IsActive = 1				
									AND SR.IsActive = 1							
									) SR
							ON SR.AnalyzerNutrientID = ANX.AnalyzerNutrientID
							AND SR.DataSourceTypeID = ST.DataSourceTypeID
				LEFT OUTER JOIN
					CFCUser.CFCUser U 
						ON STG.CFCUserID = U.CFCUserID
				LEFT OUTER JOIN
					dbo.WarningLimits WL
					ON WL.AIMNutrientID = ANX.AIMNutrientID
					AND WL.AIMMaterialID = @AIMMaterialID
			WHERE
				STG.SampleResultTemplateID = @SampleResultTemplateID
				AND GETDATE() >= DATEADD(DAY, STG.TestFrequency, StartDate)
				AND STG.IsDateFrequency = 1
			ORDER BY
					CASE WHEN SortOrder = 0 THEN 1 ELSE 0 END,
							SortOrder
						, NGD.NutrientGroupDetailID

		SELECT 
			  GridBoth.* 
			, #Outside.Message as MessageOutside
			, #Inside.Message as MessageInside
			, ISNULL(CASE WHEN CurrentValue < Min THEN 1 ELSE CASE WHEN CurrentValue > Max THEN 2 ELSE 0 END END, 0) AS ResultValueFlag
		FROM 
		(
			SELECT  
				  SampleResultTemplateDetailGroupID
				, SampleResultTemplateID
				, NutrientGroupID
				, TestFrequency
				, IsDateFrequency
				, UpdateStamp
				, CFCUserID
				, AnalyzerNutrientCode
				, AnalyzerNutrientName
				, AnalyzerNutrientID
				, UnitType
				, AIMNutrientName	
				, CurrentValue
				, UserName
				, SortOrder
				, NutrientGroupDetailID
				, Min
				, Max
			FROM 
				#GridResultIsDateFalse
			UNION ALL
			SELECT  
				  SampleResultTemplateDetailGroupID
				, SampleResultTemplateID
				, NutrientGroupID
				, TestFrequency
				, IsDateFrequency
				, UpdateStamp
				, CFCUserID
				, AnalyzerNutrientCode
				, AnalyzerNutrientName
				, AnalyzerNutrientID
				, UnitType
				, AIMNutrientName	
				, CurrentValue
				, UserName
				, SortOrder
				, NutrientGroupDetailID
				, Min
				, Max
			FROM 
				#GridResultIsDateTrue
		) AS GridBoth

		LEFT OUTER JOIN #Outside 
			ON GridBoth.AnalyzerNutrientID = #Outside.AnalyzerNutrientID
		LEFT OUTER JOIN #Inside 
			ON GridBoth.AnalyzerNutrientID = #Inside.AnalyzerNutrientID
		ORDER BY 
			CASE WHEN GridBoth.SortOrder = 0 THEN 1 ELSE 0 END,
					GridBoth.SortOrder
				,   GridBoth.NutrientGroupDetailID
	END
END