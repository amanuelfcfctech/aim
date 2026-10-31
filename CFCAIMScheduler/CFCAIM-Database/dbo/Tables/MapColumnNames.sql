CREATE TABLE [dbo].[MapColumnNames] (
    [SampleMapColumnNamesID] INT           IDENTITY (1, 1) NOT NULL,
    [TableName]              NVARCHAR (50) NULL,
    [MapColumnID]            INT           NULL,
    [MapColumnName]          NVARCHAR (50) NULL,
    [Description]            NVARCHAR (50) NULL,
    [SortNumber]             INT           NULL,
    [IsDetail]               BIT           NULL,
    [CanAlter]               BIT           NULL,
    [AlternateNameID]        INT           NULL,
    CONSTRAINT [PK_MapColumnNames] PRIMARY KEY CLUSTERED ([SampleMapColumnNamesID] ASC)
);





