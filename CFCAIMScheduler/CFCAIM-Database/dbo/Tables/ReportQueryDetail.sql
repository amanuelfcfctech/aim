CREATE TABLE [dbo].[ReportQueryDetail] (
    [ReportQueryDetailID]       INT        IDENTITY (1, 1) NOT NULL,
    [ReportQueryID]             INT        NULL,
    [CFCUserID]                 INT        NULL,
    [UpdateStamp]               DATETIME   CONSTRAINT [DF_ReportQueryDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]                  BIT        CONSTRAINT [DF_ReportQueryDetail_IsActive] DEFAULT ((1)) NULL,
    [ReportQueryDetailAndOr]    NCHAR (10) NULL,
    [ReportQueryDetailField]    NCHAR (20) NULL,
    [ReportQueryDetailOperator] NCHAR (10) NULL,
    [ReportQueryDetailValue]    NCHAR (30) NULL
);

