USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Retrieve Data Using Name(UC5)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spRetrieveDataBasedOnName] 
	-- Add the parameters for the stored procedure here
	@Name varchar(50)
AS
	Select * From Employee_Payroll Where Name = @Name;