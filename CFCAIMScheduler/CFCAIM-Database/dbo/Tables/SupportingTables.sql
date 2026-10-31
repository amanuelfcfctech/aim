CREATE TABLE [dbo].[SupportingTables] (
    [SupportingTablesID] INT           IDENTITY (1, 1) NOT NULL,
    [TableName]          NVARCHAR (50) NULL,
    [KeyName]            NVARCHAR (50) NULL,
    [IdentityColumn]     BIT           NULL
);



