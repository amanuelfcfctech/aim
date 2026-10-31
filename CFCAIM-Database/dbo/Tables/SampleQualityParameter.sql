CREATE TABLE [dbo].[SampleQualityParameter] (
    [SampleQualityParameterID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleQualityParameterName] NVARCHAR (50) NULL,
    [DataSourceTypeID]           INT           NULL,
    [ParameterIsNumeric]         BIT           CONSTRAINT [DF_SampleQualityParameter_IsNumeric] DEFAULT ((1)) NULL,
    [IsActive]                   BIT           CONSTRAINT [DF_SampleQualityParameter_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                  INT           NULL,
    [UpdateStamp]                DATETIME      CONSTRAINT [DF_SampleQualityParameter_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SampleQualityParameter] PRIMARY KEY CLUSTERED ([SampleQualityParameterID] ASC)
);

