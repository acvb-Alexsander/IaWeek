
using OpenAI;
using OpenAI.Chat;

namespace IaWeek.Services
{
    public class ChatService
    {
        private readonly OpenAIClient _openAiClient;
        private readonly string _model;

        public ChatService(OpenAIClient openAiClient, IConfiguration configuration)
        {
            _openAiClient = openAiClient;
            _model = configuration["OpenAi:ChatModel"] ?? "gpt-5.2";
        }

        public async Task<string> GetChatResponseAsync(string prompt)
        {
            var chatClient = _openAiClient.GetChatClient(_model);
            var response = await chatClient.CompleteChatAsync(prompt);
            return response.Value.Content[^1].Text ?? "Sorry, I couldn't generate a response.";    
        }
        public async Task<string> GetChatResponseWithOptionsAsync(string prompt)
        {
            var chatClient = _openAiClient.GetChatClient(_model);

            var messages = new List<ChatMessage>
            {
                new UserChatMessage(prompt)
            };

            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f,
                MaxOutputTokenCount = 200
                
            };
            var response = await chatClient.CompleteChatAsync(messages, options);
            return response.Value.Content[^1].Text ?? "Sorry, I couldn't generate a response.";    
        }
    }
}
