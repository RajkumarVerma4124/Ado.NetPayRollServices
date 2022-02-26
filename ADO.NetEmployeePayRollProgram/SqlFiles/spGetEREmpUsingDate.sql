USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Rerive ER Employee Record Using Date(UC7-UC10)--------
-- ====================================================================
Alter PROCEDURE [dbo].[spGetEREmpUsingDate] 
	@StartDate date
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender
	FROM Company AS comp 
	INNER JOIN Employee AS emp 
	ON comp.CompanyId = emp.CompanyId Where emp.StartDate Between Cast(@StartDate as Date) AND getdate();
