CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive_SampleDate]
		@IsActive				BIT
		, @StartDate			DATE
		, @EndDate				DATE

		, @MaterialTypeID		INT
			

-- exec usp_AIMMaterial_SEL_IsActive_SampleDate 1, '2021-01-01', '2023-07-23', 1			

AS
BEGIN
SET NOCOUNT ON;


SELECT 
	  AM.AIMMaterialID
	, AM.MaterialTypeID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, AM.AIMOwnerLocationID
	, IsNull(AOL.LocationName, '') as AIMOwnerLocationName
	, U.UserName

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	INNER JOIN
		(Select AIMMaterialID 
		From dbo.Sample 
		Where SampleDate >= @StartDate and SampleDate <= @EndDate 
		Group By AIMMaterialID) x
			ON am.aimmaterialid = x.aimmaterialid

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
WHERE 
	AM.IsActive = @IsActive AND
	AM.MaterialTypeID = @MaterialTypeID

--order by removed for CSA speed issue
--ORDER BY AM.AIMMaterialCode


END