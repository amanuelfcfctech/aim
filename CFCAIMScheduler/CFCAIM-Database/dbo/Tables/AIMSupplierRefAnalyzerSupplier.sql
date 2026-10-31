CREATE TABLE [dbo].[AIMSupplierRefAnalyzerSupplier] (
    [AIMSupplierRefAnalyzerSupplierID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMSupplierID]                    INT      NULL,
    [AnalyzerSupplierID]               INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_AIMSupplierRefAnalyzerSupplier_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL,
    CONSTRAINT [PK_AIMSupplierRefAnalyzerSupplier] PRIMARY KEY CLUSTERED ([AIMSupplierRefAnalyzerSupplierID] ASC)
);

