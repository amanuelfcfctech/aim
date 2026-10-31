CREATE TABLE [dbo].[AnalysisViewTemplate] (
    [AnalysisViewTemplateID]   INT            IDENTITY (1, 1) NOT NULL,
    [AnalysisViewTemplateCode] NCHAR (20)     NULL,
    [AnalysisViewTemplateName] NCHAR (30)     NULL,
    [AnalysisViewTemplateXML]  NVARCHAR (MAX) NULL,
    [UpdateStamp]              DATETIME       CONSTRAINT [DF_AnalysisViewTemplate_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT            NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_AnalysisViewTemplate_IsActive] DEFAULT ((1)) NULL,
    [GridNumber]               INT            NULL,
    [TemplateType]             INT            NULL,
    [AIMMaterialID]            INT            NULL,
    [AIMOwnerID]               INT            NULL,
    [AIMOwnerLocationID]       INT            NULL,
    [AIMSupplierID]            INT            NULL,
    [AIMSupplierLocationID]    INT            NULL,
    [MaterialTypeID]           INT            NULL
);







