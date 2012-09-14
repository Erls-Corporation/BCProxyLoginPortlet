USE [ICS_NET]
GO

/****** Object:  Table [dbo].[CUS_BCPortletLogs]    Script Date: 06/06/2011 14:17:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[CUS_BCPortletLogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[CUS_BCPortletLogs](
	[LogNumber] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PortletID] [uniqueidentifier] NOT NULL,
	[SourceUser] [uniqueidentifier] NOT NULL,
	[TargetUser] [uniqueidentifier] NULL,
	[Action] [nvarchar](255) NOT NULL,
	[Time] [datetime] NOT NULL
) ON [PRIMARY]

IF NOT EXISTS (SELECT * FROM sys.objects where type_desc like '%CONSTRAINT' and parent_object_id = OBJECT_ID('[dbo].[CUS_BCPortletLogs]') and name = 'DF_CUS_BCPortletLogs_LogNumber')
ALTER TABLE [dbo].[CUS_BCPortletLogs] ADD  CONSTRAINT [DF_CUS_BCPortletLogs_LogNumber]  DEFAULT (newid()) FOR [LogNumber]
GO



