CREATE TABLE [dbo].[AIMOwnerLocationGroup] (
    [AIMOwnerLocationGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [AIMOwnerLocationGroupCode] NVARCHAR (50) NULL,
    [IsActive]                  BIT           CONSTRAINT [DF_AIMOwnerLocationGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]               DATETIME      CONSTRAINT [DF_AIMOwnerLocationGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                 INT           NULL,
    CONSTRAINT [PK_AIMOwnerLocationGroup] PRIMARY KEY CLUSTERED ([AIMOwnerLocationGroupID] ASC)
);

