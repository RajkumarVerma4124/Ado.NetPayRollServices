USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Rerive ER Employee Record Using Income(UC7-UC9)--------
-- ====================================================================
Alter PROCEDURE [dbo].[spAggregateErFunction] 
	@Gender char(1)
AS
	SELECT SUM(pay.BasicPay),CAST(AVG(pay.BasicPay) as decimal(10,2)),MIN(pay.BasicPay),MAX(pay.BasicPay),emp.Gender,COUNT(*) 
	From Employee As emp INNER JOIN Payroll As pay On pay.EmployeeId = emp.EmployeeId where emp.Gender = @Gender Group By emp.Gender;
