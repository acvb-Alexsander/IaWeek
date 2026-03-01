using OpenAI;

namespace IaWeek.Extensions
{
    public static class OpenAiExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder) 
        {
           // var apiKey = builder.Configuration["OPENAI_API_KEY"];

            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if(string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set.");
            }

            var openAiClient = new OpenAIClient(apiKey);

            builder.Services.AddSingleton(openAiClient);
            return builder;
        }
    }
}
