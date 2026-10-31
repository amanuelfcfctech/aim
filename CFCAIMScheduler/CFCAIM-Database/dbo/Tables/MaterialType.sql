CREATE TABLE [dbo].[MaterialType] (
    [MaterialTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [MaterialTypeName] NVARCHAR (50) NULL,
    [UpdateStamp]      DATETIME      CONSTRAINT [DF_MaterialType_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]         BIT           CONSTRAINT [DF_MaterialType_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]        INT           NULL,
    CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED ([MaterialTypeID] ASC)
);

