CREATE TABLE [dbo].[AIMSupplierGroupDetail] (
    [AIMSupplierGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMSupplierGroupID]       INT      NULL,
    [AIMSupplierID]            INT      NULL,
    [IsActive]                 BIT      CONSTRAINT [DF_AIMSupplierGroupDetail_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]              DATETIME CONSTRAINT [DF_AIMSupplierGroupDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT      NULL,
    CONSTRAINT [PK_AIMSupplierGroupDetail] PRIMARY KEY CLUSTERED ([AIMSupplierGroupDetailID] ASC)
);

