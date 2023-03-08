use [Selero]

/*
-- Check if service broker is enabled
select is_broker_enabled from sys.databases where name='Selero'

-- Enable service broker
alter database Selero set enable_broker with rollback immediate

-- Disable service broker
alter database Selero set disable_broker
*/

-- CLOSED handles drop off after security_timestamp is passed
select [conversation_handle], conversation_id, CONVERT(varchar, DATEADD(HH, DATEDIFF(HOUR,GETUTCDATE(),GETDATE()), [lifetime]), 109) as 'life_time', CONVERT(varchar, DATEADD(HH, DATEDIFF(HOUR,GETUTCDATE(),GETDATE()), security_timestamp), 109) as 'security_timestamp',
	is_initiator, s.[name] as 'local service', far_service, sc.[name] 'contract', ce.[state], ce.state_desc
from sys.conversation_endpoints ce with (nolock)
	left join sys.services s with (nolock) on ce.service_id=s.service_id
	left join sys.service_contracts sc with (nolock) on ce.service_contract_id=sc.service_contract_id
--where security_timestamp < '2/3/2020'
order by security_timestamp

/*
Initially the state of both conversations (the sender and receiver) is "CONVERSING".
When the conversation is received and processed successfully, the conversation is ended using the conversation_handle.
The conversation defined as the initiator, or sender is removed.
The conversation defined as the receiver has its state changed to "CLOSED". It will be removed after the security timestamp has passed.

-- clear up conversation endpoints
declare @conversationHandle uniqueidentifier
--declare @stateDesc varchar(50)='CONVERSING'

select top 1 @conversationHandle=conversation_handle
from sys.conversation_endpoints
--where state_desc=@stateDesc

while @@rowcount=1
begin
	end conversation @conversationHandle with cleanup

	select top 1 @conversationHandle=conversation_handle
	from sys.conversation_endpoints
	--where state_desc=@stateDesc
end
*/