CREATE TABLE [dbo].[CFCPlantRefAIMOwnerLocationLog] (
    [CFCPlantRefAIMOwnerLocationLogID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCPlantID]                       INT      NULL,
    [AIMOwnerLocationID]               INT      NULL,
    [LogStatus]                        CHAR (1) NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_CFCPlantRefAIMOwnerLocationLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL
);

