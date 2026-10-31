CREATE TABLE [dbo].[AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined] (
    [AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerSupplierLocationCombinedID]                               INT      NULL,
    [AIMSupplierID]                                                    INT      NULL,
    [AIMSupplierLocationID]                                            INT      NULL,
    [UpdateStamp]                                                      DATETIME CONSTRAINT [DF_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                                        INT      NULL,
    CONSTRAINT [PK_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined] PRIMARY KEY CLUSTERED ([AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID] ASC)
);

