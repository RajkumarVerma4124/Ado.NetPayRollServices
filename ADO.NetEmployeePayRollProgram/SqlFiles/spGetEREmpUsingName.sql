USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[spGetEREmpUsingName]    Script Date: 2/28/2022 1:45:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetEREmpUsingName] 
	@EmployeeName varchar(50)
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.IsActive
	FROM Company AS comp
	INNER JOIN Employee AS emp ON comp.CompanyId = emp.CompanyId AND emp.EmployeeName=@EmployeeName;