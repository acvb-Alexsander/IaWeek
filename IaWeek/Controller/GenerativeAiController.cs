using IaWeek.Services;
using Microsoft.AspNetCore.Mvc;

namespace IaWeek.Controller
{
    [ApiController]
    public class GenerativeAiController : ControllerBase
    {
        private readonly ChatService _chatService;

        public GenerativeAiController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("api/ask-ai")]

        public async Task<IActionResult> GetGenerativeResponse([FromQuery] string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }
            try
            {
                var response = await _chatService.GetChatResponseAsync(prompt);
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
