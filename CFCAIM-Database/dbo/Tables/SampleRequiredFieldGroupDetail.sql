CREATE TABLE [dbo].[SampleRequiredFieldGroupDetail] (
    [SampleRequiredFieldGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleRequiredFieldGroupID]       INT      NOT NULL,
    [RequiredControlID]                INT      NOT NULL,
    [IsActive]                         BIT      CONSTRAINT [DF_SampleRequiredFieldGroupDetail_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                        INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_SampleRequiredFieldGroupDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SampleRequiredFieldGroupDetail] PRIMARY KEY CLUSTERED ([SampleRequiredFieldGroupDetailID] ASC)
);

