
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/11/2017 16:35:52
-- Generated from EDMX file: C:\Users\jujub\onedrive\documentos\visual studio 2015\Projects\Estoque_Pessoal\Estoque_Pessoal\Models\Modelos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EstoquePessoal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ItemEstoqueItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstoqueItemSet] DROP CONSTRAINT [FK_ItemEstoqueItem];
GO
IF OBJECT_ID(N'[dbo].[FK_EstoqueItemEstoque]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstoqueSet] DROP CONSTRAINT [FK_EstoqueItemEstoque];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteEstoque]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstoqueSet] DROP CONSTRAINT [FK_ClienteEstoque];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClienteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClienteSet];
GO
IF OBJECT_ID(N'[dbo].[EstoqueSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstoqueSet];
GO
IF OBJECT_ID(N'[dbo].[EstoqueItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstoqueItemSet];
GO
IF OBJECT_ID(N'[dbo].[ItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClienteSet'
CREATE TABLE [dbo].[ClienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [CEP] nvarchar(max)  NOT NULL,
    [Telefone] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Senha] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EstoqueSet'
CREATE TABLE [dbo].[EstoqueSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EstoqueItem_Id] int  NOT NULL,
    [Cliente_Id] int  NOT NULL
);
GO

-- Creating table 'EstoqueItemSet'
CREATE TABLE [dbo].[EstoqueItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantidade] int  NOT NULL,
    [Item_Id] int  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ClienteSet'
ALTER TABLE [dbo].[ClienteSet]
ADD CONSTRAINT [PK_ClienteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EstoqueSet'
ALTER TABLE [dbo].[EstoqueSet]
ADD CONSTRAINT [PK_EstoqueSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EstoqueItemSet'
ALTER TABLE [dbo].[EstoqueItemSet]
ADD CONSTRAINT [PK_EstoqueItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Item_Id] in table 'EstoqueItemSet'
ALTER TABLE [dbo].[EstoqueItemSet]
ADD CONSTRAINT [FK_ItemEstoqueItem]
    FOREIGN KEY ([Item_Id])
    REFERENCES [dbo].[ItemSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemEstoqueItem'
CREATE INDEX [IX_FK_ItemEstoqueItem]
ON [dbo].[EstoqueItemSet]
    ([Item_Id]);
GO

-- Creating foreign key on [EstoqueItem_Id] in table 'EstoqueSet'
ALTER TABLE [dbo].[EstoqueSet]
ADD CONSTRAINT [FK_EstoqueItemEstoque]
    FOREIGN KEY ([EstoqueItem_Id])
    REFERENCES [dbo].[EstoqueItemSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstoqueItemEstoque'
CREATE INDEX [IX_FK_EstoqueItemEstoque]
ON [dbo].[EstoqueSet]
    ([EstoqueItem_Id]);
GO

-- Creating foreign key on [Cliente_Id] in table 'EstoqueSet'
ALTER TABLE [dbo].[EstoqueSet]
ADD CONSTRAINT [FK_ClienteEstoque]
    FOREIGN KEY ([Cliente_Id])
    REFERENCES [dbo].[ClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteEstoque'
CREATE INDEX [IX_FK_ClienteEstoque]
ON [dbo].[EstoqueSet]
    ([Cliente_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------