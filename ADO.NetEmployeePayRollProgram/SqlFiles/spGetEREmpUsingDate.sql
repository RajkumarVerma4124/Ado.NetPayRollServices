USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[spGetEREmpUsingDate]    Script Date: 2/28/2022 1:44:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetEREmpUsingDate] 
	@StartDate date
AS
	SELECT comp.CompanyID,comp.CompanyName,emp.EmployeeId,emp.EmployeeName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.IsActive
	FROM Company AS comp 
	INNER JOIN Employee AS emp 
	ON comp.CompanyId = emp.CompanyId Where emp.StartDate Between Cast(@StartDate as Date) AND getdate();