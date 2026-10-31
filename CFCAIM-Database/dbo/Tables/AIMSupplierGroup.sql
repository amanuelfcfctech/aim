CREATE TABLE [dbo].[AIMSupplierGroup] (
    [AIMSupplierGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [AIMSupplierGroupCode] NVARCHAR (50) NULL,
    [IsActive]             BIT           CONSTRAINT [DF_AIMSupplierGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_AIMSupplierGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_AIMSupplierGroup] PRIMARY KEY CLUSTERED ([AIMSupplierGroupID] ASC)
);

