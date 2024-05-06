using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Services.Base;

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard(CancellationToken cancellationToken)
        {
            try
            {
                string cardNumber = await _cardService.CreateCardAsync(cancellationToken);
                return Ok(cardNumber);
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, "Operation cancelled");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred {ex.Message}");
            }

        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(string cardNumber, decimal amount, CancellationToken cancellationToken)
        {
            try
            {
                bool success = await _cardService.PayAsync(cardNumber, amount, cancellationToken);
                if (success)
                    return Ok("Payment successful");
                else
                    return BadRequest("Invalid card number");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, "Operation cancelled");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred {ex.Message}");
            }

        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            decimal balance = await _cardService.GetCardBalanceAsync(cardNumber);
            return Ok(balance);
        }
    }
}
