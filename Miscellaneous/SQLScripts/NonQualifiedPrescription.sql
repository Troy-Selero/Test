use [Selena]

-- Changing status to Errored, then back to Ready initiates a conversation
select *
-- update nqp set [Status]='Ready',StatusMachine=HOST_NAME(),StatusApp='Studio',StatusDate=GETUTCDATE(),StatusBy=SUSER_NAME()
from NonQualifiedPrescription nqp with (nolock)
--where
--	NonQualifiedPrescriptionUID=1

/*
insert into NonQualifiedPrescription
values
	('uabh','0131-2479','1225068620','Ready',HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())
	,('uabh','0131-2479','1598780777','Ready',HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME(),HOST_NAME(),'Studio',GETDATE(),SUSER_NAME())

truncate table NonQualifiedPrescription
*/