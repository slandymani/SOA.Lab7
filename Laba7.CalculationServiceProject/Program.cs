using System.Text.Json.Serialization;
using Laba7.CalculationLib;
using Laba7.CalculationServiceProject;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<FindIntersectionService>();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(cp => cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/",
    (FindIntersectionService intersectionService, [FromBody] FindIntersectionDto dto) => Results.Ok(
        intersectionService.Find(new TwoLines
        {
            Line1Point1 = dto.Line1Point1, Line1Point2 = dto.Line1Point2, Line2Point1 = dto.Line2Point1,
            Line2Point2 = dto.Line2Point2
        })));

app.Run();

[JsonSerializable(typeof(IntersectionPoint))]
[JsonSerializable(typeof(FindIntersectionDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}