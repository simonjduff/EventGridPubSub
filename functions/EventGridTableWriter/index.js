module.exports = async function (context, eventGridEvent) {
    context.bindings.tableBinding = [];

    context.bindings.tableBinding.push({
        PartitionKey: eventGridEvent.id,
        RowKey: eventGridEvent.eventType,
        Payload: JSON.stringify(eventGridEvent)
    });
    
    context.done();
};