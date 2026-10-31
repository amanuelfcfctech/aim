CREATE TABLE [dbo].[SampleQualityControlMassEntry] (
    [SampleQualityControlMassEntryID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleQualityControlMassEntryName] NVARCHAR (50) NULL,
    [UpdateStamp]                       DATETIME      NULL,
    [CFCUserID]                         INT           NULL,
    [DataSourceTypeID]                  INT           NULL,
    CONSTRAINT [PK_SampleQualityControlMassEntry] PRIMARY KEY CLUSTERED ([SampleQualityControlMassEntryID] ASC)
);

