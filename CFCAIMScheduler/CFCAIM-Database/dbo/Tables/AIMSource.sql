CREATE TABLE [dbo].[AIMSource] (
    [AIMSourceID]     INT           IDENTITY (1, 1) NOT NULL,
    [SourceName]      VARCHAR (50)  NULL,
    [IsActive]        BIT           CONSTRAINT [DF_AIMSource_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]     DATETIME      CONSTRAINT [DF_AIMSource_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT           NULL,
    [AIMSupplierCode] NVARCHAR (20) NULL,
    CONSTRAINT [PK_AIMSource] PRIMARY KEY CLUSTERED ([AIMSourceID] ASC)
);








GO
CREATE NONCLUSTERED INDEX [AIMSource_AIMSourceID]
    ON [dbo].[AIMSource]([AIMSourceID] ASC);

