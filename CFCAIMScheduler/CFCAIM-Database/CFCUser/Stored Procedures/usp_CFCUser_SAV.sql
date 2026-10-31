CREATE PROCEDURE [CFCUser].[usp_CFCUser_SAV]
	  @CFCUserID							INT
	, @ApplicationName                      VARCHAR(50)
    , @UserName                             VARCHAR(50)
    , @Password                             VARCHAR(50)
    , @OldPassword                          VARCHAR(50)
	, @LastLoginDate						DATETIME 
    , @FirstName							VARCHAR(50)
	, @LastName								VARCHAR(50)
	, @EmailAddress							VARCHAR(50) 	= NULL

	
	, @SpecificIngredientsGroupID			INT = NULL				--10/25/22	
	, @AIMOwnerLocationGroupID				INT = NULL				--11/22/22	
	, @SpecificProductsGroupID				INT = NULL				--5/18/23	
	, @AIMSupplierGroupID					INT = NULL				--8/28/23
	, @AIMSupplierLocationGroupID			INT = NULL
	, @SampleTestTypeGroupID				INT=NULL				--10/4/23
	, @C5UserName							NVARCHAR(8)				--10/4/23
	
	
-- exec CFCUser.usp_CFCUser_SAV 1, 'CFCAIM', 'Admin', 'Admin', '2022-01-03', 'Admin', 'Admin', 'Admin', null, null

AS
BEGIN
SET NOCOUNT ON;
		/*declaration for [dbo].[aspnet_Membership_CreateUser]*/
		DECLARE	@return_value		INT
		DECLARE @PasswordSalt		VARCHAR
		DECLARE @Email				VARCHAR
		DECLARE @PasswordQuestion	VARCHAR
		DECLARE @PasswordAnswer		VARCHAR
		DECLARE @IsApproved			VARCHAR
		DECLARE @UserID				UNIQUEIDENTIFIER
		
		--1/3/22
		--DECLARE @CurrentTimeUtc		DATETIME = CONVERT( VARCHAR(10),GETDATE(), 101)
		DECLARE @CurrentTimeUtc		DATETIME = CONVERT( VARCHAR(10),GETDATE(), 120)

		/*
		declaration for 
		[dbo].[aspnet_Membership_UpdateUser]
		and
		[dbo].[aspnet_Membership_SetPassword]
		*/
		--DECLARE @LastLoginDate DATETIME
		DECLARE @LastActivityDate DATETIME
		DECLARE @UniqueEmail INT
		DECLARE @PasswordFormat INT
				
IF @CFCUserID = 0
	BEGIN
		EXEC	  @return_value = [dbo].[aspnet_Membership_CreateUser]
				  @ApplicationName = @ApplicationName
				, @UserName = @UserName
				, @Password = @Password
				, @CurrentTimeUtc = @CurrentTimeUtc
				, @PasswordSalt = ''
				, @Email = null
				, @PasswordQuestion = null
				, @PasswordAnswer = null
				, @IsApproved = true
				, @UserId = @UserId OUTPUT

		INSERT INTO
			CFCUser.CFCUser WITH (ROWLOCK)
			(
				  UserID
				, FirstName
				, LastName
				, EmailAddress
				, IsActive
				, UserName				
				, SpecificIngredientsGroupID				--10/25/22
				, AIMOwnerLocationGroupID					--11/22/22
				, SpecificProductsGroupID					--5/18/23
				, AIMSupplierGroupID						--8/28/23
				, AIMSupplierLocationGroupID				--8/28/23
				, SampleTestTypeGroupID						--10/4/23
				, C5UserName								--10/4/23
				
								
			)
			VALUES
			(
				  @UserID
				, @FirstName
				, @LastName
				, @EmailAddress
				, 1
				, @UserName				
				, @SpecificIngredientsGroupID				--10/25/22
				, @AIMOwnerLocationGroupID					--11/22/22
				, @SpecificProductsGroupID					--5/18/23
				, @AIMSupplierGroupID						--8/28/23
				, @AIMSupplierLocationGroupID				--8/28/23
				, @SampleTestTypeGroupID					--10/4/23
				, @C5UserName								--10/4/23
				
			)
			
			SET @CFCUserID = @@IDENTITY 
		END
		
ELSE
		/*
		comment is ntext in the proc aspnet_Membership_UpdateUser
		we cannot declare it locally, its sql's problem
		http://msdn.microsoft.com/en-us/library/ms187993(v=sql.100).aspx
		in the above page its said that they going to remove in the
		future version MS SQL
		*/
	BEGIN
		EXEC	  dbo.aspnet_Membership_UpdateUser
				  @ApplicationName
				, @UserName
				, @Email
				, null --Comment
				, 1--@IsApproved
				, @LastLoginDate
				, @CurrentTimeUtc--@LastActivityDate
				, @UniqueEmail
				, @CurrentTimeUtc
						
		EXEC	  dbo.aspnet_Membership_SetPassword
				  @ApplicationName
				, @UserName
				, @Password
				, ''--@PasswordSalt
				, @CurrentTimeUtc
				, 0 --@PasswordFormat
		
		UPDATE CFCUser.CFCUser
			SET FirstName = @FirstName,
				LastName = @LastName,
				EmailAddress = @EmailAddress
				,SpecificIngredientsGroupID = @SpecificIngredientsGroupID			--10/25/22
				,AIMOwnerLocationGroupID = @AIMOwnerLocationGroupID					--11/22/22
				,SpecificProductsGroupID = @SpecificProductsGroupID					--5/18/23
				,AIMSupplierGroupID = @AIMSupplierGroupID							--8/28/23
				,AIMSupplierLocationGroupID = @AIMSupplierLocationGroupID			--8/28/23
				,SampleTestTypeGroupID = @SampleTestTypeGroupID						--10/4/23
				,C5UserName = @C5UserName											--10/4/23
				


			WHERE CFCUserID = @CFCUserID
	END
	SELECT @CFCUserID AS CFCUserID
END


