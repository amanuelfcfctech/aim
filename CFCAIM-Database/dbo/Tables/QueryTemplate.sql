CREATE TABLE [dbo].[QueryTemplate] (
    [QueryTemplateID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]                   NVARCHAR (30) NULL,
    [UpdateStamp]            DATETIME      CONSTRAINT [DF_QueryTemplate_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]              INT           CONSTRAINT [DF_QueryTemplate_CFCUserID] DEFAULT ((0)) NULL,
    [ExportTargetID]         INT           NULL,
    [AIMOwnerLocationID]     INT           NULL,
    [IsAnalysis]             BIT           CONSTRAINT [DF_QueryTemplate_IsReport] DEFAULT ((1)) NULL,
    [IncludeExcludeCrossRef] BIT           NULL,
    [HideInAnalysis]         BIT           NULL,
    [IngredientOrProduct]    NVARCHAR (1)  NULL,
    CONSTRAINT [PK_QueryTemplate] PRIMARY KEY CLUSTERED ([QueryTemplateID] ASC)
);







