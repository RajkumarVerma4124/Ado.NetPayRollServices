USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure Viewing All Employee(UC2)-----------
-- ====================================================================
ALTER PROCEDURE [dbo].[ViewAllEmployee] 
As
	SELECT * From Employee_Payroll
