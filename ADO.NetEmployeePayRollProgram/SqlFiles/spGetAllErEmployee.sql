USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Retrieve Data Using Date Range(UC5)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spGetAllErEmployee] 
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.EmployeeAddress,
	pay.BasicPay,pay.TaxablePay,pay.IncomeTax,pay.NetPay,pay.Deductions,dep.DepartmentId,dep.DepartmentName
	FROM Company AS comp
	INNER JOIN Employee AS emp ON comp.CompanyId=emp.CompanyId
	INNER JOIN Payroll AS pay ON pay.EmployeeId = emp.EmployeeId	
	INNER JOIN EmployeeDepartment as empDep ON empDep.EmployeeId = emp.EmployeeId
	INNER JOIN Department as dep ON dep.DepartmentId = empDep.DepartmentId;
