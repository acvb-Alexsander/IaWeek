using OpenAI;

namespace IaWeek.Extensions
{
    public static class OpenAiExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder) 
        {
            var apiKey = builder.Configuration["OpenAi:Key"];

            

            if(string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Warning: OPENAI_API_KEY environment variable is not set. OpenAI client will not be configured.");
            }

            var openAiClient = new OpenAIClient(apiKey);

            builder.Services.AddSingleton(openAiClient);
            return builder;
        }
    }
}
