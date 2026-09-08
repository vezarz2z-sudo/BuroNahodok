IF DB_ID(N'BuroNahodok') IS NULL
BEGIN
    CREATE DATABASE BuroNahodok;
END
GO

USE BuroNahodok;
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
        Login NVARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Login UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        Role NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT (SYSDATETIME())
    );
END
GO

IF OBJECT_ID(N'dbo.Employees', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        EmployeeId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Employees PRIMARY KEY,
        UserId INT NULL,
        FullName NVARCHAR(200) NOT NULL,
        Phone NVARCHAR(50) NULL,
        Email NVARCHAR(150) NULL,
        Position NVARCHAR(100) NULL,
        CONSTRAINT FK_Employees_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
    );
END
GO

IF OBJECT_ID(N'dbo.Owners', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Owners
    (
        OwnerId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Owners PRIMARY KEY,
        FullName NVARCHAR(200) NOT NULL,
        Phone NVARCHAR(50) NULL,
        Email NVARCHAR(150) NULL,
        Description NVARCHAR(500) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.FindPlaces', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.FindPlaces
    (
        PlaceId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_FindPlaces PRIMARY KEY,
        PlaceName NVARCHAR(200) NOT NULL,
        Address NVARCHAR(300) NULL,
        Description NVARCHAR(500) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Statuses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Statuses
    (
        StatusId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Statuses PRIMARY KEY,
        StatusName NVARCHAR(100) NOT NULL CONSTRAINT UQ_Statuses_StatusName UNIQUE,
        Description NVARCHAR(500) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.FoundItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.FoundItems
    (
        FoundItemId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_FoundItems PRIMARY KEY,
        ItemName NVARCHAR(200) NOT NULL,
        Category NVARCHAR(100) NOT NULL,
        Description NVARCHAR(1000) NULL,
        Color NVARCHAR(100) NULL,
        Brand NVARCHAR(100) NULL,
        FoundDate DATE NOT NULL,
        FoundTime TIME NULL,
        PlaceId INT NULL,
        StatusId INT NULL,
        EmployeeId INT NULL,
        OwnerId INT NULL,
        StorageLocation NVARCHAR(200) NULL,
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_FoundItems_CreatedAt DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_FoundItems_FindPlaces FOREIGN KEY (PlaceId) REFERENCES dbo.FindPlaces(PlaceId),
        CONSTRAINT FK_FoundItems_Statuses FOREIGN KEY (StatusId) REFERENCES dbo.Statuses(StatusId),
        CONSTRAINT FK_FoundItems_Employees FOREIGN KEY (EmployeeId) REFERENCES dbo.Employees(EmployeeId),
        CONSTRAINT FK_FoundItems_Owners FOREIGN KEY (OwnerId) REFERENCES dbo.Owners(OwnerId)
    );
END
GO

IF OBJECT_ID(N'dbo.LostReports', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LostReports
    (
        LostReportId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LostReports PRIMARY KEY,
        OwnerId INT NOT NULL,
        ItemName NVARCHAR(200) NOT NULL,
        Category NVARCHAR(100) NOT NULL,
        Description NVARCHAR(1000) NULL,
        Color NVARCHAR(100) NULL,
        Brand NVARCHAR(100) NULL,
        LostDate DATE NULL,
        LostPlace NVARCHAR(300) NULL,
        Status NVARCHAR(100) NOT NULL CONSTRAINT DF_LostReports_Status DEFAULT (N'Ищется'),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_LostReports_CreatedAt DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_LostReports_Owners FOREIGN KEY (OwnerId) REFERENCES dbo.Owners(OwnerId)
    );
END
GO

IF OBJECT_ID(N'dbo.ItemIssues', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ItemIssues
    (
        IssueId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ItemIssues PRIMARY KEY,
        FoundItemId INT NOT NULL,
        OwnerId INT NOT NULL,
        EmployeeId INT NULL,
        IssueDate DATETIME2 NOT NULL CONSTRAINT DF_ItemIssues_IssueDate DEFAULT (SYSDATETIME()),
        DocumentNumber NVARCHAR(100) NULL,
        Comment NVARCHAR(1000) NULL,
        CONSTRAINT FK_ItemIssues_FoundItems FOREIGN KEY (FoundItemId) REFERENCES dbo.FoundItems(FoundItemId),
        CONSTRAINT FK_ItemIssues_Owners FOREIGN KEY (OwnerId) REFERENCES dbo.Owners(OwnerId),
        CONSTRAINT FK_ItemIssues_Employees FOREIGN KEY (EmployeeId) REFERENCES dbo.Employees(EmployeeId)
    );
END
GO

IF OBJECT_ID(N'dbo.Notifications', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Notifications
    (
        NotificationId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Notifications PRIMARY KEY,
        OwnerId INT NOT NULL,
        FoundItemId INT NULL,
        Message NVARCHAR(1000) NOT NULL,
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Notifications_CreatedAt DEFAULT (SYSDATETIME()),
        IsRead BIT NOT NULL CONSTRAINT DF_Notifications_IsRead DEFAULT (0),
        CONSTRAINT FK_Notifications_Owners FOREIGN KEY (OwnerId) REFERENCES dbo.Owners(OwnerId),
        CONSTRAINT FK_Notifications_FoundItems FOREIGN KEY (FoundItemId) REFERENCES dbo.FoundItems(FoundItemId)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Statuses WHERE StatusName = N'Найдена')
    INSERT INTO dbo.Statuses (StatusName, Description) VALUES (N'Найдена', N'Вещь зарегистрирована в бюро находок');
IF NOT EXISTS (SELECT 1 FROM dbo.Statuses WHERE StatusName = N'На хранении')
    INSERT INTO dbo.Statuses (StatusName, Description) VALUES (N'На хранении', N'Вещь находится на хранении');
IF NOT EXISTS (SELECT 1 FROM dbo.Statuses WHERE StatusName = N'Владелец найден')
    INSERT INTO dbo.Statuses (StatusName, Description) VALUES (N'Владелец найден', N'Установлен предполагаемый владелец');
IF NOT EXISTS (SELECT 1 FROM dbo.Statuses WHERE StatusName = N'Выдана')
    INSERT INTO dbo.Statuses (StatusName, Description) VALUES (N'Выдана', N'Вещь выдана владельцу');
IF NOT EXISTS (SELECT 1 FROM dbo.Statuses WHERE StatusName = N'Утилизирована')
    INSERT INTO dbo.Statuses (StatusName, Description) VALUES (N'Утилизирована', N'Вещь выведена из хранения');
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Login = N'admin')
BEGIN
    INSERT INTO dbo.Users (Login, PasswordHash, Role, IsActive)
    VALUES (N'admin', N'1234', N'Администратор', 1);
END
GO

SELECT N'База данных BuroNahodok успешно создана и подготовлена.' AS Result;
GO
