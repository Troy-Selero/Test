use [Selena]

select * from Prescriber
--where FirstName='troy'

/*
insert into Prescriber values('12345','troy','edmonson',HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())
insert into Prescriber values('67890','Troy','Edmonson',HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())

declare @return_Value int
declare @pPrescriberUID bigint

exec @return_value=dbo.csp_Prescriber_Add @pPrescriberUID=@pPrescriberUID OUTPUT, @pNPI='1225068620', @pFirstName='LOUIS', @pLastName='NABORS'

select @pPrescriberUID as '@pPrescriberUID'
select 'Return Value'=@return_value

truncate table Prescriber
*/
