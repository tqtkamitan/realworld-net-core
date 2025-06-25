USE [QuangTanWeb]
GO

/****** Object:  Table [dbo].[BaiViet]    Script Date: 8/21/2022 11:13:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Post](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PostName] [nvarchar](500) NOT NULL,
	[Alias] [varchar](500) NOT NULL,
	[Title] [nvarchar](1500) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ShortContent] [nvarchar](max) NOT NULL,
	[PostImage] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[FView] [int] NULL,
	[View] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[HomePageOrder] [int] NULL,
	[Order] [int] NULL,
	[IsDeleted] [bit] NULL,
	[PostTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_PostType] FOREIGN KEY([PostTypeId])
REFERENCES [dbo].[PostType] ([Id])
GO

ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_PostType]
GO


