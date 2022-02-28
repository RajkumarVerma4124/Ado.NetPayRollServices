USE [PayRoll_Service]
GO
Alter PROCEDURE [dbo].[spDeleteDataFromEmployee] 
	@EmployeeId INT,
	@EmployeeName VARCHAR(50),
AS
	DELETE FROM Employee WHERE EmployeeId = @EmployeeId AND EmployeeName = @EmployeeName;
