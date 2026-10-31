CREATE TABLE [dbo].[SampleQuality] (
    [SampleQualityID]          INT             IDENTITY (1, 1) NOT NULL,
    [SampleQualityParameterID] INT             NULL,
    [SampleID]                 INT             NULL,
    [ResultValueNumeric]       NUMERIC (18, 9) NULL,
    [ResultValueString]        VARCHAR (50)    NULL,
    [IsActive]                 BIT             CONSTRAINT [DF_SampleQuality_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                INT             NULL,
    [UpdateStamp]              DATETIME        CONSTRAINT [DF_SampleQuality_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SampleQuality] PRIMARY KEY CLUSTERED ([SampleQualityID] ASC)
);

