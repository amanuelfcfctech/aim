CREATE TABLE [dbo].[AIMSupplierLocationGroup] (
    [AIMSupplierLocationGroupID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMSupplierGroupID]         INT      NULL,
    [AIMSupplierID]              INT      NULL,
    [AIMSupplierLocationID]      INT      NULL,
    [IsActive]                   BIT      CONSTRAINT [DF_AIMSupplierLocationGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_AIMSupplierLocationGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                  INT      NULL,
    CONSTRAINT [PK_AIMSupplierLocationGroup] PRIMARY KEY CLUSTERED ([AIMSupplierLocationGroupID] ASC)
);

