using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;
using System.Text.Json;

var sqsClient = new AmazonSQSClient(new BasicAWSCredentials("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY"));

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "test@test.com",
    FullName = "Test Name",
    DateOfBirth = new DateTime(1990, 1, 1),
    GitHubUsername = "testname"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

await sqsClient.SendMessageAsync(sendMessageRequest);
