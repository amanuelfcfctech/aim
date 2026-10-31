CREATE TABLE [dbo].[TestGroupName] (
    [TestGroupNameID] INT           IDENTITY (1, 1) NOT NULL,
    [TestGroupName]   NVARCHAR (50) NULL,
    [UpdateStamp]     DATETIME      NULL,
    [CFCUserID]       INT           NULL
);

