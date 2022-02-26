USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[spAddEmployee]    Script Date: 2/26/2022 11:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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

