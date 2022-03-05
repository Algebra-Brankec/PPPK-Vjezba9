
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/23/2022 19:13:16
-- Generated from EDMX file: C:\Users\brand\Projects\Algebra\PPPK\PPPK-Vjezba9\ApartmentsManger\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Apartments];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ApartmentUploadedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedFiles] DROP CONSTRAINT [FK_ApartmentUploadedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_ApartmentDescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Descriptions] DROP CONSTRAINT [FK_ApartmentDescription];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Apartments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apartments];
GO
IF OBJECT_ID(N'[dbo].[UploadedFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UploadedFiles];
GO
IF OBJECT_ID(N'[dbo].[Descriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Descriptions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Apartments'
CREATE TABLE [dbo].[Apartments] (
    [IDApartment] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [City] nvarchar(20)  NOT NULL,
    [Contact] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(50)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [ApartmentIDApartment] int  NOT NULL
);
GO

-- Creating table 'Descriptions'
CREATE TABLE [dbo].[Descriptions] (
    [IDDescription] int IDENTITY(1,1) NOT NULL,
    [Floor] nvarchar(max)  NOT NULL,
    [LivingSpace] nvarchar(max)  NOT NULL,
    [NumberOfRooms] nvarchar(max)  NOT NULL,
    [ApartmentType] nvarchar(max)  NOT NULL,
    [EnergyClass] nvarchar(max)  NOT NULL,
    [ApartmentIDApartment] nvarchar(max)  NOT NULL,
    [Apartment_IDApartment] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDApartment] in table 'Apartments'
ALTER TABLE [dbo].[Apartments]
ADD CONSTRAINT [PK_Apartments]
    PRIMARY KEY CLUSTERED ([IDApartment] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- Creating primary key on [IDDescription] in table 'Descriptions'
ALTER TABLE [dbo].[Descriptions]
ADD CONSTRAINT [PK_Descriptions]
    PRIMARY KEY CLUSTERED ([IDDescription] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ApartmentIDApartment] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_ApartmentUploadedFile]
    FOREIGN KEY ([ApartmentIDApartment])
    REFERENCES [dbo].[Apartments]
        ([IDApartment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApartmentUploadedFile'
CREATE INDEX [IX_FK_ApartmentUploadedFile]
ON [dbo].[UploadedFiles]
    ([ApartmentIDApartment]);
GO

-- Creating foreign key on [Apartment_IDApartment] in table 'Descriptions'
ALTER TABLE [dbo].[Descriptions]
ADD CONSTRAINT [FK_ApartmentDescription]
    FOREIGN KEY ([Apartment_IDApartment])
    REFERENCES [dbo].[Apartments]
        ([IDApartment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApartmentDescription'
CREATE INDEX [IX_FK_ApartmentDescription]
ON [dbo].[Descriptions]
    ([Apartment_IDApartment]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------