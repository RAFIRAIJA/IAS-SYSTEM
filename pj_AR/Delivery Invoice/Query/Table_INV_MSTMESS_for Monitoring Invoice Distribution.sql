USE [PROD_ISS_NAV]
GO

/****** Object:  Table [dbo].[INV_MSTMESS]    Script Date: 03/28/2014 11:19:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INV_MSTMESS](
	[InvCodeMess] [nvarchar](10) NOT NULL,
	[InvMessName] [nvarchar](50) NULL,
	[Disabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_INV_MSTMESS] PRIMARY KEY CLUSTERED 
(
	[InvCodeMess] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=active; 1=inactive' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'INV_MSTMESS', @level2type=N'COLUMN',@level2name=N'Disabled'
GO

ALTER TABLE [dbo].[INV_MSTMESS] ADD  CONSTRAINT [DF_INV_MSTMESS_Disabled]  DEFAULT ((0)) FOR [Disabled]
GO


