
using Scalar.AspNetCore;
using IaWeek.Extensions;
using IaWeek.Services;
var builder = WebApplication.CreateBuilder(args);

builder.AddOpenAI();

builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddSingleton<ImageService>();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => 
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
}
));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "Alexsander Basic Chat AI ",
            Version = "1.0",
            Description = "A simple API that demonstrates how to use OpenAI's GPT models to generate responses based on user prompts. This API includes endpoints for general chat interactions, recipe generation based on ingredients and cuisine, and image generation based on text prompts. The API is designed to be easy to use and can be integrated into various applications to provide AI-powered features.",
            Contact = new()
            {
                Name = "Your Name",
                Email = "acvb.developer@gmail.com",
                Url = new Uri("https://acvb-portfolio.netlify.app/")
            },


        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Alexsander Basic Chat AI ";
        options.Theme = ScalarTheme.Default;
        options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    } );
}

app.UseAuthorization();

app.MapControllers();

app.Run();
