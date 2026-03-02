using OpenAI;
using OpenAI.Images;

namespace IaWeek.Services
{
    public class ImageService
    {
        private readonly OpenAIClient _openAiClient;
        private readonly string _model;

        public ImageService(OpenAIClient openAiClient, IConfiguration configuration)
        {
            _openAiClient = openAiClient;
            _model = configuration["OpenAi:ImageModel"] ?? "gpt-image-1.5";
        }

        public async Task<IEnumerable <string>> GenerateImageAsync(
            string prompt,
            string quality ="hd",
            int n = 1,
            int hight = 1024,
            int width = 1024)
        {
            var imageClient = _openAiClient.GetImageClient(_model);
            
            var options = new ImageGenerationOptions
            {
                Quality = quality.Equals("hd",StringComparison.OrdinalIgnoreCase) ? GeneratedImageQuality.High : GeneratedImageQuality.Standard,
                Size = GetSize(hight,width),
            };
            var response = await imageClient.GenerateImagesAsync(prompt, n, options);
            return response.Value.Select(result => result.ImageUri.ToString());
        }

        private GeneratedImageSize GetSize(int hight, int width)
        {
            return (hight, width) switch
            {
                (256, 256) => GeneratedImageSize.W256xH256,
                (512, 512) => GeneratedImageSize.W512xH512,
                (1024, 1024) => GeneratedImageSize.W1024xH1024,
                _ => GeneratedImageSize.W1024xH1024,
            };
        }

    }
}
