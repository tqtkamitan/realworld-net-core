USE [QuangTanWeb]
GO

/****** Object:  Table [dbo].[BinhLuan]    Script Date: 8/21/2022 10:48:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommentData] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[MemberId] [bigint] NULL,
	[PostId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


