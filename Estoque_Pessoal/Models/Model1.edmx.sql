
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2017 20:37:06
-- Generated from EDMX file: C:\Users\jujub\OneDrive\Documentos\Visual Studio 2015\Projects\Estoque_Pessoal\Estoque_Pessoal\Models\Model1.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClienteSet'
CREATE TABLE [dbo].[ClienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [cep] nvarchar(max)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [senha] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EstoqueSet'
CREATE TABLE [dbo].[EstoqueSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantidade] int  NOT NULL,
    [Cliente_Id] int  NOT NULL,
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

-- Creating primary key on [Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [Item_Id] in table 'EstoqueSet'
ALTER TABLE [dbo].[EstoqueSet]
ADD CONSTRAINT [FK_ItemEstoque]
    FOREIGN KEY ([Item_Id])
    REFERENCES [dbo].[ItemSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemEstoque'
CREATE INDEX [IX_FK_ItemEstoque]
ON [dbo].[EstoqueSet]
    ([Item_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------