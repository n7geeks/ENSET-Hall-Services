namespace ENSETHall.ScolarityService.Services;

public class OpenAiApiKeyProvider : IOpenAiApiKeyProvider
{
	private readonly string _apiKey;
	public OpenAiApiKeyProvider(string apiKey) => _apiKey = apiKey;
	public string GetApiKey() => _apiKey;
}