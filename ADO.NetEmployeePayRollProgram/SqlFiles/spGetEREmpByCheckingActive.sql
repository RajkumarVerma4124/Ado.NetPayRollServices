USE [PayRoll_Service]
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetEREmpByCheckingActive] 
	@IsActive varchar(10)
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.IsActive
	FROM Company AS comp
	INNER JOIN Employee AS emp ON comp.CompanyId = emp.CompanyId AND emp.IsActive=@IsActive;