USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[spInsertDataIntoPayroll]    Script Date: 2/28/2022 1:17:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spInsertDataIntoPayroll] 
	@EmployeeId INT,
	@BasicPay FLOAT,
	@TaxablePay FLOAT,
	@IncomeTax FLOAT,
	@NetPay FLOAT,
	@Deductions FLOAT
AS
	INSERT INTO Payroll VALUES(@EmployeeId,@BasicPay,@TaxablePay,@IncomeTax,@NetPay,@Deductions);