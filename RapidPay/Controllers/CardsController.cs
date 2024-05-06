using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Services.Base;

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard()
        {
            string cardNumber = await _cardService.CreateCardAsync();
            return Ok(cardNumber);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(string cardNumber, decimal amount)
        {
            bool success = await _cardService.PayAsync(cardNumber, amount);
            if (success)
                return Ok("Payment suceessful");
            else
                return BadRequest("Invalid card number");
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            decimal balance = await _cardService.GetCardBalanceAsync(cardNumber);
            return Ok(balance);
        }
    }
}
