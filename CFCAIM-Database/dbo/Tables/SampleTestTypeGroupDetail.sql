CREATE TABLE [dbo].[SampleTestTypeGroupDetail] (
    [SampleTestTypeGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleTestTypeGroupID]       INT      NULL,
    [SampleTestTypeID]            INT      NULL,
    [IsActive]                    BIT      CONSTRAINT [DF_SampleTestTypeGroupDetail_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                 DATETIME CONSTRAINT [DF_SampleTestTypeGroupDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                   INT      NULL,
    CONSTRAINT [PK_SampleTestTypeGroupDetail] PRIMARY KEY CLUSTERED ([SampleTestTypeGroupDetailID] ASC)
);

