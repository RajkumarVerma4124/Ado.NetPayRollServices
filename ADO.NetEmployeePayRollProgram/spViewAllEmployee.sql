USE [PayRoll_Service]
GO
/****** Object:  StoredProcedure [dbo].[ViewAllEmployee]    Script Date: 2/26/2022 10:02:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ViewAllEmployee] 
As
	SELECT * From Employee_Payroll
