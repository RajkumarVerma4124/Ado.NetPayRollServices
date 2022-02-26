USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Rerive ER Employee Record Using Name(UC7-UC10)--------
-- ====================================================================
CREATE PROCEDURE [dbo].[spGetEREmpUsingName] 
	@EmployeeName varchar(50)
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender
	FROM Company AS comp
	INNER JOIN Employee AS emp ON comp.CompanyId = emp.CompanyId AND emp.EmployeeName=@EmployeeName;