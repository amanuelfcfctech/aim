
			create PROCEDURE [dbo].[usp_Version_SEL_cm]			
			AS
			BEGIN
			SET NOCOUNT ON;
			SELECT TOP 1 	  ID 	, [Version]	, CreatedDate
			FROM 	dbo.[Version] WITH(NOLOCK)
			ORDER BY	CreatedDate DESC
			END