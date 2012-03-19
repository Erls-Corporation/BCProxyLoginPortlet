USE [ICS_NET];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;

INSERT INTO [dbo].[FWK_Globalization]([Text_Key], [Language_Code], [Text_Value], [Text_Custom_Value])
SELECT N'CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL', N'En', N'Can See SideBar Control', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG', N'En', N'Allows User to See the Proxy Login Tools Sidebar Control to Login', NULL

COMMIT;
RAISERROR (N'[dbo].[FWK_Globalization]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO


