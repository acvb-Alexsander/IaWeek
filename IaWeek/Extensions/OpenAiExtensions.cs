using OpenAI;

namespace IaWeek.Extensions
{
    public static class OpenAiExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder) 
        {
           // var apiKey = builder.Configuration["OPENAI_API_KEY"];

            var apiKey = Environment.GetEnvironmentVariable("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");

            if(string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException(" environment variable is not set.");
            }

            var openAiClient = new OpenAIClient(apiKey);

            builder.Services.AddSingleton(openAiClient);
            return builder;
        }
    }
}
