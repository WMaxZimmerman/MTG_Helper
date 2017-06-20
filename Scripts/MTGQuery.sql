--/*
Drop Table [dbo].[CardSets]
Drop Table [dbo].[CardColors]
Drop Table [dbo].[CardSubTypes]
Drop Table [dbo].[CardTypes]
Drop Table [dbo].[Cards]
Drop Table [dbo].[Sets]
Drop Table [dbo].[Colors]
Drop Table [dbo].[SubTypes]
Drop Table [dbo].[Types]
Drop Table [dbo].[DeckCards]
Drop Table [dbo].[Deck]
--*/

CREATE TABLE [dbo].[Types] (
    TypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    TypeName varchar(255) Unique
);

CREATE TABLE [dbo].[SubTypes] (
    SubTypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    SubTypeName varchar(255) Unique
);

CREATE TABLE [dbo].[Colors] (
    ColorId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ColorName varchar(255) Unique
);

CREATE TABLE [dbo].[Cards] (
    [CardId] varchar(255) PRIMARY KEY,
    [CardName] varchar(255) Unique,
    [Url] nvarchar(MAX),
    [StoreUrl] nvarchar(MAX),
    [ConvertedManaCost] nvarchar(MAX),
	[Cost] nvarchar(MAX),
	[RulesText] nvarchar(MAX),
	[Power] nvarchar(MAX),
	[Toughness] nvarchar(MAX),
	[Commander] nvarchar(MAX),
	[Legacy] nvarchar(MAX),
	[Modern] nvarchar(MAX),
	[Standard] nvarchar(MAX),
	[Vintage] nvarchar(MAX),
	[Types] nvarchar(MAX),
	[SubTypes] nvarchar(MAX),
	[Colors] nvarchar(MAX)
);

CREATE TABLE [dbo].[Sets] (
    [SetId] varchar(255) PRIMARY KEY,
    [SetName] varchar(255) Unique,
	[Border] nvarchar(MAX),
	[SetType] nvarchar(MAX),
    [Url] nvarchar(MAX),
	[SetNumber] int
);

CREATE TABLE [dbo].[CardTypes] (
    [CardTypeId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CardId] varchar(255) NOT NULL,
	[TypeId] int NOT NULL,

	--Foreign Keys	
    FOREIGN KEY (CardId) REFERENCES [dbo].[Cards](CardId),
    FOREIGN KEY (TypeId) REFERENCES [dbo].[Types](TypeID)
);

CREATE TABLE [dbo].[CardSubTypes] (
    [CardSubTypeId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CardId] varchar(255) NOT NULL,
	[SubTypeId] int NOT NULL,

	--Foreign Keys	
    FOREIGN KEY (CardId) REFERENCES [dbo].[Cards](CardId),
    FOREIGN KEY (SubTypeId) REFERENCES [dbo].[SubTypes](SubTypeID)
);

CREATE TABLE [dbo].[CardColors] (
    [CardTypeId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CardId] varchar(255) NOT NULL,
	[ColorId] int NOT NULL,

	--Foreign Keys	
    FOREIGN KEY (CardId) REFERENCES [dbo].[Cards](CardId),
    FOREIGN KEY (ColorId) REFERENCES [dbo].[Colors](ColorId)
);

CREATE TABLE [dbo].[CardSets] (
    [CardSetId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CardId] varchar(255) NOT NULL,
	[SetId] varchar(255) NOT NULL,
	[CardNumber] nvarchar(MAX),
	[MultiverseId] BigInt NOT NULL,
	[Artist] nvarchar(MAX),
	[Rarity] nvarchar(MAX),
	[FlavorText] nvarchar(MAX),
	[ImageUrl] nvarchar(MAX),
	[StoreUrl] nvarchar(MAX),
	[HighPrice] decimal,
	[MedianPrice] decimal,
	[LowPrice] decimal,

	--Foreign Keys	
    FOREIGN KEY (CardId) REFERENCES [dbo].[Cards](CardId),
    FOREIGN KEY (SetId) REFERENCES [dbo].[Sets](SetId)
);

CREATE TABLE [dbo].[Deck] (
    [DeckName] varchar(255) NOT NULL PRIMARY KEY,
	[Commander] varchar(255) NOT NULL,

	--Foreign Keys
	FOREIGN KEY (Commander) REFERENCES [dbo].[Cards](CardId)
)

CREATE TABLE [dbo].[DeckCards] (
    [DeckCardsId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [DeckName] varchar(255) NOT NULL,
	[CardId] varchar(255) NOT NULL,
	[Quantity] int NOT NULL DEFAULT 1,

	--Foreign Keys
	FOREIGN KEY (DeckName) REFERENCES [dbo].[Deck](DeckName),	
	FOREIGN KEY (CardId) REFERENCES [dbo].[Cards](CardId)
)































