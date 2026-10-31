CREATE TABLE [dbo].[UnitType] (
    [UnitTypeID]          INT           IDENTITY (1, 1) NOT NULL,
    [UnitTypeDescription] NVARCHAR (30) NULL,
    [UpdateStamp]         DATETIME      CONSTRAINT [DF_UnitType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]           INT           NULL,
    [UnitTypeCode]        NVARCHAR (15) NULL,
    [IsActive]            BIT           CONSTRAINT [DF_UnitType_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_UnitType] PRIMARY KEY CLUSTERED ([UnitTypeID] ASC)
);