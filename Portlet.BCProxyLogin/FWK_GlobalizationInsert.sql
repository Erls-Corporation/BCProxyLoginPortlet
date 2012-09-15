
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO


BEGIN TRANSACTION;
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL', 'En','Can See SideBar Control',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG', 'En','Allows User to See the Proxy Login Tools Sidebar Control to Login',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_STUDENT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_STUDENT', 'En','Can Proxy Student',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_STUDENT_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_STUDENT_MSG', 'En','Allows User to Proxy Users with the Student Role',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_STUDENT_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_STUDENT_PERMS', 'En','User has the Student Role, but you are not authorized to proxy students',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_STAFF' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_STAFF', 'En','Can Proxy Staff',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_STAFF_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_STAFF_MSG', 'En','Allows User to Proxy Users with the Staff Role',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_STAFF_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_STAFF_PERMS', 'En','User has the Staff Role, but you are not authorized to proxy staff',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_FACULTY' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_FACULTY', 'En','Can Proxy Faculty',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_FACULTY_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_FACULTY_MSG', 'En','Allows User to Proxy Users with the Faculty Role',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_FACULTY_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_FACULTY_PERMS', 'En','User has the Faculty Role, but you are not authorized to proxy faculty',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_CANDIDATE' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_CANDIDATE', 'En','Can Proxy Candidate',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_CANDIDATE_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_CANDIDATE_MSG', 'En','Allows User to Proxy Users with the Candidate Role',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CANDIDATE_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CANDIDATE_PERMS', 'En','User has the Candidate Role, but you are not authorized to proxy candidates',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_CONSTITUENT' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_CONSTITUENT', 'En','Can Proxy Constituent',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CAN_PROXY_CONSTITUENT_MSG' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CAN_PROXY_CONSTITUENT_MSG', 'En','Allows User to Proxy Users with the Constituent Role',null);
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_BC_PL_CONSTITUENT_PERMS' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
        VALUES ('CUS_BC_PL_CONSTITUENT_PERMS', 'En','User has the Constituent Role, but you are not authorized to proxy constituents',null);
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

