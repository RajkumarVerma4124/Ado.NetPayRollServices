USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Aggregate Functions(UC6)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spForAggregateFunctions] 
	@Gender char(1)
AS
	SELECT SUM(BasicPay),MAX(BasicPay),MIN(BasicPay),CAST(AVG(BasicPay) as decimal(10,2)),Gender,Count(*) From Employee_Payroll Where Gender = @Gender Group By Gender;
