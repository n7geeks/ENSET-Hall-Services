using ENSETHall.ScolarityService;
using ENSETHall.ScolarityService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITextExtractor, TesseractTextExtractor>();
builder.Services.AddScoped<IStudentInfoParser, DavinciStudentInfoParser>();

builder.AddOpenAiApiKeyProvider();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();