USE [PayRoll_Service]
GO
CREATE PROCEDURE [dbo].[spInsertDataIntoPayroll] 
	@EmployeeId INT,
	@BasicPay FLOAT,
	@TaxablePay FLOAT,
	@IncomeTax FLOAT,
	@NetPay FLOAT,
	@Deductions FLOAT
AS
	INSERT INTO Payroll VALUES(@EmployeeId,@BasicPay,@TaxablePay,@IncomeTax,@NetPay,@Deductions);
