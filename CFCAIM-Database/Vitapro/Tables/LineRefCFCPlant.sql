CREATE TABLE [Vitapro].[LineRefCFCPlant] (
    [LineRefCFCPlantID] INT          IDENTITY (1, 1) NOT NULL,
    [LineID]            INT          NULL,
    [CFCPlantID]        INT          NULL,
    [UpdateStamp]       DATETIME     CONSTRAINT [DF_LineRefCFCPlant_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]          BIT          CONSTRAINT [DF_LineRefCFCPlant_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]         INT          NULL,
    [LineName]          VARCHAR (50) NULL,
    CONSTRAINT [PK_LineRefCFCPlant] PRIMARY KEY CLUSTERED ([LineRefCFCPlantID] ASC)
);



