USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Retrieve Data Using Income Range(UC5)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spRetrieveDataBasedOnIncome] 
	-- Add the parameters for the stored procedure here
	@BasicPayStart float,
	@BasicPayEnd float
AS
	SELECT * FROM Employee_Payroll WHERE StartDate between @BasicPayStart and @BasicPayEnd;
