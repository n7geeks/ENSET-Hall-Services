using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ENSETHall.ScolarityService.Services;

public interface ITextExtractor
{
	Task<string?> ExtractAsync(IFormFile file, CancellationToken cancellationToken = default);
}