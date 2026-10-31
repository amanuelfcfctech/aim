CREATE TABLE [dbo].[SampleQualityControlMassEntryDetail] (
    [SampleQualityControlMassEntryDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [SampleQualityControlMassEntryID]       INT             NULL,
    [SampleQualityParameterID]              INT             NULL,
    [SortOrder]                             INT             NULL,
    [UpdateStamp]                           DATETIME        NULL,
    [CFCUserID]                             INT             NULL,
    [ResultValue]                           NUMERIC (18, 8) NULL,
    [ResultValueString]                     VARCHAR (15)    NULL,
    CONSTRAINT [PKSampleQualityControlMassEntryDetail] PRIMARY KEY CLUSTERED ([SampleQualityControlMassEntryDetailID] ASC)
);

