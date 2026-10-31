CREATE TABLE [dbo].[AnalyzerSupplierLocationCombined] (
    [AnalyzerSupplierLocationCombinedID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerSupplierLocationCombinedName] NVARCHAR (50) NULL,
    [DataSourceTypeID]                     INT           NULL,
    [IsActive]                             BIT           CONSTRAINT [DF_AnalyzerSupplierLocationCombined_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                          DATETIME      CONSTRAINT [DF_AnalyzerSupplierLocationCombined_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                            INT           NULL,
    CONSTRAINT [PK_AnalyzerSupplierLocationCombined] PRIMARY KEY CLUSTERED ([AnalyzerSupplierLocationCombinedID] ASC)
);

