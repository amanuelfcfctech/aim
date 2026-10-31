CREATE TABLE [dbo].[SampleQualityEquation] (
    [SampleQualityEquationID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleQualityEquationName] NVARCHAR (50) NULL,
    [AnalyzerNutrientID]        INT           NULL,
    [Equation]                  VARCHAR (200) NULL,
    [IsActive]                  BIT           CONSTRAINT [DF_SampleQualityEquation_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                 INT           NULL,
    [UpdateStamp]               DATETIME      CONSTRAINT [DF_SampleQualityEquation_UpdateStamp] DEFAULT (getdate()) NULL,
    [DataSourceTypeID]          INT           NULL,
    CONSTRAINT [PK_SampleQualityEquation] PRIMARY KEY CLUSTERED ([SampleQualityEquationID] ASC)
);

