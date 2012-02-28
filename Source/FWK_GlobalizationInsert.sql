USE [ICS_NET];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;
DELETE FROM [dbo].FWK_Globalization WHERE Text_Key like 'CUS_BC_PL%';

INSERT INTO [dbo].[FWK_Globalization]([Text_Key], [Language_Code], [Text_Value], [Text_Custom_Value])
SELECT N'CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL', N'En', N'Can See SideBar Control', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG', N'En', N'Allows User to See the Proxy Login Tools Sidebar Control to Login', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_STUDENT', N'En', N'Can Proxy Student', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_STUDENT_MSG', N'En', N'Allows User to Proxy Users with the Student Role', NULL UNION ALL
SELECT N'CUS_BC_PL_STUDENT_PERMS', N'En', N'User has the Student Role, but you are not authorized to proxy students', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_STAFF', N'En', N'Can Proxy Staff', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_STAFF_MSG', N'En', N'Allows User to Proxy Users with the Staff Role', NULL UNION ALL
SELECT N'CUS_BC_PL_STAFF_PERMS', N'En', N'User has the Staff Role, but you are not authorized to proxy staff', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_FACULTY', N'En', N'Can Proxy Faculty', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_FACULTY_MSG', N'En', N'Allows User to Proxy Users with the Faculty Role', NULL UNION ALL
SELECT N'CUS_BC_PL_FACULTY_PERMS', N'En', N'User has the Faculty Role, but you are not authorized to proxy faculty', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_CANDIDATE', N'En', N'Can Proxy Candidate', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_CANDIDATE_MSG', N'En', N'Allows User to Proxy Users with the Candidate Role', NULL UNION ALL
SELECT N'CUS_BC_PL_CANDIDATE_PERMS', N'En', N'User has the Candidate Role, but you are not authorized to proxy candidates', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_CONSTITUENT', N'En', N'Can Proxy Constituent', NULL UNION ALL
SELECT N'CUS_BC_PL_CAN_PROXY_CONSTITUENT_MSG', N'En', N'Allows User to Proxy Users with the Constituent Role', NULL  UNION ALL
SELECT N'CUS_BC_PL_CONSTITUENT_PERMS', N'En', N'User has the Constituent Role, but you are not authorized to proxy constituents', NULL UNION ALL
SELECT N'CUS_BC_PL_SITE_ADMIN_PERMS', N'En', N'User is a site admin, cannot proxy site admins', NULL UNION ALL
SELECT N'CUS_BC_PL_ERROR_USER', N'En', N'Error logging action, contact your site administrator', NULL UNION ALL
SELECT N'CUS_BC_PL_ERROR_ADMIN', N'En', N'Error logging action:', NULL UNION ALL
SELECT N'CUS_BC_PL_USERNAME_LABEL_TEXT', N'En', N'User to Login As:', NULL UNION ALL
SELECT N'CUS_BC_PL_REASON_LABEL_TEXT', N'En', N'Why are you logging in as this user:', NULL UNION ALL
SELECT N'CUS_BC_PL_USER_NOT_FOUND', N'En', N'User not found in System', NULL UNION ALL
SELECT N'CUS_BC_PL_REQUIRED_REASON', N'En', N'You must supply a reason for the proxy.', NULL UNION ALL
SELECT N'CUS_BC_PL_INVALID_PW', N'En', N'Password Incorrect.', NULL UNION ALL
SELECT N'CUS_BC_PL_PASSWORD_LABEL_TEXT', N'En', N'Your Password:', NULL UNION ALL
SELECT N'CUS_BC_PL_DENY_ROLE', N'En', N'Deny Proxy Access', NULL UNION ALL
SELECT N'CUS_BC_PL_DENY_ROLE_MSG', N'En', N'Prevents any proxy login user from accessing this role.', NULL UNION ALL
SELECT N'CUS_BC_PL_DENIED_PERMS', N'En', N'This user is a member of a role that has been restricted from proxy login attemps.', NULL;

DELETE FROM [dbo].[FWK_ConfigSettings]
      WHERE [Key] like 'CUS_BC_PL%';

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

