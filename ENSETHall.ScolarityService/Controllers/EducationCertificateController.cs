using ENSETHall.ScolarityService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ENSETHall.ScolarityService.Controllers;

[Controller]
[Route("analyze")]
public class EducationCertificateController : Controller
{
	private readonly ITextExtractor _textExtractor;
	//private readonly IStudentInfoParser _studentInfoParser;

	public EducationCertificateController(ITextExtractor textExtractor, IStudentInfoParser studentInfoParser)
	{
		_textExtractor = textExtractor;
		//_studentInfoParser = studentInfoParser;
	}

	[HttpPost]
	public async Task<IActionResult> ExtractStudentInfo([FromForm] IFormFile file, CancellationToken cancellationToken)
	{
		var extractedText = await _textExtractor.ExtractAsync(file, cancellationToken);
		if (extractedText is null)
		{
			return BadRequest();
		}
		//var result = await _studentInfoParser.ParseAsync(
		//	extractedText, 
		//	cancellationToken);
		//if (result is null)
		//{
		//	return BadRequest();
		//}
		//Response.ContentType = "application/json";
		return Ok(extractedText);
	}
}
