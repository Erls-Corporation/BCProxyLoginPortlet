USE [ICS_NET];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;

INSERT INTO [dbo].[FWK_Globalization]([Text_Key], [Language_Code], [Text_Value], [Text_Custom_Value])
SELECT N'CUS_BC_PL_REQUIRED_REASON', N'En', N'You must supply a reason for the proxy.', NULL UNION ALL
SELECT N'CUS_BC_PL_INVALID_PW', N'En', N'Password Incorrect.', NULL UNION ALL
SELECT N'CUS_BC_PL_PASSWORD_LABEL_TEXT', N'En', N'Your Password:', NULL UNION ALL
SELECT N'CUS_BC_PL_DENY_ROLE', N'En', N'Deny Proxy Access', NULL UNION ALL
SELECT N'CUS_BC_PL_DENY_ROLE_MSG', N'En', N'Prevents any proxy login user from accessing this role.', NULL UNION ALL
SELECT N'CUS_BC_PL_DENIED_PERMS', N'En', N'This user is a member of a role that has been restricted from proxy login attemps.', NULL

INSERT INTO [dbo].[FWK_ConfigSettings]
           ([Category]
           ,[Key]
           ,[Value]
           ,[DefaultValue])
     VALUES
           ('C_PortletSettings',
			'CUS_BC_PL_ENABLE_PW',
            'false',
            'false');
            
INSERT INTO [dbo].[FWK_ConfigSettings]
           ([Category]
           ,[Key]
           ,[Value]
           ,[DefaultValue])
     VALUES
           ('C_PortletSettings',
			'CUS_BC_PL_LOG_IP',
            'true',
            'true');
            
INSERT INTO [dbo].[FWK_ConfigSettings]
           ([Category]
           ,[Key]
           ,[Value]
           ,[DefaultValue])
     VALUES
           ('C_PortletSettings',
			'CUS_BC_PL_LOG_FAILURES',
            'true',
            'true');           
GO


COMMIT;
RAISERROR (N'[dbo].[FWK_Globalization]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO


