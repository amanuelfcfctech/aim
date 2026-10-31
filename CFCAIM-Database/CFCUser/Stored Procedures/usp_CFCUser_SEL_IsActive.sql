

-------------------------------------------------
CREATE PROCEDURE [CFCUser].[usp_CFCUser_SEL_IsActive]
	@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;
SELECT
	  C.CFCUserID
	, C.FirstName
	, C.LastName
	, C.IsActive
	, A.ApplicationName	
	, M.Password
	, M.CreateDate
	, M.LastLoginDate
	, C.EmailAddress
	, LastActiveDataMapID
	, C.UserName	
	, C.SpecificIngredientsGroupID				-- 10/25/22
	, C.AIMOwnerLocationGroupID					-- 11/22/22
	, C.SpecificProductsGroupID					-- 5/18/23
	, C.AIMSupplierGroupID						-- 8/28/23
	, C.AIMSupplierLocationGroupID				-- 8/28/23
	, C.SampleTestTypeGroupID					-- 10/4/23
	, C.C5UserName								-- 10/4/23
	, C.TabOrderGroupID						    --5/29/25	Task1041 DB
	, C.SampleScreenDisplayGroupID				--7/7/26	SampleScreenDisplay FG
	, ISNULL(C.SampleRequiredFieldGroupID, 0) AS SampleRequiredFieldGroupID		--8/4/26	Task1155 RequiredField FG
FROM
	dbo.aspnet_Applications A
	INNER JOIN
		dbo.aspnet_Users U WITH(NOLOCK)
			ON A.ApplicationId = U.ApplicationId
	INNER JOIN
		CFCUser.CFCUser C WITH(NOLOCK)
			ON C.UserID = U.UserId
	INNER JOIN
		dbo.aspnet_Membership M WITH(NOLOCK)
			ON U.UserId = M.UserId
WHERE
	IsActive = @IsActive
	AND C.UserName != 'admin'
END