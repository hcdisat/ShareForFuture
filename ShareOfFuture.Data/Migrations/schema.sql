IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [OfferingDeviceCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [ParentCategoryId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_OfferingDeviceCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfferingDeviceCategories_OfferingDeviceCategories_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [OfferingDeviceCategories] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OfferingTags] (
    [Id] int NOT NULL IDENTITY,
    [Tag] nvarchar(max) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_OfferingTags] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserGroup] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_UserGroup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(450) NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [Lastname] nvarchar(100) NOT NULL,
    [Street] nvarchar(150) NOT NULL,
    [City] nvarchar(50) NOT NULL,
    [State] nvarchar(50) NOT NULL,
    [Country] nvarchar(50) NOT NULL,
    [ZipCode] nvarchar(6) NOT NULL,
    [PhoneNumber] nvarchar(11) NULL,
    [GroupId] int NULL,
    [EmailVerifiedDate] datetimeoffset NULL,
    [LastLogingDate] datetimeoffset NULL,
    [IdentityProvider] int NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_UserGroup_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [UserGroup] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Complains] (
    [Id] int NOT NULL IDENTITY,
    [ComplainerId] int NULL,
    [ComplaineeId] int NULL,
    [AssignedToId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Complains] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Complains_Users_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Complains_Users_ComplaineeId] FOREIGN KEY ([ComplaineeId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Complains_Users_ComplainerId] FOREIGN KEY ([ComplainerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OfferingDevices] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Condition] int NOT NULL,
    [LastVerificationDate] datetimeoffset NOT NULL DEFAULT '2021-10-17T15:27:50.4649081-04:00',
    [OwnerId] int NULL,
    [CategoryId] int NULL,
    [MarkedAsUnavailableDate] datetimeoffset NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_OfferingDevices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfferingDevices_OfferingDeviceCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [OfferingDeviceCategories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OfferingDevices_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [ComplainNotes] (
    [Id] int NOT NULL IDENTITY,
    [ComplainId] int NULL,
    [Note] nvarchar(max) NULL,
    [Picture] varbinary(max) NULL,
    [DoneDate] datetimeoffset NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_ComplainNotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ComplainNotes_Complains_ComplainId] FOREIGN KEY ([ComplainId]) REFERENCES [Complains] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [DeviceUnavailabilityPeriods] (
    [Id] int NOT NULL IDENTITY,
    [From] datetimeoffset NOT NULL,
    [Until] datetimeoffset NULL,
    [OfferingId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_DeviceUnavailabilityPeriods] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_DeviceUnavailabilityPeriods_UntilAfterFrom] CHECK ([Until] IS NULL
                OR [Until] > [From]),
    CONSTRAINT [FK_DeviceUnavailabilityPeriods_OfferingDevices_OfferingId] FOREIGN KEY ([OfferingId]) REFERENCES [OfferingDevices] ([Id])
);
GO

CREATE TABLE [OfferingDeviceImages] (
    [Id] int NOT NULL IDENTITY,
    [ImageURL] nvarchar(max) NOT NULL,
    [OfferingId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_OfferingDeviceImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OfferingDeviceImages_OfferingDevices_OfferingId] FOREIGN KEY ([OfferingId]) REFERENCES [OfferingDevices] ([Id])
);
GO

CREATE TABLE [OfferingOfferingTag] (
    [OfferingsId] int NOT NULL,
    [TagsId] int NOT NULL,
    CONSTRAINT [PK_OfferingOfferingTag] PRIMARY KEY ([OfferingsId], [TagsId]),
    CONSTRAINT [FK_OfferingOfferingTag_OfferingDevices_OfferingsId] FOREIGN KEY ([OfferingsId]) REFERENCES [OfferingDevices] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OfferingOfferingTag_OfferingTags_TagsId] FOREIGN KEY ([TagsId]) REFERENCES [OfferingTags] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Sharings] (
    [Id] int NOT NULL IDENTITY,
    [BorrowerId] int NULL,
    [OfferingId] int NULL,
    [From] datetimeoffset NOT NULL,
    [Until] datetimeoffset NOT NULL,
    [LastRequestNotificationSentDate] datetimeoffset NULL,
    [OfferingWasAccepted] bit NULL,
    [AcceptedDeclinedDate] datetimeoffset NULL,
    [AcceptOrDeclineMessage] nvarchar(max) NOT NULL,
    [ShareActivationDate] datetimeoffset NULL,
    [ShareDoneDate] datetimeoffset NULL,
    [LenderToBorrowerRating] int NULL,
    [BorrowerToLenderRating] int NULL,
    [BorrowerToOffering] int NULL,
    [LenderToBorrowerNotes] nvarchar(max) NOT NULL,
    [BorrowerToLenderNotes] nvarchar(max) NOT NULL,
    [BorrowerToOfferingNotes] nvarchar(max) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Sharings] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Sharings_UntilAfterFrom] CHECK ([Until] > [From]),
    CONSTRAINT [FK_Sharings_OfferingDevices_OfferingId] FOREIGN KEY ([OfferingId]) REFERENCES [OfferingDevices] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sharings_Users_BorrowerId] FOREIGN KEY ([BorrowerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] ON;
INSERT INTO [UserGroup] ([Id], [CreatedAt], [Name], [UpdatedAt])
VALUES (1, '2021-10-17T15:27:50.4658243-04:00', N'S4FEmployee', '2021-10-17T15:27:50.4658264-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] ON;
INSERT INTO [UserGroup] ([Id], [CreatedAt], [Name], [UpdatedAt])
VALUES (2, '2021-10-17T15:27:50.4658269-04:00', N'Manager', '2021-10-17T15:27:50.4658271-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] ON;
INSERT INTO [UserGroup] ([Id], [CreatedAt], [Name], [UpdatedAt])
VALUES (3, '2021-10-17T15:27:50.4658275-04:00', N'SystemAdministrator', '2021-10-17T15:27:50.4658277-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[UserGroup]'))
    SET IDENTITY_INSERT [UserGroup] OFF;
GO

CREATE INDEX [IX_ComplainNotes_ComplainId] ON [ComplainNotes] ([ComplainId]);
GO

CREATE INDEX [IX_Complains_AssignedToId] ON [Complains] ([AssignedToId]);
GO

CREATE INDEX [IX_Complains_ComplaineeId] ON [Complains] ([ComplaineeId]);
GO

CREATE INDEX [IX_Complains_ComplainerId] ON [Complains] ([ComplainerId]);
GO

CREATE UNIQUE INDEX [IX_DeviceUnavailabilityPeriods_OfferingId] ON [DeviceUnavailabilityPeriods] ([OfferingId]) WHERE [OfferingId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_OfferingDeviceCategories_Name] ON [OfferingDeviceCategories] ([Name]);
GO

CREATE INDEX [IX_OfferingDeviceCategories_ParentCategoryId] ON [OfferingDeviceCategories] ([ParentCategoryId]);
GO

CREATE INDEX [IX_OfferingDeviceImages_OfferingId] ON [OfferingDeviceImages] ([OfferingId]);
GO

CREATE INDEX [IX_OfferingDevices_CategoryId] ON [OfferingDevices] ([CategoryId]);
GO

CREATE INDEX [IX_OfferingDevices_OwnerId] ON [OfferingDevices] ([OwnerId]);
GO

CREATE INDEX [IX_OfferingOfferingTag_TagsId] ON [OfferingOfferingTag] ([TagsId]);
GO

CREATE INDEX [IX_Sharings_BorrowerId] ON [Sharings] ([BorrowerId]);
GO

CREATE INDEX [IX_Sharings_OfferingId] ON [Sharings] ([OfferingId]);
GO

CREATE UNIQUE INDEX [IX_UserGroup_Name] ON [UserGroup] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

CREATE INDEX [IX_Users_GroupId] ON [Users] ([GroupId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211017192750_Init', N'6.0.0-rc.2.21480.5');
GO

COMMIT;
GO

