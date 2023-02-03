using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

var queueName = args.Length == 1 ? args[0] : "customers";

var cts = new CancellationTokenSource();

var sqsClient = new AmazonSQSClient(new BasicAWSCredentials("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY"));
var queueUrlResponse = await sqsClient.GetQueueUrlAsync(queueName);

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = new List<string> { "All" },
    MessageAttributeNames = new List<string> { "All" },
};

while (!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

    foreach (var message in response.Messages)
    {
        Console.WriteLine($@"Message Id: {message.MessageId}");
        Console.WriteLine($@"Message Body: {message.Body}");

        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle); // deletes the message from the Q
    }

    await Task.Delay(3000);
}