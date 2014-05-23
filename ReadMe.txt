1. The HRPaidTimeOff.sql is to create the HRPaidTimeOff database tables, stored procedures, and load it with dummy data.
But there is also HRPaidTimeOff - the ready database file to be attached to your SQLServer 2008-2012 default instance. For that
- open Web.config file of AgencyTimeManagementGUI application and correct connectionString="data source=.\YOUR_DEFAULDT_SQLServer ... 
with name of your SQLServer instance (for examle: localhost, .\SQLExpress, etc)

2. use AgencyAdminWinUI to add your current windows account to DB (click button "Include the Windows User to DB"). 
In this case, int the ENTUserAccount table (used for the Role Based Security) will be inserted your current WindowsAccount (YOURDOMAIN\YourLogin) 
with administrator role of the application.

3.AgencyTimeManagementGUI - is a main webForms project (for your agency people & time management).
Befor start it, check that
- Default.aspx is startup page
- HRPaidTimeOff database file is NOT attached to yout database SQL Server (it will be attached automatically). 
So you should DETACH HRPaidTimeOff database before start the application is started