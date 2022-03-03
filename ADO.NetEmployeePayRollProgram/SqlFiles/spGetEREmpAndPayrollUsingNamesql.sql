USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[spGetEREmpUsingName]    Script Date: 3/3/2022 10:35:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[spGetEREmpAndPayrollUsingName] 
	@EmployeeName varchar(50)
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.EmployeeAddress,emp.IsActive,
	pay.BasicPay,pay.TaxablePay,pay.IncomeTax,pay.NetPay,pay.Deductions
	FROM Company AS comp
	INNER JOIN Employee AS emp ON comp.CompanyId=emp.CompanyId AND emp.EmployeeName=@EmployeeName
	INNER JOIN Payroll AS pay ON pay.EmployeeId = emp.EmployeeId;	

