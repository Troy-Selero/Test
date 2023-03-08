use [Selero]

select *
from Entity with (nolock)

select *
from EntityPrescriber with (nolock)

/*
select * from EntityPrescriber
where PrescriberFirstName='louis'

insert into Entity values ('uabh','HRSA ID','University of Alabama Birmingham','UAB',HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())

insert into EntityPrescriber values (1,'1225068620','LOUIS','NABORS','Psychiatry & Neurology, Neurology',null,null,null,HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())

truncate table Entity
truncate table EntityPrescriber
*/