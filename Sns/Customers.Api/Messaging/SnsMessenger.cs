using Microsoft.Extensions.Options;
using System.Text.Json;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;

namespace Customers.Api.Messaging;

public class SnsMessenger : ISnsMessenger
{
    private const string MessageType = "MessageType";
    private const string DataType = "String";

    private readonly IAmazonSimpleNotificationService _sns;
    private readonly IOptions<TopicSettings> _topicSettings;
    private string? _topicArn;

    public SnsMessenger(IAmazonSimpleNotificationService sns, IOptions<TopicSettings> topicSettings)
    {
        _sns = sns;
        _topicSettings = topicSettings;
    }

    public async Task<PublishResponse> PublishMessageAsync<T>(T message)
    {
        var topicArn = await GetTopicArn();

        var sendMessageRequest = new PublishRequest
        {
            TopicArn = topicArn,
            Message = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    MessageType, new MessageAttributeValue
                    {
                        DataType = DataType,
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _sns.PublishAsync(sendMessageRequest);
    }

    private async ValueTask<string> GetTopicArn()
    {
        if (_topicArn is not null)
        {
            return _topicArn;
        }

        _topicArn = (await _sns.FindTopicAsync(_topicSettings.Value.Name)).TopicArn;

        return _topicArn;
    }
}

