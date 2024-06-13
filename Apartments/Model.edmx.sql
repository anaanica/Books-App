
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/26/2024 17:39:07
-- Generated from EDMX file: C:\Users\ana\Desktop\ADPC_Exercise0506_Start\Apartments\Apartments\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Books];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [IdBook] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Isbn] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IdUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [BookIdBook] int  NOT NULL
);
GO

-- Creating table 'Authors'
CREATE TABLE [dbo].[Authors] (
    [IdAuthor] int IDENTITY(1,1) NOT NULL,
    [AuthorName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [IdGenre] int IDENTITY(1,1) NOT NULL,
    [GenreName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'BookAuthor'
CREATE TABLE [dbo].[BookAuthor] (
    [Books_IdBook] int  NOT NULL,
    [Authors_IdAuthor] int  NOT NULL
);
GO

-- Creating table 'BookGenre'
CREATE TABLE [dbo].[BookGenre] (
    [Books_IdBook] int  NOT NULL,
    [Genres_IdGenre] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdBook] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([IdBook] ASC);
GO

-- Creating primary key on [IdUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IdUploadedFile] ASC);
GO

-- Creating primary key on [IdAuthor] in table 'Authors'
ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [PK_Authors]
    PRIMARY KEY CLUSTERED ([IdAuthor] ASC);
GO

-- Creating primary key on [IdGenre] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([IdGenre] ASC);
GO

-- Creating primary key on [Books_IdBook], [Authors_IdAuthor] in table 'BookAuthor'
ALTER TABLE [dbo].[BookAuthor]
ADD CONSTRAINT [PK_BookAuthor]
    PRIMARY KEY CLUSTERED ([Books_IdBook], [Authors_IdAuthor] ASC);
GO

-- Creating primary key on [Books_IdBook], [Genres_IdGenre] in table 'BookGenre'
ALTER TABLE [dbo].[BookGenre]
ADD CONSTRAINT [PK_BookGenre]
    PRIMARY KEY CLUSTERED ([Books_IdBook], [Genres_IdGenre] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BookIdBook] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_BookUploadedFile]
    FOREIGN KEY ([BookIdBook])
    REFERENCES [dbo].[Books]
        ([IdBook])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookUploadedFile'
CREATE INDEX [IX_FK_BookUploadedFile]
ON [dbo].[UploadedFiles]
    ([BookIdBook]);
GO

-- Creating foreign key on [Books_IdBook] in table 'BookAuthor'
ALTER TABLE [dbo].[BookAuthor]
ADD CONSTRAINT [FK_BookAuthor_Book]
    FOREIGN KEY ([Books_IdBook])
    REFERENCES [dbo].[Books]
        ([IdBook])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Authors_IdAuthor] in table 'BookAuthor'
ALTER TABLE [dbo].[BookAuthor]
ADD CONSTRAINT [FK_BookAuthor_Author]
    FOREIGN KEY ([Authors_IdAuthor])
    REFERENCES [dbo].[Authors]
        ([IdAuthor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookAuthor_Author'
CREATE INDEX [IX_FK_BookAuthor_Author]
ON [dbo].[BookAuthor]
    ([Authors_IdAuthor]);
GO

-- Creating foreign key on [Books_IdBook] in table 'BookGenre'
ALTER TABLE [dbo].[BookGenre]
ADD CONSTRAINT [FK_BookGenre_Book]
    FOREIGN KEY ([Books_IdBook])
    REFERENCES [dbo].[Books]
        ([IdBook])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Genres_IdGenre] in table 'BookGenre'
ALTER TABLE [dbo].[BookGenre]
ADD CONSTRAINT [FK_BookGenre_Genre]
    FOREIGN KEY ([Genres_IdGenre])
    REFERENCES [dbo].[Genres]
        ([IdGenre])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookGenre_Genre'
CREATE INDEX [IX_FK_BookGenre_Genre]
ON [dbo].[BookGenre]
    ([Genres_IdGenre]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------