CREATE TABLE [dbo].[SampleType] (
    [SampleTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleTypeName] NVARCHAR (50) NULL,
    [UpdateStamp]    DATETIME      CONSTRAINT [DF_SampleType_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]       BIT           CONSTRAINT [DF_SampleType_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]      INT           NULL,
    CONSTRAINT [PK_SampleType] PRIMARY KEY CLUSTERED ([SampleTypeID] ASC)
);

