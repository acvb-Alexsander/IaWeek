using IaWeek.Services;
using Microsoft.AspNetCore.Mvc;

namespace IaWeek.Controller
{
    [ApiController]
    public class GenerativeAiController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly RecipeService _recipeService;
        private readonly ImageService _imageService;

        public GenerativeAiController(ChatService chatService, RecipeService recipeService, ImageService imageService )
        {
            _chatService = chatService;
            _recipeService = recipeService;
            _imageService = imageService;
        }

        [HttpGet("api/ask-ai")]

        public async Task<IActionResult> GetGenerativeResponse([FromQuery] string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return BadRequest("the Prompt Parameter is required and cannot be empty.");
            }
            
            var response = await _chatService.GetChatResponseAsync(prompt);
            return Ok(new { response });
          
        }
        [HttpGet("api/ask-ai-options")]

        public async Task<IActionResult> AskAiOptions([FromQuery] string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return BadRequest("the Prompt Parameter is required and cannot be empty.");
            }
            
            
            var response = await _chatService.GetChatResponseWithOptionsAsync(prompt);
            return Ok(new { response });
            
            
        }
        [HttpGet("api/recipe-creator")]

        public async Task<IActionResult> GenerateRecipe(
            [FromQuery] string ingredients,
            [FromQuery] string cuisine,
            [FromQuery] string restrictions = "none")
        {
            if (string.IsNullOrEmpty(ingredients))
            {
                return BadRequest("the 'ingredients' Parameter is required and cannot be empty.");
            }
            
            var response = await _recipeService.GetRecipeAsync(ingredients, cuisine, restrictions);
            return Ok(new { response });
           
        }
        [HttpGet("api/generate-Image")]

        public async Task<IActionResult> GenerateImage(
            [FromQuery] string prompt,
            [FromQuery] string quality = "hd",
            [FromQuery] int n = 1,
            [FromQuery] int hight = 1024,
            [FromQuery] int width = 1024
            )
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return BadRequest("the 'prompt' Parameter is required and cannot be empty.");
            }
            
            var imageUrls = await _imageService.GenerateImageAsync(prompt, quality, n, hight, width);
            return Ok(new { imageUrls });
           
        }
    }
}
