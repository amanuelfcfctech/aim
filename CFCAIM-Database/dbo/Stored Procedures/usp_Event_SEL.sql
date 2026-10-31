CREATE PROCEDURE [dbo].[usp_Event_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	E.EventID
	, E.EventName
	, E.EventDescription
	, E.EventRunTypeID
	, E.EventTypeID
	, E.StartDateTime	
	, E.LastRunDateTime	
	, E.TemplateID	
	, E.EventStatus
	, E.UpdateStamp
	, E.CFCUserID
	, ET.EventRunTypeName	
	, IC.Name as SystemImportName
	, UDDSD.ImportFileName
	, SE.TemplateName as ExportName
	, case when IC.Name is not null then IC.Name else case when UDDSD.ImportFileName is not null then UDDSD.ImportFileName else case when se.TemplateName is not null then se.TemplateName else '' end end end as [FileName]

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

	
FROM 
	dbo.Event E WITH(NOLOCK)

LEFT OUTER JOIN
	dbo.EventRunType ET with (nolock)
		ON E.EventRunTypeID = ET.EventRunTypeID

LEFT OUTER JOIN
	dbo.ImportConfig IC with (nolock)
		ON E.TemplateID = IC.ImportConfigID

LEFT OUTER JOIN
	dbo.UserDefinedDataSourceDefinition UDDSD with (nolock)
		ON E.TemplateID = UDDSD.UserDefinedDataSourceDefinitionID

LEFT OUTER JOIN
	dbo.SampleExportTemplate SE with (nolock)
		ON E.TemplateID = SE.SampleExportTemplateID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON E.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

END