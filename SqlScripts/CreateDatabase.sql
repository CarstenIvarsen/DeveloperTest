-----------------------------------------------------------------------
-- NOTE: Remember to correct file paths for database creation
-----------------------------------------------------------------------

CREATE DATABASE [DeveloperTest] ON PRIMARY ( 
    NAME = N'DeveloperTest', FILENAME = N'D:\_Databases\DeveloperTest.mdf' , SIZE = 65536KB , FILEGROWTH = 65536KB 
)
LOG ON ( 
    NAME = N'DeveloperTest_log', FILENAME = N'D:\_Databases\DeveloperTest_log.ldf' , SIZE = 65536KB , FILEGROWTH = 65536KB 
)
GO

USE [DeveloperTest]
GO

CREATE TABLE [dbo].[WatchList] (
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Created] [DATETIME2](7) NOT NULL DEFAULT (SYSDATETIME()),
	[Word] [NVARCHAR](256) NOT NULL,
    CONSTRAINT [PK_WatchList] PRIMARY KEY CLUSTERED ( [Id] ASC )
) WITH (DATA_COMPRESSION = PAGE)
GO

CREATE TABLE [dbo].[TextEngineResponseList](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Created] [DATETIME2](7) NOT NULL DEFAULT (SYSDATETIME()),
	[DistinctUniqueWordCount] [INT] NOT NULL,
	[DistinctUniqueWords] [NVARCHAR](MAX) NULL,
    [WatchListWordCount] [INT] NOT NULL,
	[WatchListWords] [NVARCHAR](MAX) NULL,
    CONSTRAINT [PK_TextEngineResponses] PRIMARY KEY CLUSTERED ( [Id] ASC )
) WITH (DATA_COMPRESSION = PAGE)
GO

INSERT INTO [dbo].[WatchList] ([Word])
VALUES ('horse'), ('zebra'), ('dog'), ('elephant'), ('titanic'), ('iceberg'), ('lifeboat')