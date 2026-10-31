CREATE TABLE [dbo].[SampleTestTypeDetail] (
    [SampleTestTypeDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleTestTypeID]       INT      NULL,
    [DataSourceTypeID]       INT      NULL,
    [SortOrder]              INT      NULL,
    [UpdateStamp]            DATETIME NULL,
    [CFCUserID]              INT      NULL,
    [IsActive]               BIT      NULL,
    CONSTRAINT [PK_SampleTestTypeDetail] PRIMARY KEY CLUSTERED ([SampleTestTypeDetailID] ASC)
);

