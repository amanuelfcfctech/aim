CREATE TABLE [dbo].[Control] (
    [ControlID]          INT           IDENTITY (1, 1) NOT NULL,
    [ControlName]        VARCHAR (50)  NULL,
    [FormID]             INT           NULL,
    [UpdateStamp]        DATETIME      CONSTRAINT [DF_Control_UpdateStamp] DEFAULT (getdate()) NULL,
    [UserControlName]    NVARCHAR (50) NULL,
    [CFCUserID]          INT           NULL,
    [AllowAlternateName] BIT           NULL,
    [AllowTabOrder]      BIT           NULL,
    [AllowFilter]        BIT           NULL,
    [RequiredField]      BIT           NULL,
    [LinkToControlID]    INT           NULL,
    [AllowUserDefault]   BIT           NULL,
    CONSTRAINT [PK_Control] PRIMARY KEY CLUSTERED ([ControlID] ASC)
);









