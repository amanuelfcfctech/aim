CREATE TABLE [dbo].[PNEMaterialSampleTypeRefGroundUnGround] (
    [PNEMaterialSampleTypeRefGroundUnGroundID] INT      IDENTITY (1, 1) NOT NULL,
    [PNEMaterialSampleTypeID]                  INT      NULL,
    [GroundUnGroundID]                         INT      NULL,
    [UpdateStamp]                              DATETIME CONSTRAINT [DF_PNEMaterialSampleTypeRefGroundUnGround_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                INT      NULL,
    CONSTRAINT [PK_PNEMaterialSampleTypeRefGroundUnGround] PRIMARY KEY CLUSTERED ([PNEMaterialSampleTypeRefGroundUnGroundID] ASC)
);

