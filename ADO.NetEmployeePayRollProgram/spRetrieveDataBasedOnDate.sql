USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Retrieve Data Using Date Range(UC5)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spRetrieveDataBasedOnDate] 
	-- Add the parameters for the stored procedure here
	@StartDate date
AS
	SELECT * FROM Employee_Payroll WHERE StartDate between CAST(@StartDate as Date) and getdate();
