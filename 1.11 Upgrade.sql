USE [ICS_NET];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;

INSERT INTO [dbo].[FWK_ConfigSettings]
           ([Category]
           ,[Key]
           ,[Value]
           ,[DefaultValue])
     VALUES
           ('C_PortletSettings',
			'CUS_BC_PL_ALLOWRELOGIN',
            'false',
            'false');         
GO


COMMIT;
RAISERROR (N'[dbo].[FWK_Globalization]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO


