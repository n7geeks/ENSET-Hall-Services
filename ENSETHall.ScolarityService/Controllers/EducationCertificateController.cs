using ENSETHall.ScolarityService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ENSETHall.ScolarityService.Controllers;

[Controller]
[Route("analyze")]
public class EducationCertificateController : Controller
{
	private readonly ITextExtractor _textExtractor;
	private readonly IStudentInfoParser _studentInfoParser;

	public EducationCertificateController(
		ITextExtractor textExtractor,
		IStudentInfoParser studentInfoParser)
	{
		_textExtractor = textExtractor;
		_studentInfoParser = studentInfoParser;
	}

	[HttpPost]
	public async Task<IActionResult> ExtractStudentInfo(
		[FromForm] IFormFile? file, 
		CancellationToken cancellationToken)
	{
		if (file is null)
		{
			return BadRequest(new
			{
				code = "MissingFile",
				message = "The file is missing."
			});
		}
		if (!file.ContentType.StartsWith("image/"))
		{
			return BadRequest(new
			{
				code = "InvalidFile",
				message = "The file is not an image."
			});
		}
		if (file.Length > 4 * 1024 * 1024)
		{
			return BadRequest(new
			{
				code = "InvalidFile",
				message = "The file is too large."
			});
		}
		var extractedText = await _textExtractor.ExtractAsync(file, cancellationToken);
		if (extractedText is null)
		{
			return BadRequest(new
			{
				code = "TextExtractionError",
				message = "Could not extract text from the image."
			});
		}
		var result = await _studentInfoParser.ParseAsync(
			extractedText, 
			cancellationToken);
		if (result is null)
		{
			return BadRequest(new
			{
				code = "TextParsingError",
				message = "Could not parse the text."
			});
		}
		var info = new
		{
			fullName = "",
			cne = "",
			cin = "",
			promo = "",
			major = "",
			department = "",
			diploma = "",
			formation = ""
		};
		return Ok(JsonConvert.DeserializeAnonymousType(result, info));
	}
}
