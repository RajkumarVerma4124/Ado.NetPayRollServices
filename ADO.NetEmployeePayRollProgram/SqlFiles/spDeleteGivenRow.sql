USE [PayRoll_Service]
GO
-- ====================================================================
------Created Stored Procedure For Deleting Data(UC4)-----------
-- ====================================================================
CREATE PROCEDURE [dbo].[spDeleteGivenRow] 
	-- Add the parameters for the stored procedure here
	@Id int,
	@Name varchar(50)
AS
	Delete FROM Employee_Payroll WHERE Id = @Id And Name = @Name;

