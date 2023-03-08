USE [Selena]
GO

-- If exists, drop Stored Procedures and Triggers
IF (EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='csp_NonQualifiedPrescription_SenderQueue_Activator'))
	DROP PROCEDURE csp_NonQualifiedPrescription_SenderQueue_Activator
GO
IF (EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='qsp_NonQualifiedPrescription_End'))
	DROP PROCEDURE qsp_NonQualifiedPrescription_End
GO
IF (EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='qsp_NonQualifiedPrescription_Receive'))
	DROP PROCEDURE qsp_NonQualifiedPrescription_Receive
GO
IF (EXISTS(SELECT * FROM sys.objects WHERE type='TR' AND name='NonQualifiedPrescription_ins'))
	DROP TRIGGER NonQualifiedPrescription_ins
GO
IF (EXISTS(SELECT * FROM sys.objects WHERE type='TR' AND name='NonQualifiedPrescription_upd'))
	DROP TRIGGER NonQualifiedPrescription_upd
GO

CREATE PROCEDURE csp_NonQualifiedPrescription_SenderQueue_Activator
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @messageType SYSNAME
	DECLARE @conversationHandle UNIQUEIDENTIFIER;

	RECEIVE TOP (1)
		@messageType=message_type_name,
		@conversationHandle=[conversation_handle]
	FROM
		NonQualifiedPrescription_SenderQueue

	IF @messageType=(N'http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog') OR @messageType=(N'http://schemas.microsoft.com/SQL/ServiceBroker/Error')
	BEGIN
		END CONVERSATION @conversationHandle
	END
END
GO
GRANT EXECUTE ON csp_NonQualifiedPrescription_SenderQueue_Activator TO [public]
GO

CREATE PROCEDURE [dbo].[qsp_NonQualifiedPrescription_End]
	@pConverstionHandle uniqueidentifier
AS
	END CONVERSATION @pConverstionHandle
GO
GRANT EXECUTE ON qsp_NonQualifiedPrescription_End TO [public]
GO

CREATE PROCEDURE [dbo].[qsp_NonQualifiedPrescription_Receive]
AS
	WAITFOR (RECEIVE TOP(1) [conversation_handle], [message_body], [message_type_name], [queuing_order] FROM [dbo].[NonQualifiedPrescription_ReceiverQueue]), TIMEOUT 10000
GO
GRANT EXECUTE ON qsp_NonQualifiedPrescription_Receive TO [public]
GO

CREATE TRIGGER NonQualifiedPrescription_ins ON NonQualifiedPrescription AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON

	-- Read inserted rows into concatenated message strings in a numbered table
	DECLARE @messages TABLE ([RowNumber] INT IDENTITY(1,1), [Message] NVARCHAR(MAX))

	INSERT INTO @messages
		SELECT
			'NonQualifiedPrescriptionUID:' + CAST(i.NonQualifiedPrescriptionUID AS nvarchar) + '|' +
			'RxPrescriberNPI:' + i.RxPrescriberNPI
		FROM
			inserted i

	-- Loop over each row in message table and send a message for each
	DECLARE @rowsToProcess INT
	SELECT @rowsToProcess=COUNT(1) FROM @messages

	DECLARE @message NVARCHAR(MAX)

	WHILE @rowsToProcess > 0
	BEGIN
		DECLARE @conversationHandle UNIQUEIDENTIFIER
		SELECT @message=[message] FROM @messages WHERE [RowNumber]=@rowsToProcess

		BEGIN DIALOG @conversationHandle
		FROM SERVICE NonQualifiedPrescription_SenderService
		TO SERVICE 'NonQualifiedPrescription_ReceiverService'
		ON CONTRACT NonQualifiedPrescription_MessageContract
		WITH ENCRYPTION=OFF;

		SEND ON CONVERSATION @conversationHandle
		MESSAGE TYPE NonQualifiedPrescription_RequestMessage(@message)

		SET @rowsToProcess=@rowsToProcess - 1
	END
END
GO

