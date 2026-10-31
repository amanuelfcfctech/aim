CREATE TABLE [dbo].[AIMOwner] (
    [AIMOwnerID]  INT           IDENTITY (1, 1) NOT NULL,
    [OwnerCode]   NVARCHAR (20) NULL,
    [OwnerName]   NVARCHAR (50) NULL,
    [IsActive]    BIT           CONSTRAINT [DF_AIMOwner_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp] DATETIME      CONSTRAINT [DF_AIMOwner_CreatedDate] DEFAULT (getdate()) NULL,
    [CFCUserID]   INT           NULL,
    CONSTRAINT [PK_AimOwner] PRIMARY KEY CLUSTERED ([AIMOwnerID] ASC)
);






GO
CREATE NONCLUSTERED INDEX [AIMOwner_AIMOwnerID]
    ON [dbo].[AIMOwner]([AIMOwnerID] ASC);

