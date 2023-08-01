CREATE TABLE [dbo].[ApplicationUser] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (100) NOT NULL,
    [Userpassword] VARCHAR (30)  NOT NULL,
    [RoleId]       INT           NOT NULL,
    [IsActive]     INT           NOT NULL,
    [CreatedBy]    INT           NOT NULL,
    [CreatedOn]    DATETIME      NOT NULL,
    [UpdatedBy]    INT           NOT NULL,
    [UpdatedOn]    DATETIME      NOT NULL,
    [IsDeleted]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationUser_ApplicationRole] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[ApplicationRole] ([Id]),
    CONSTRAINT [FK_AppUserCreated_AppUserId] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_AppUserUpdated_AppUserId] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[ApplicationUser] ([Id])
);

