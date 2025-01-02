using FluentBuilder.API.Models;
using FluentBuilder.API.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapPost("/api/test", (CreateTestRequest createTestRequest) =>
{
    var test = new Test
    {
        Id = Guid.NewGuid(),
        TestName = createTestRequest.TestName,
        Description = createTestRequest.Description,
        StartDate = createTestRequest.StartDate,
        EndDate = createTestRequest.EndDate,
        IsActive = createTestRequest.IsActive,
        Tags = createTestRequest.Tags,
        Difficulty = createTestRequest.Difficulty,
        MaxParticipants = createTestRequest.MaxParticipants,
        Sections = createTestRequest.Sections.Select(s => new TestSection
        {
            Title = s.Title,
            TimeLimit = s.TimeLimit,
            QuestionCount = s.QuestionCount
        }).ToList()
    };

    return Results.Ok(test);
});


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();