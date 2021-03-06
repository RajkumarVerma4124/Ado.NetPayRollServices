USE [PayRoll_Service]
GO
-- ====================================================================
------Add the stored procedure for adding data into db (UC3)-----------
-- ====================================================================
ALTER PROCEDURE [dbo].[spAddEmployee]
	@Name varchar(50),
	@BasicPay float,
	@StartDate date,
	@Address varchar(255),
	@Department varchar(50),
	@Gender char(1),
	@PhoneNum bigint,
	@Deductions float,
	@IncomeTax float,
	@TaxablePay float,
	@NetPay float
AS
	Insert Into Employee_Payroll Values(@Name, @BasicPay, @StartDate, @Gender, @Department, @PhoneNum, @Address, @Deductions, @TaxablePay, @IncomeTax, @NetPay) 

