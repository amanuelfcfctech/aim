CREATE TABLE [dbo].[AnalyzerNutrient] (
    [AnalyzerNutrientID]   INT             IDENTITY (1, 1) NOT NULL,
    [AnalyzerNutrientCode] NVARCHAR (100)  NULL,
    [AnalyzerNutrientName] NVARCHAR (100)  NULL,
    [IsActive]             BIT             CONSTRAINT [DF_AnalyzerNutrient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]          DATETIME        CONSTRAINT [DF_AnalyzerNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT             NULL,
    [DataSourceTypeID]     INT             CONSTRAINT [DF_AnalyzerNutrient_DataSourceTypeID] DEFAULT ((0)) NULL,
    [UnitType]             NVARCHAR (15)   NULL,
    [ConversionFactor]     NUMERIC (18, 9) CONSTRAINT [DF_AnalyzerNutrient_ConversionFactor] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_AnalyzerNutrient] PRIMARY KEY CLUSTERED ([AnalyzerNutrientID] ASC)
);














GO


