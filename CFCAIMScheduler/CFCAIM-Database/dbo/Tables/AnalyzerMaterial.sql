CREATE TABLE [dbo].[AnalyzerMaterial] (
    [AnalyzerMaterialID]   INT           IDENTITY (1, 1) NOT NULL,
    [DataSourceTypeID]     INT           CONSTRAINT [DF_AnalyzerMaterial_DataSourceTypeID] DEFAULT ((0)) NULL,
    [AnalyzerMaterialCode] NVARCHAR (20) NULL,
    [AnalyzerMaterialName] NVARCHAR (30) NULL,
    [IsActive]             BIT           CONSTRAINT [DF_AnalyzerMaterial_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_AnalyzerMaterial_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_AnalyzerMaterial] PRIMARY KEY CLUSTERED ([AnalyzerMaterialID] ASC)
);







