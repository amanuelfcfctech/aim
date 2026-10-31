CREATE TABLE [dbo].[SampleMapColumnNames] (
    [SampleMapColumnNamesID] INT           IDENTITY (1, 1) NOT NULL,
    [TableName]              NVARCHAR (50) NULL,
    [MapColumnID]            INT           NULL,
    [MapColumnName]          NVARCHAR (50) NULL,
    [Description]            NVARCHAR (50) NULL,
    [SortNumber]             INT           NULL,
    [IsDetail]               BIT           NULL,
    CONSTRAINT [PK_SampleMapColumnNames_cm] PRIMARY KEY CLUSTERED ([SampleMapColumnNamesID] ASC)
);



