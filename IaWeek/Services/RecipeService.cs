using OpenAI;
using OpenAI.Chat;

namespace IaWeek.Services
{
    public class RecipeService
    {
        private readonly OpenAIClient _openAiClient;
        private readonly string _model;

        public RecipeService(OpenAIClient openAiClient, IConfiguration configuration)
        {
            _openAiClient = openAiClient;
            _model = configuration["OpenAi:ChatModel"] ?? "gpt-5.2";
        }

        public async Task<string> GetRecipeAsync(
            string ingredients, 
            string cuisine,
            string restrictions)
        {
            var systemMessage = new SystemChatMessage("Please generate a professional recipe based on the criteria provided.");

            var userMessage = new UserChatMessage($"You are a helpfully generates recipes based on the following criteria:\n" +
                                $"- Ingredients: {ingredients}\n" +
                                $"- Cuisine: {cuisine}\n" +
                                $"- Dietary Restrictions: {restrictions}\n" +
                                $"Please provide a recipe that fits these criteria."); 

            var messages = new List<ChatMessage>
            {
                systemMessage,
                userMessage
            };

            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f,
                MaxOutputTokenCount = 500

            };
            var chatClient = _openAiClient.GetChatClient(_model);

            var response = await chatClient.CompleteChatAsync(messages, options);

            return response.Value.Content[^1].Text ?? "Sorry, I couldn't generate a recipe.";
        }
    }
}
