CREATE TABLE [dbo].[AnalystTemplate] (
    [AnalysisTemplateID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalysisTemplateName] NVARCHAR (50) NULL,
    [MathType]             INT           NULL,
    [IsActive]             BIT           CONSTRAINT [DF_AnalystTemplate_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_AnalystTemplate_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_AnalystTemplate] PRIMARY KEY CLUSTERED ([AnalysisTemplateID] ASC)
);