CREATE TRIGGER NonQualifiedPrescription_upd ON NonQualifiedPrescription AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	-- Read inserted rows into concatenated message strings in a numbered table
	DECLARE @messages TABLE ([RowNumber] INT IDENTITY(1,1), [Message] NVARCHAR(MAX))

	INSERT INTO @messages
		SELECT
			'NonQualifiedPrescriptionUID:' + CAST(i.NonQualifiedPrescriptionUID AS nvarchar) + '|' +
			'RxPrescriberNPI:' + i.RxPrescriberNPI
		FROM
			inserted i
			INNER JOIN deleted d ON d.NonQualifiedPrescriptionUID=i.NonQualifiedPrescriptionUID
		WHERE
			d.[Status]='Errored'
			AND i.[Status]='Ready'
			
	-- Loop over each row in message table and send a message for each
	DECLARE @rowsToProcess INT
	SELECT @rowsToProcess=COUNT(1) FROM @messages

	DECLARE @message NVARCHAR(MAX)

	WHILE @rowsToProcess > 0
	BEGIN
		DECLARE @conversationHandle UNIQUEIDENTIFIER
		SELECT @message=[message] FROM @messages WHERE [RowNumber]=@rowsToProcess

		BEGIN DIALOG @conversationHandle
		FROM SERVICE NonQualifiedPrescription_SenderService
		TO SERVICE 'NonQualifiedPrescription_ReceiverService'
		ON CONTRACT NonQualifiedPrescription_MessageContract
		WITH ENCRYPTION=OFF;

		SEND ON CONVERSATION @conversationHandle
		MESSAGE TYPE NonQualifiedPrescription_RequestMessage(@message)

		SET @rowsToProcess=@rowsToProcess - 1
	END
END
GO

-- If exists, drop Service Broker objects
IF (EXISTS(SELECT * FROM sys.services WHERE name='NonQualifiedPrescription_ReceiverService'))
	DROP SERVICE NonQualifiedPrescription_ReceiverService
GO
IF (EXISTS(SELECT * FROM sys.services WHERE name='NonQualifiedPrescription_SenderService'))
	DROP SERVICE NonQualifiedPrescription_SenderService
GO
IF (EXISTS(SELECT * FROM sys.service_queues WHERE name='NonQualifiedPrescription_ReceiverQueue'))
	DROP QUEUE NonQualifiedPrescription_ReceiverQueue
GO
IF (EXISTS(SELECT * FROM sys.service_queues WHERE name='NonQualifiedPrescription_SenderQueue'))
	DROP QUEUE NonQualifiedPrescription_SenderQueue
GO
IF (EXISTS(SELECT * FROM sys.service_contracts WHERE name='NonQualifiedPrescription_MessageContract'))
	DROP CONTRACT NonQualifiedPrescription_MessageContract
GO
IF (EXISTS(SELECT * FROM sys.service_message_types WHERE name='NonQualifiedPrescription_RequestMessage'))
	DROP MESSAGE TYPE NonQualifiedPrescription_RequestMessage
GO
IF (EXISTS(SELECT * FROM sys.service_message_types WHERE name='NonQualifiedPrescription_ResponseMessage'))
	DROP MESSAGE TYPE NonQualifiedPrescription_ResponseMessage
GO

-- Create Service Broker objects
CREATE MESSAGE TYPE NonQualifiedPrescription_RequestMessage VALIDATION=NONE
CREATE MESSAGE TYPE NonQualifiedPrescription_ResponseMessage VALIDATION=NONE
CREATE CONTRACT NonQualifiedPrescription_MessageContract(NonQualifiedPrescription_RequestMessage SENT BY INITIATOR, NonQualifiedPrescription_ResponseMessage SENT BY INITIATOR)
CREATE QUEUE NonQualifiedPrescription_ReceiverQueue
CREATE QUEUE NonQualifiedPrescription_SenderQueue
CREATE SERVICE NonQualifiedPrescription_ReceiverService ON QUEUE NonQualifiedPrescription_ReceiverQueue(NonQualifiedPrescription_MessageContract)
CREATE SERVICE NonQualifiedPrescription_SenderService ON QUEUE NonQualifiedPrescription_SenderQueue
GO

ALTER QUEUE NonQualifiedPrescription_SenderQueue WITH ACTIVATION( PROCEDURE_NAME=dbo.csp_NonQualifiedPrescription_SenderQueue_Activator, MAX_QUEUE_READERS=1, EXECUTE AS OWNER, STATUS=ON);
GO

/*
SELECT is_broker_enabled FROM sys.databases WHERE name='Selena'
SELECT user_access_desc FROM sys.databases WHERE name='Selena'

USE [MASTER]
GO

ALTER DATABASE Selena SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
ALTER DATABASE Selena SET ENABLE_BROKER
GO
ALTER DATABASE Selena SET MULTI_USER
GO
*/
