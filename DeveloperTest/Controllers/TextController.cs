using DeveloperTest.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeveloperTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {

        private readonly ILogger<TextController> _logger;
        private readonly TextEngine _textEngine;

        public TextController(ILogger<TextController> logger, TextEngine textEngine)
        {
            _logger = logger;
            _textEngine = textEngine;
        }

        [HttpPost]        
        public IActionResult GetAsync([FromBody]TextRequest textRequest)
        {
            try
            {
                _logger.LogDebug("Post method called");

                // Call TextEngine in order to process request and generate response
                var textResponse = _textEngine.HandleTextRequest(textRequest);                
                return new JsonResult(textResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("General error occured in post method", ex);
                return BadRequest();
            }
        }

    }
}
