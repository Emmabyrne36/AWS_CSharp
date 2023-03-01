using System.Runtime.CompilerServices;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var secretId = "ApiKey";

var secretsManagerClient = new AmazonSecretsManagerClient(new BasicAWSCredentials("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY"));

var listSecretVersionRequest = new ListSecretVersionIdsRequest
{
    SecretId = secretId,
    IncludeDeprecated = true
};

var versionResponse = await secretsManagerClient.ListSecretVersionIdsAsync(listSecretVersionRequest);

// Do something with the versions ...

var request = new GetSecretValueRequest
{
    SecretId = secretId
};

var response = await secretsManagerClient.GetSecretValueAsync(request);

Console.WriteLine(response.SecretString);

var describeSecretRequest = new DescribeSecretRequest
{
    SecretId = secretId
};

var describeResponse = await secretsManagerClient.DescribeSecretAsync(describeSecretRequest);