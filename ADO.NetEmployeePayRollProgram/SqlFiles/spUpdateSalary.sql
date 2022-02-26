USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Updating Basic pay(UC4)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spUpdateSalary] 
	-- Add the parameters for the stored procedure here
	@Name varchar(50), 
	@BasicPay float,
	@Id int
AS
	Update Employee_Payroll Set BasicPay=@BasicPay Where Id = @Id And Name = @Name;
