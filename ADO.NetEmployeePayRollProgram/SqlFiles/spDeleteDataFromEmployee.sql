USE [PayRoll_Service]
GO
ALTER PROCEDURE [dbo].[spInsertDataIntoEmployee] 
	@CompanyId INT,
	@EmployeeName VARCHAR(30),
	@PhoneNumber BIGINT,
	@EmployeeAddress VARCHAR(50),
	@StartDate DATE,
	@Gender CHAR(1),
	@id INT OUTPUT
AS
	INSERT INTO Employee (CompanyId,EmployeeName,PhoneNumber,EmployeeAddress,StartDate,Gender)VALUES(@CompanyId,@EmployeeName,@PhoneNumber,@EmployeeAddress,@StartDate,@Gender);
SET @id= SCOPE_IDENTITY()
	RETURN @id;