CREATE TABLE [dbo].[QueryField] (
    [QueryFieldID]   INT           IDENTITY (1, 1) NOT NULL,
    [Field]          NVARCHAR (20) NULL,
    [IsActive]       BIT           CONSTRAINT [DF_QueryField_IsActive] DEFAULT ((0)) NULL,
    [UpdateStamp]    DATETIME      CONSTRAINT [DF_QueryField_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]      INT           CONSTRAINT [DF_QueryField_CFCUserID] DEFAULT ((0)) NULL,
    [FieldName]      NVARCHAR (30) NULL,
    [QueryFieldType] INT           NULL,
    [Field2]         NVARCHAR (20) NULL,
    [FieldName2]     NVARCHAR (20) NULL,
    CONSTRAINT [PK_QueryField] PRIMARY KEY CLUSTERED ([QueryFieldID] ASC)
);



