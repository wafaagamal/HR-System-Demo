use [HR_Company]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetEmployees]
 @page INT,
 @size INT,
 @mangerName varchar(50)=null
 AS
BEGIN
    IF(@page=0)
     IF(@page <=0)
        BEGIN
        SET @page = 1;
        END
        IF(@size<=0)
        BEGIN
        SET @size = 2147483647;
        END
    DECLARE @SkipRows int = (@page - 1) * @size;
    SET NOCOUNT ON
SELECT
		   e.Name ,e.Email ,e2.Name manger,e.Id
		   FROM Employees e 
		 inner  join Employees e2 on e.MangerId=e2.Id
         inner  join Attendences att on e.Id=att.EmployeeId
		 where (@mangerName is null or  e2.Name = @mangerName )
    GROUP BY   e.Name ,e.Email ,e2.Name
    ORDER BY e2.Name 

	OFFSET @SkipRows ROWS
	FETCH NEXT @size ROWS ONLY
End