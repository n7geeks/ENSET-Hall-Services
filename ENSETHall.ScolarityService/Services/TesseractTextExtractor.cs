using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tesseract;

namespace ENSETHall.ScolarityService.Services;

public class TesseractTextExtractor : ITextExtractor
{
	public async Task<string?> ExtractAsync(IFormFile file, CancellationToken cancellationToken = default)
	{
		try
		{
			using var engine = new TesseractEngine(
				Path.Combine(Directory.GetCurrentDirectory(), "tessdata"), 
				"fra", 
				EngineMode.Default);
			byte[] fileBytes;
			using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms, cancellationToken);
				fileBytes = ms.ToArray();
			}
			using (var pix = Pix.LoadFromMemory(fileBytes))
			{
				using (var page = engine.Process(pix))
				{
					return page.GetText();
				}
			}
		}
		catch (Exception e)
		{
			return null;
		}

	}
}