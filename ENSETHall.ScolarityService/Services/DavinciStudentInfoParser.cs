using ENSETHall.ScolarityService.Models;
using Newtonsoft.Json.Linq;
using OpenAI;
using OpenAI.Models;

namespace ENSETHall.ScolarityService.Services;

public class DavinciStudentInfoParser : IStudentInfoParser
{
	private const string OpenAiApiKey = "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";
	private const string JsonOutput = "{\"fullName\":string,\"cne\":string,\"cin\":string,\"universityYear\":string,\"yearNumber\":number,\"major\":string,\"department\":string,\"diploma\":string,\"formation\":string}";
	private readonly string[] _majors =
	{
		"{major:glsid,majorFullName:'Génie du Logiciel et des Systèmes Informatiques Distribués',department:math-info,diploma:engineering,formation:init}",
		"{major:ii-bdcc,majorFullName:'Ingénierie Informatique : Big Data et Cloud Computing',department:math-info,diploma:engineering,formation:init}",
		"{major:sdia,majorFullName:'Systèmes Distribués et Intelligence Artificielle',department:math-info,diploma:master-res,formation:init}",
		"{major:mrmi,majorFullName:'Mécanique, Robotique et Matériaux Innovants',department:genie-meca,diploma:master-res,formation:init}",
		"{major:gmsi,majorFullName:'Génie Mécanique des Systèmes Industriels',department:genie-meca,diploma:engineering,formation:init}",
		"{major:gil,majorFullName:'Génie Industriel et Logistique',department:genie-meca,diploma:engineering,formation:init}",
		"{major:gecsi,majorFullName:'Génie Electrique et Contrôle des Systèmes Industriels',department:genie-elect,diploma:engineering,formation:init}",
		"{major:seer,majorFullName:'Génie Electrique option : Systèmes Electriques et Energies Renouvelables',department:genie-elect,diploma:engineering,formation:init}",
		"{major:eseg,majorFullName:'Education et Sciences Economiques et de Gestion',department:genie-eco,diploma:lep,formation:init}",
		"{major:fc,majorFullName:'Finance et Comptabilité',department:genie-eco,diploma:dut,formation:init}",
		"{major:aoe,majorFullName:'Administration et Organisation des Entreprises',department:staic,diploma:dut,formation:init}",
	};
	private readonly HttpRequestMessage _request = new HttpRequestMessage(
		HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
	public async Task<string?> ParseAsync(string text, CancellationToken cancellationToken)
	{
		var majors = _majors.Aggregate((a, b) => a + ", " + b);
		var prompt = "From the text above extract student info in a json file such as : " + JsonOutput +
		         ".\n Note that the major should only include initials in lowercase, and the available majors are: " +
		         majors + " (the department, formation, and diploma should only deduced from the available majors objects and not from the text). cni also known as cin (Numéro de la carte d'identité nationale). cne also knows as (Code national de l'étudiant). The answer should be a raw json text only.";
		var api = new OpenAIClient(OpenAiApiKey);
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