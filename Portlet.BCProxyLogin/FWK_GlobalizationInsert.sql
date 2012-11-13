
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;

/*
	New for 2.0
*/
DELETE FROM FWK_Globalization WHERE Text_Key in ('CUS_BC_PL_CAN_PROXY_STUDENT','CUS_BC_PL_CAN_PROXY_STUDENT_MSG','CUS_BC_PL_CAN_PROXY_STAFF','CUS_BC_PL_CAN_PROXY_STAFF_MSG','CUS_BC_PL_CAN_PROXY_FACULTY','CUS_BC_PL_CAN_PROXY_FACULTY_MSG','CUS_BC_PL_CAN_PROXY_CANDIDATE','CUS_BC_PL_CAN_PROXY_CANDIDATE_MSG', 'CUS_BC_PL_CAN_PROXY_CONSTITUENT','CUS_BC_PL_CAN_PROXY_CONSTITUENT_MSG' );

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_DAILY' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_DAILY', 'En','Daily',null);

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ALLOW' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ALLOW', 'En','Allow',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_DENY' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_DENY', 'En','Deny',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ENABLE_PW' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ENABLE_PW', 'En','Require the user to enter their password again before proxying?',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_LOG_IP' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_LOG_IP', 'En','Log IP address of proxy attempts?',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_LOG_FAILURES' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_LOG_FAILURES', 'En','Log failed attempts?',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ALLOWRELOGIN' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ALLOWRELOGIN', 'En','Enable "re-login" icon in the logs as a shortcut for logging in as a previously proxied user?',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ENABLE_REPORTS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ENABLE_REPORTS', 'En','Enable periodic reports of proxy activity?',null);
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REPORT_FREQUENCY' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REPORT_FREQUENCY', 'En','How often should the reports be sent out?',null);
    
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REPORT_ROLES' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REPORT_ROLES', 'En','Who should this report be sent to?',null);    
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_PERMISSIONS_RANK' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_PERMISSIONS_RANK', 'En','When calculating access permissions, which takes precedence?',null);   
        
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_USAGE_REPORTS_HEADER' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_USAGE_REPORTS_HEADER', 'En','Usage Reports',null);   

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_GENERAL_SETUP_HEADER' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_GENERAL_SETUP_HEADER', 'En','General Setup',null);   

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REPORT_EMAIL_SUBJECT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REPORT_EMAIL_SUBJECT', 'En','E-Mail Subject:',null);   

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REPORT_EMAIL_BODY' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REPORT_EMAIL_BODY', 'En','Extra Message in E-mail Body:',null);   

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_ENABLE_REPORTS')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_ENABLE_REPORTS','false','false');     

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_REPORT_FREQUENCY')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_REPORT_FREQUENCY','N','N');     

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_REPORT_ROLES')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_REPORT_ROLES','','');

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_REPORT_EMAIL_SUBJECT')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_REPORT_EMAIL_SUBJECT','{Type} Proxy Login Report','');

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_REPORT_EMAIL_BODY')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_REPORT_EMAIL_BODY','The following is the report for the {Type} proxy login usage.','');


/* 1.13 */


IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL', 'En','Can See SideBar Control',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG', 'En','Allows User to See the Proxy Login Tools Sidebar Control to Login',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_SITE_ADMIN_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_SITE_ADMIN_PERMS', 'En','User is a site admin, cannot proxy site admins',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ERROR_USER' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ERROR_USER', 'En','Error logging action, contact your site administrator',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ERROR_ADMIN' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ERROR_ADMIN', 'En','Error logging action:',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_USERNAME_LABEL_TEXT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_USERNAME_LABEL_TEXT', 'En','User to Login As:',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REASON_LABEL_TEXT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REASON_LABEL_TEXT', 'En','Why are you logging in as this user:',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_USER_NOT_FOUND' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_USER_NOT_FOUND', 'En','User not found in System',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_REQUIRED_REASON' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_REQUIRED_REASON', 'En','You must supply a reason for the proxy.',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_INVALID_PW' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_INVALID_PW', 'En','Password Incorrect.',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_PASSWORD_LABEL_TEXT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_PASSWORD_LABEL_TEXT', 'En','Your Password:',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_DENY_ROLE' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_DENY_ROLE', 'En','Deny Proxy Access',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_DENY_ROLE_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_DENY_ROLE_MSG', 'En','Prevents any proxy login user from accessing this role.',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_DENIED_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_DENIED_PERMS', 'En','This user is a member of a role that has been restricted from proxy login attemps.',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_ALREADY_PROXIED' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_ALREADY_PROXIED', 'En','You may not proxy another user while proxied yourself.',null);




IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_ENABLE_PW')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_ENABLE_PW','false','false');

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_LOG_IP')            
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_LOG_IP','true','true');

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_LOG_FAILURES')            
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_LOG_FAILURES','true','true');     

IF NOT EXISTS(SELECT [Key] FROM FWK_ConfigSettings WHERE [Category]= 'C_PortletSettings' AND [Key] = 'CUS_BC_PL_ALLOWRELOGIN')
INSERT INTO [dbo].[FWK_ConfigSettings]([Category],[Key],[Value],[DefaultValue])VALUES('C_PortletSettings','CUS_BC_PL_ALLOWRELOGIN','true','true');     


GO



COMMIT;
RAISERROR (N'[dbo].[FWK_Globalization]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

