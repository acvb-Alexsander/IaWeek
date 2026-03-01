using IaWeek.Services;
using Microsoft.AspNetCore.Mvc;

namespace IaWeek.Controller
{
    [ApiController]
    public class GenerativeAiController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly RecipeService _recipeService;

        public GenerativeAiController(ChatService chatService, RecipeService recipeService  )
        {
            _chatService = chatService;
            _recipeService = recipeService;
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
    }
}
