/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
Adding master table entries post-deployment 
This SQL query is only useful for initial execution fo database migration
*/


/*ApplicationRole*/
IF NOT EXISTS (SELECT 1 FROM dbo.ApplicationRole)
BEGIN
    INSERT INTO dbo.ApplicationRole VALUES (1,'Administrator')
    INSERT INTO dbo.ApplicationRole VALUES (2,'Manager')
    INSERT INTO dbo.ApplicationRole VALUES (3,'Employee')
END



/*ApplicationUser*/
IF NOT EXISTS (SELECT 1 FROM dbo.ApplicationUser)
BEGIN
    INSERT INTO dbo.ApplicationUser VALUES ('admin','a',1,1,1,GETDATE(),1,GETDATE(),0)
END



/*Department*/
IF NOT EXISTS (SELECT 1 FROM dbo.Department)
BEGIN
    INSERT INTO dbo.Department VALUES (1,'Management')
    INSERT INTO dbo.Department VALUES (2,'Software Development')
    INSERT INTO dbo.Department VALUES (3,'Accounts')
    INSERT INTO dbo.Department VALUES (4,'HR')
END
