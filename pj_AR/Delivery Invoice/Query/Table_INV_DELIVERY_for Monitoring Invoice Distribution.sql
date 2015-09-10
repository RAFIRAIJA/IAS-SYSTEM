USE [PROD_ISS_NAV]
GO

/****** Object:  Table [dbo].[INV_DELIVERY]    Script Date: 03/28/2014 11:19:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INV_DELIVERY](
	[InvNo] [nvarchar](20) NOT NULL,
	[InvCustCode] [nvarchar](6) NULL,
	[InvCustName] [nvarchar](100) NULL,
	[InvDate] [datetime] NULL,
	[InvBranch] [nvarchar](10) NULL,
	[InvAmount] [decimal](18, 2) NULL,
	[InvSiteCard] [nvarchar](15) NULL,
	[InvProdOffer] [nvarchar](10) NULL,
	[InvOCM] [nvarchar](50) NULL,
	[InvFCM] [nvarchar](50) NULL,
	[InvCollector] [nvarchar](10) NULL,
	[InvCodeMess] [nvarchar](10) NULL,
	[InvMessName] [nvarchar](50) NULL,
	[InvStatus] [nvarchar](10) NULL,
	[InvDesc] [nvarchar](1024) NULL,
	[InvReceiptCode] [nvarchar](20) NULL,
	[InvResiTiki] [nvarchar](50) NULL,
	[InvProceedsDate] [datetime] NULL,
	[InvPreparedForDate] [datetime] NULL,
	[InvDeliveredDate] [datetime] NULL,
	[InvReturnedDate] [datetime] NULL,
	[InvDoneDate] [datetime] NULL,
	[InvCustPenerima] [nvarchar](50) NULL,
	[UserID] [nvarchar](10) NULL,
	[UserName] [nvarchar](50) NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[InvCreatedDate] [datetime] NULL,
	[InvCreatedBy] [nvarchar](50) NULL,
	[InvModifiedDate] [datetime] NULL,
	[InvModifiedBy] [nvarchar](50) NULL,
	[InvReceiptFlag] [nvarchar](1) NOT NULL,
	[Disabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_INV_DELIVERY] PRIMARY KEY CLUSTERED 
(
	[InvNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Proceeds, Delivered, Returned, Done' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'INV_DELIVERY', @level2type=N'COLUMN',@level2name=N'InvStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Rumusnya : yyMM-SequenceNo(5 digit)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'INV_DELIVERY', @level2type=N'COLUMN',@level2name=N'InvReceiptCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0/P = awal nya; 1/I = Invoice Receipt; 2/R = Receipt Data' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'INV_DELIVERY', @level2type=N'COLUMN',@level2name=N'InvReceiptFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = active; 1 = inactive' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'INV_DELIVERY', @level2type=N'COLUMN',@level2name=N'Disabled'
GO

ALTER TABLE [dbo].[INV_DELIVERY] ADD  CONSTRAINT [DF_INV_DELIVERY_flagInvReceipt]  DEFAULT ((0)) FOR [InvReceiptFlag]
GO

ALTER TABLE [dbo].[INV_DELIVERY] ADD  CONSTRAINT [DF_INV_DELIVERY_Disabled]  DEFAULT ((0)) FOR [Disabled]
GO


