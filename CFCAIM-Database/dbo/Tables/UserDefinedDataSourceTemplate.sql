CREATE TABLE [dbo].[UserDefinedDataSourceTemplate] (
    [UserDefinedDataSourceTemplateID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserDefinedDataSourceDefinitionID] INT           NULL,
    [SourceColumnName]                  NVARCHAR (30) NULL,
    [SourceColumnNumber]                INT           NULL,
    [MapColumnID]                       INT           NULL,
    [MapColumnName]                     NVARCHAR (30) NULL,
    [IsManualEntry]                     BIT           NULL,
    [UpdateStamp]                       DATETIME      CONSTRAINT [DF_UserDefinedDataSourceTemplate_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                         INT           NULL,
    [IsDetail]                          BIT           NULL,
    [UseAsSampleCode]                   BIT           NULL,
    CONSTRAINT [PK_UserDefinedDataSourceTemplate] PRIMARY KEY CLUSTERED ([UserDefinedDataSourceTemplateID] ASC)
);







