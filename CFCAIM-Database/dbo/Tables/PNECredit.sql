CREATE TABLE [dbo].[PNECredit] (
    [PNECreditID]    INT          IDENTITY (1, 1) NOT NULL,
    [Credit]         INT          NULL,
    [PNEKey]         VARCHAR (40) NULL,
    [PNEDescription] VARCHAR (50) NULL,
    [UpdateStamp]    DATETIME     CONSTRAINT [DF_PNECredit_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]      INT          CONSTRAINT [DF_PNECredit_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_PNECredit] PRIMARY KEY CLUSTERED ([PNECreditID] ASC)
);

