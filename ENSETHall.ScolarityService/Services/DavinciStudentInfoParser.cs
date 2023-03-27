using ENSETHall.ScolarityService.Domain;
using ENSETHall.ScolarityService.Models;
using OpenAI;
using OpenAI.Models;

namespace ENSETHall.ScolarityService.Services;

public class DavinciStudentInfoParser : IStudentInfoParser
{
	public DavinciStudentInfoParser(IOpenAiApiKeyProvider openAiApiKeyProvider) 
		=> _openAiApiKeyProvider = openAiApiKeyProvider;
	private readonly IOpenAiApiKeyProvider _openAiApiKeyProvider;
	private const string JsonOutput = 
		"{\"fullName\":string,\"cne\":string,\"cin\":string,\"universityYear\":string,\"yearNumber\":number,\"major\":string,\"department\":string,\"diploma\":string,\"formation\":string}";
	public async Task<string?> ParseAsync(string text, CancellationToken cancellationToken)
	{
		var majors = MajorsProvider.GetEnsetMajors();
		var prompt = "From the text above extract student info in a json file such as : " + JsonOutput +
		         ".\n Note that the major should only include initials in lowercase, and the available majors are: " +
		         majors + " (the department, formation, and diploma should only deduced from the available majors objects and not from the text). cni also known as cin (Numéro de la carte d'identité nationale). cne also knows as (Code national de l'étudiant). The answer should be a raw json text only.";
		var api = new OpenAIClient(_openAiApiKeyProvider.GetApiKey());
		try
		{
			var response = await api.CompletionsEndpoint
				.CreateCompletionAsync(
					$"`{text}` \n {prompt}",
					temperature: 0.1,
					model: Model.Davinci,
					maxTokens: 2048,
					cancellationToken: cancellationToken);
			return UserInfoCompletion.FromText(response.Completions[0].Text.Trim());
		}
		catch (Exception e)
		{
			return null;
		}
	}
}