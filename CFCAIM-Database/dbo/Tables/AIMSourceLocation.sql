CREATE TABLE [dbo].[AIMSourceLocation] (
    [AIMSourceLocationID]     INT           IDENTITY (1, 1) NOT NULL,
    [AIMSourceID]             INT           NULL,
    [SourceLocationName]      VARCHAR (50)  NULL,
    [IsActive]                BIT           CONSTRAINT [DF_AIMSourceLocation_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]             DATETIME      CONSTRAINT [DF_AIMSourceLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT           NULL,
    [AIMSupplierLocationCode] NVARCHAR (20) NULL,
    CONSTRAINT [PK_AIMSourceLocation] PRIMARY KEY CLUSTERED ([AIMSourceLocationID] ASC)
);








GO
CREATE NONCLUSTERED INDEX [AIMSourceLocation_AIMSourceLocationID]
    ON [dbo].[AIMSourceLocation]([AIMSourceLocationID] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMSourceLocation_AIMSourceID]
    ON [dbo].[AIMSourceLocation]([AIMSourceID] ASC);

