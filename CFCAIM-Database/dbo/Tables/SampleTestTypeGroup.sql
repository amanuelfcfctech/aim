CREATE TABLE [dbo].[SampleTestTypeGroup] (
    [SampleTestTypeGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleTestTypeGroupCode] NVARCHAR (50) NULL,
    [IsActive]                BIT           CONSTRAINT [DF_SampleTestTypeGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]             DATETIME      CONSTRAINT [DF_SampleTestTypeGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT           NULL,
    CONSTRAINT [PK_SampleTestTypeGroup] PRIMARY KEY CLUSTERED ([SampleTestTypeGroupID] ASC)
);

