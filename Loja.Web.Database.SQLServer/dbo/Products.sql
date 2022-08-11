﻿-- To be compared.

CREATE TABLE [dbo].[Products]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(300)		NOT NULL,
	[Description]		VARCHAR(MAX)		NOT NULL,
	[Price]				MONEY				NOT NULL,
	[Discount]			FLOAT				CONSTRAINT [DF_Product_Discount]	DEFAULT (0) NOT NULL,
	[SubcategoryID]		INT					NOT NULL,
	[ManufacturerID]	INT					NOT NULL,
	[Peso]				FLOAT,
	[Altura]			FLOAT,
	[Largura]			FLOAT,
	[Comprimento]		FLOAT,
	[Stock]				INT					CONSTRAINT [DF_Product_Stock]		DEFAULT (0) NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Product_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Product_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Product_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Products_SubcategoryID] FOREIGN KEY([SubcategoryID])
	REFERENCES Subcategories([ID])

	CONSTRAINT [FK_Products_ManufacturerID] FOREIGN KEY([ManufacturerID])
	REFERENCES Manufacturers([ID])
)
