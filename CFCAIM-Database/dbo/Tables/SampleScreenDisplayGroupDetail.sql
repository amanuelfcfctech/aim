CREATE TABLE [dbo].[SampleScreenDisplayGroupDetail] (
    [SampleScreenDisplayGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleScreenDisplayGroupID]       INT      NULL,
    [ControlID]                        INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_SampleScreenDisplayDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL,
    CONSTRAINT [PK_SampleScreenDisplayDetailID] PRIMARY KEY CLUSTERED ([SampleScreenDisplayGroupDetailID] ASC)
);

