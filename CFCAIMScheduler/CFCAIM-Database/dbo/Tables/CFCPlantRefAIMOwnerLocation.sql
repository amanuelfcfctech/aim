CREATE TABLE [dbo].[CFCPlantRefAIMOwnerLocation] (
    [CFCPlantRefAIMOwnerLocationID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCPlantID]                    INT      NULL,
    [AIMOwnerLocationID]            INT      NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_CFCPlantRefAIMOwnerLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      NULL
);




GO
CREATE NONCLUSTERED INDEX [CFCPlantRefAIMOwnerLocation_CFCPlantID]
    ON [dbo].[CFCPlantRefAIMOwnerLocation]([CFCPlantID] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCPlantRefAIMOwnerLocation_AIMOwnerLocationID]
    ON [dbo].[CFCPlantRefAIMOwnerLocation]([AIMOwnerLocationID] ASC);

