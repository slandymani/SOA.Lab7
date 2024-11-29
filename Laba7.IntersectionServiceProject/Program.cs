using System.Text.Json;
using System.Text.Json.Serialization;
using Laba7.CalculationLib;
using Laba7.CalculationServiceProject;
using Laba7.GraphicLib;
using Laba7.GraphicsServiceProject;
using Laba7.IntersectionServiceProject;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new HttpClient());
builder.Services.AddSingleton<DrawService>();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(cp => cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/", async (HttpClient httpClient, [FromBody] DrawIntersectionDto dto) =>
{
    using var response = await httpClient.PostAsJsonAsync("http://localhost:5020", new FindIntersectionDto
    {
        Line1Point1 = dto.Line1Point1,
        Line1Point2 = dto.Line1Point2,
        Line2Point1 = dto.Line2Point1,
        Line2Point2 = dto.Line2Point2
    });
    response.EnsureSuccessStatusCode();
    var textBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(textBody);
    var body = await response.Content.ReadFromJsonAsync<IntersectionPoint>();
    if (body is null)
        return Results.Problem();
    using var graphicResponse = await httpClient.PostAsJsonAsync("http://localhost:5156", new DrawLinesAndIntersectionDto
    {
        TwoLines = new TwoLines
        {
            Line1Point1 = dto.Line1Point1,
            Line1Point2 = dto.Line1Point2,
            Line2Point1 = dto.Line2Point1,
            Line2Point2 = dto.Line2Point2
        },
        IntersectionPoint = body
    });
    graphicResponse.EnsureSuccessStatusCode();
    var graphicBody = await graphicResponse.Content.ReadFromJsonAsync<DrawResult>();
    if (graphicBody is null)
        return Results.Problem();
    return Results.Ok(new DrawIntersectionResult
    {
        IntersectionPoint = body,
        Base64 = graphicBody,
    });
});

app.Run();
