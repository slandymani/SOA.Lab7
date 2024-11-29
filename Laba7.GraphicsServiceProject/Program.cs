using System.Text.Json.Serialization;
using Laba7.GraphicLib;
using Laba7.GraphicsServiceProject;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<DrawService>();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(cp => cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/", (DrawService drawService, [FromBody] DrawLinesAndIntersectionDto dto) => Results.Ok(drawService.DrawLinesAndIntersection(new DrawParams
{
    TwoLines = dto.TwoLines,
    IntersectionPoint = dto.IntersectionPoint,
})));

app.Run();

[JsonSerializable(typeof(DrawLinesAndIntersectionDto))]
[JsonSerializable(typeof(DrawResult))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}