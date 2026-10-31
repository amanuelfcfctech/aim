CREATE TABLE [dbo].[AIMOwnerLocationGroupDetail] (
    [AIMOwnerLocationGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMOwnerLocationGroupID]       INT      NULL,
    [AIMOwnerLocationID]            INT      NULL,
    [IsActive]                      BIT      CONSTRAINT [DF_AIMOwnerLocationGroupDetail_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_AIMOwnerLocationGroupDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      NULL,
    CONSTRAINT [PK_AIMOwnerLocationGroupDetail] PRIMARY KEY CLUSTERED ([AIMOwnerLocationGroupDetailID] ASC)
);

