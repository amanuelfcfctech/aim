CREATE TABLE [dbo].[AIMOwnerLocation] (
    [AIMOwnerLocationID] INT           IDENTITY (1, 1) NOT NULL,
    [AIMOwnerID]         INT           NOT NULL,
    [LocationCode]       NVARCHAR (20) NULL,
    [LocationName]       NVARCHAR (50) NULL,
    [IsActive]           BIT           CONSTRAINT [DF_AIMOwnerLocation_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]        DATETIME      CONSTRAINT [DF_AIMOwnerLocation_CreatedDate] DEFAULT (getdate()) NULL,
    [CFCUserID]          INT           NULL,
    [IsFormulationPlant] BIT           NULL,
    CONSTRAINT [PK_AimOwnerLocation] PRIMARY KEY CLUSTERED ([AIMOwnerLocationID] ASC)
);










GO
CREATE NONCLUSTERED INDEX [AIMOwnerLocation_AIMOwnerLocationID]
    ON [dbo].[AIMOwnerLocation]([AIMOwnerLocationID] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMOwnerLocation_AIMOwnerID]
    ON [dbo].[AIMOwnerLocation]([AIMOwnerID] ASC);


GO
CREATE NONCLUSTERED INDEX [AIMOwnerLocation_AIMOwnerLocationIDAIMOwnerIDIsActive]
    ON [dbo].[AIMOwnerLocation]([AIMOwnerLocationID] ASC, [AIMOwnerID] ASC, [IsActive] ASC);

