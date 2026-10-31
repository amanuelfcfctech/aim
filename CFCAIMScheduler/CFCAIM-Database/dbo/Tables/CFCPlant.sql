CREATE TABLE [dbo].[CFCPlant] (
    [CFCPlantID]    INT           IDENTITY (1, 1) NOT NULL,
    [CFCPlantCode]  NVARCHAR (20) NULL,
    [CFCPlantName]  VARCHAR (40)  NULL,
    [IsActive]      BIT           CONSTRAINT [DF_CFCPlant_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]   DATETIME      NULL,
    [AIMDatabaseID] INT           NULL,
    [CFCUserID]     INT           NULL,
    CONSTRAINT [PK_CFCPlant] PRIMARY KEY CLUSTERED ([CFCPlantID] ASC)
);








GO
CREATE NONCLUSTERED INDEX [CFCPlant_CFCPlantCode]
    ON [dbo].[CFCPlant]([CFCPlantCode] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCPlant_CFCPlantID]
    ON [dbo].[CFCPlant]([CFCPlantID] ASC);

