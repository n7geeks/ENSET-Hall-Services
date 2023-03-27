using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ENSETHall.ScolarityService.Services;

namespace ENSETHall.ScolarityService;

public static class OpenAiApiKeyExtension
{
	public static void AddOpenAiApiKeyProvider(this WebApplicationBuilder builder)
	{
		var options = new SecretClientOptions()
		{
			Retry =
			{
				Delay= TimeSpan.FromSeconds(2),
				MaxDelay = TimeSpan.FromSeconds(16),
				MaxRetries = 5,
				Mode = RetryMode.Exponential
			}
		};
		var client = new SecretClient(
			new Uri(builder.Configuration.GetSection("KeyVaultUrl").Value!), 
			new DefaultAzureCredential(), 
			options);
		KeyVaultSecret secret = client.GetSecret("OpenAiApiKey");

		builder.Services.AddSingleton<IOpenAiApiKeyProvider>(new OpenAiApiKeyProvider(secret.Value));
	}
}