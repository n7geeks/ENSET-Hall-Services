namespace ENSETHall.ScolarityService.Services;

public interface IStudentInfoParser
{
	Task<string?> ParseAsync(string text, CancellationToken cancellationToken);
}