begin tran 
use ICS_NET; 
Declare @ApplicationName varchar(255) = 'CUS BC Proxy Login Portlet'; 
Declare @VersionNumber varchar(255) = '1.12'; 
Declare @VersionDate DateTime = '2012-06-18 09:50:00';
if exists (select * from [FWK_VersionInfo] where [ApplicationName] = @ApplicationName) 
begin 
   update [FWK_VersionInfo] set VersionNumber = @VersionNumber, VersionDate = @VersionDate 
   where [ApplicationName] = @ApplicationName 
end 
else 
begin 
   insert [FWK_VersionInfo]  (ApplicationName, VersionNumber, VersionDate) 
   values (@ApplicationName,@VersionNumber,@VersionDate ) 
end 
commit tran 
