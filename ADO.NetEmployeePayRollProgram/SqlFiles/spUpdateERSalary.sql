USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Updating ER Salary(UC7-UC10)--------
-- ====================================================================
CREATE PROCEDURE [dbo].[spUpdateERSalary] 
	@BasicPay float,
	@EmployeeId int,
	@EmployeeName varchar(50)
AS
	Update PayRoll Set BasicPay=@BasicPay From PayRoll As p INNER JOIN Employee As emp On emp.EmployeeId = p.EmployeeId Where emp.EmployeeId = @EmployeeId And emp.EmployeeName = @EmployeeName;
