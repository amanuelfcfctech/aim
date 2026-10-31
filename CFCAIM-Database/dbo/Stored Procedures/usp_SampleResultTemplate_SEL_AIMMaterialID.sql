

CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL_AIMMaterialID]

@AIMMaterialID	INT

AS
BEGIN

--exec usp_SampleResultTemplate_SEL_AIMMaterialID 0

SET NOCOUNT ON;

--Task1061 DB251030 Start
--Added logic to inlcude prioritizing group or single templates.
--We pull the IsPrioritizeGroupTemplate from the table to determine which template to 
--set in the drop down when we load the sample on sample screen

DECLARE @IsPrioritizeGroupTemplate BIT = 0
DECLARE @SampleResultTemplateID_Default INT = 0

SELECT @IsPrioritizeGroupTemplate = IsPrioritizeGroupTemplate
FROM	dbo.SystemSettings

IF @IsPrioritizeGroupTemplate = 0
	BEGIN
		-- Rule 1
		SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID

		FROM 
			SampleResultTemplate
		WHERE
			IsGroupTemplate = 0
			AND AIMMaterialID = @AIMMaterialID
		ORDER BY
			SampleResultTemplateID

		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0 
				-- Rule 2
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 0
					AND AIMMaterialID = 0
				ORDER BY
					SampleResultTemplateID

		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0 
				-- Rule 3
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 1
					AND AIMMaterialID = @AIMMaterialID
				ORDER BY
					SampleResultTemplateID

		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0 
				-- Rule 3
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 1
					AND AIMMaterialID = 0
				ORDER BY
					SampleResultTemplateID
	END
IF @IsPrioritizeGroupTemplate = 1
	BEGIN
				-- Rule 3
		SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 1
					AND AIMMaterialID = @AIMMaterialID
				ORDER BY
					SampleResultTemplateID

		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0 
				-- Rule 3
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 1
					AND AIMMaterialID = 0
				ORDER BY
					SampleResultTemplateID
		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 0
					AND AIMMaterialID = @AIMMaterialID
				ORDER BY
					SampleResultTemplateID

		IF ISNULL(@SampleResultTemplateID_Default, 0) = 0 
				-- Rule 2
				SELECT TOP 1 @SampleResultTemplateID_Default = SampleResultTemplateID
				FROM 
					SampleResultTemplate
				WHERE
					IsGroupTemplate = 0
					AND AIMMaterialID = 0
				ORDER BY
					SampleResultTemplateID
	END

--Task1061 DB251030 End

SELECT 
	  SR.*
	, DST.Name as DataSourceTypeName
	, AM.AIMMaterialName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	, @SampleResultTemplateID_Default AS SampleResultTemplateID_Default

FROM 

	dbo.SampleResultTemplate SR WITH(NOLOCK)

	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON SR.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN
		dbo.AIMMaterial AM with (nolock)
			ON SR.AIMMaterialID = AM.AIMMaterialID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SR.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)				
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID


-- 3/20/18
--WHERE AM.AIMMaterialID = @AIMMaterialID
WHERE SR.AIMMaterialID = @AIMMaterialID or SR.AIMMaterialID = 0


ORDER BY SR.AIMMaterialID DESC

END