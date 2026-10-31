CREATE TABLE [dbo].[MenuItems] (
    [MenuItemsID] INT          IDENTITY (1, 1) NOT NULL,
    [ParentNo]    INT          NOT NULL,
    [Level1]      INT          NULL,
    [Level2]      INT          NULL,
    [Level3]      INT          NULL,
    [Level4]      INT          NULL,
    [Level5]      INT          NULL,
    [Description] VARCHAR (50) NULL,
    [SortOrder]   INT          NULL,
    [IsActive]    BIT          NULL,
    CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED ([MenuItemsID] ASC, [ParentNo] ASC)
);

