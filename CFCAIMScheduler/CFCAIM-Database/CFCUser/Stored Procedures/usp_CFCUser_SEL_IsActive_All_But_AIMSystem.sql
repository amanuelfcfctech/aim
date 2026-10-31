CREATE PROCEDURE [CFCUser].[usp_CFCUser_SEL_IsActive_All_But_AIMSystem]
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
	, C.SpecificIngredientsGroupID
	, C.AIMOwnerLocationGroupID
	, C.SpecificProductsGroupID
	, C.AIMSupplierGroupID
	, C.AIMSupplierLocationGroupID	
	, C.SampleTestTypeGroupID				--10/4/23
	, C.C5UserName							--10/4/23
	


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
	AND C.UserName != 'AIM System'
END