CREATE TABLE [dbo].[AIMMaterial] (
    [AIMMaterialID]      INT           IDENTITY (1, 1) NOT NULL,
    [MaterialTypeID]     INT           NULL,
    [AIMMaterialCode]    NVARCHAR (20) NULL,
    [AIMMaterialName]    NVARCHAR (30) NULL,
    [IsActive]           BIT           CONSTRAINT [DF_AIMMaterial_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]        DATETIME      CONSTRAINT [DF_AIMMaterial_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]          INT           NULL,
    [AIMOwnerLocationID] INT           NULL,
    CONSTRAINT [PK_AIMMaterial] PRIMARY KEY CLUSTERED ([AIMMaterialID] ASC)
);










GO
CREATE NONCLUSTERED INDEX [AIMMaterial_AIMMaterialID]
    ON [dbo].[AIMMaterial]([AIMMaterialID] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMMaterial_AIMOwnerLocationID]
    ON [dbo].[AIMMaterial]([AIMOwnerLocationID] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMMaterial_AIMMaterialCode]
    ON [dbo].[AIMMaterial]([AIMMaterialCode] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMMaterial_IsActive]
    ON [dbo].[AIMMaterial]([IsActive] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMMaterial_AIMOwnerLocationIDIsActive]
    ON [dbo].[AIMMaterial]([AIMOwnerLocationID] ASC, [IsActive] ASC);

