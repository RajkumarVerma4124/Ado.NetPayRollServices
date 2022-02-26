USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Rerive ER Employee Record Using Income(UC7-UC10)--------
-- ====================================================================
Alter PROCEDURE [dbo].[spGetEREmpUsingIncome] 
	-- Add the parameters for the stored procedure here
	@BasicPayStart float,
	@BasicPayEnd float
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,p.BasicPay
	FROM Company AS comp 
	INNER JOIN Employee AS emp ON comp.CompanyId = emp.CompanyId
	INNER JOIN Payroll As p ON emp.EmployeeId = p.EmployeeId Where p.BasicPay Between @BasicPayStart and @BasicPayEnd;