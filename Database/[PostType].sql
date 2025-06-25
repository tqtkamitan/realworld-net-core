USE [QuangTanWeb]
GO

/****** Object:  Table [dbo].[DanhMucBaiViet]    Script Date: 8/21/2022 10:44:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PostType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PostTypeName] [nvarchar](500) NOT NULL,
	[Alias] [varchar](500) NOT NULL,
	[Logo] [nvarchar](max) NULL,
	[Order] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PostType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


