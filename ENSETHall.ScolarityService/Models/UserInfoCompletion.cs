using Newtonsoft.Json.Linq;

namespace ENSETHall.ScolarityService.Models;

public record UserInfoCompletion(
	string? fullName,
	string? cne,
	string? cin,
	int? yearNumber,
	string? universityYear,
	string? major,
	string? department,
	string? diploma,
	string? formation
)
{
	public static string FromText(string text)
	{
		var completionObject = JObject.Parse(text);
		var userInfoCompletion = new UserInfoCompletion(
			completionObject[nameof(fullName)]?.Value<string>(),
			completionObject[nameof(cne)]?.Value<string>(),
			completionObject[nameof(cin)]?.Value<string>(),
			completionObject[nameof(yearNumber)]?.Value<int>(),
			completionObject[nameof(universityYear)]?.Value<string>(),
			completionObject[nameof(major)]?.Value<string>(),
			completionObject[nameof(department)]?.Value<string>(),
			completionObject[nameof(diploma)]?.Value<string>(),
			completionObject[nameof(formation)]?.Value<string>());
		return userInfoCompletion.Output;
	}
	private string Output => $"{{ \"fullName\": \"{fullName}\", \"cne\": \"{cne}\", \"cin\": \"{cin}\", \"promo\": \"{int.Parse(universityYear?[..4] ?? "0") - yearNumber + 1}\", \"major\": \"{major}\", \"department\": \"{department}\", \"diploma\": \"{diploma}\", \"formation\": \"{formation}\" }}";
}