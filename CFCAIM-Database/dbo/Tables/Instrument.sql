CREATE TABLE [dbo].[Instrument] (
    [InstrumentID]     INT            IDENTITY (1, 1) NOT NULL,
    [InstrumentCode]   NVARCHAR (20)  NULL,
    [Description]      NVARCHAR (30)  NULL,
    [ArchivedFolder]   NVARCHAR (250) NULL,
    [ProximateFolder]  NVARCHAR (250) NULL,
    [SpectraFolder]    NVARCHAR (250) NULL,
    [DataSourceTypeID] INT            CONSTRAINT [DF_Instrument_DataSourceTypeID] DEFAULT ((0)) NULL,
    [FileTypeID]       INT            NULL,
    [UpdateStamp]      DATETIME       CONSTRAINT [DF_Instrument_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT            NULL,
    [IsActive]         BIT            CONSTRAINT [DF_Instrument_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_InstrumentID] PRIMARY KEY CLUSTERED ([InstrumentID] ASC)
);







