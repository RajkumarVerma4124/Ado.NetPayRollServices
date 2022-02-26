USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Aggregate Functions(UC6)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spForAggregateFunctions] 
	@Gender char(1)
AS
	Delete From Employee_Payroll