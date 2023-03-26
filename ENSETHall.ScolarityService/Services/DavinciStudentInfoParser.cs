using Microsoft.Identity.Client;
using OpenAI;
using OpenAI.Models;

namespace ENSETHall.ScolarityService.Services;

public class DavinciStudentInfoParser : IStudentInfoParser
{
	private const string OpenAiApiKey = "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";
	private const string JsonOutput = "{\"fullName\":string,\"cne\":string,\"cin\":string,\"promo\":number,\"major\":string,\"department\":string,\"diploma\":string}";
	private readonly string[] _majors =
	{
		"glsid (math-info department, engineering diploma)",
		"ii-bdcc (math-info department, engineering diploma)",
		"sdia (math-info department, master diploma)"
	};
	private readonly HttpRequestMessage _request = new HttpRequestMessage(
		HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
	public async Task<string?> ParseAsync(string text, CancellationToken cancellationToken)
	{
		var majors = _majors.Aggregate((a, b) => a + ", " + b);
		var prompt = "From the text above extract student info in a json file such as : " + JsonOutput +
		         ".\n The promo should be deduced from the \"année universitaire\", for example, for année universitaire 2019-2020, the promo is 2019. Note that the major should only include initials in lowercase, and the available majors are: " +
		         majors + " (the department and diploma should only deduced from the major name and not from the text). cni also known as cin (Numéro de la carte d'identité nationale). cne also knows as (Code national de l'étudiant). The answer should be a raw json text only.";
		var api = new OpenAIClient(OpenAiApiKey);
		try
		{
			return (await api.CompletionsEndpoint
					.CreateCompletionAsync(
						$"`{text}` \n {prompt}",
						temperature: 0.1,
						model: Model.Davinci,
						maxTokens: 2048,
						cancellationToken: cancellationToken))
				.Completions[0].Text.Trim();
		}
		catch (Exception e)
		{
			return null;
		}
	}
}