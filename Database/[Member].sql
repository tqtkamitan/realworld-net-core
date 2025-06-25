USE [QuangTanWeb]
GO

/****** Object:  Table [dbo].[ThanhVien]    Script Date: 8/21/2022 10:43:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Member](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](250) NULL,
	[Email] [nchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](250) NULL,
	[Password] [nvarchar](500) NULL,
	[IsDeleted] [bit] NULL,
	[AccountTypeId] [int] NULL,
	[AccountType] [nvarchar](250) NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


