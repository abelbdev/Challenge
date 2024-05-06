using RapidPay.Models;
using RapidPay.Persistence;
using RapidPay.Services.Base;

public class CardService : ICardService
{
    private readonly IAsyncRepository<Card> repository;
    private readonly IPaymentFeeService paymentService;
    public CardService(IAsyncRepository<Card> _repository, IPaymentFeeService _paymentService)
    {
        repository = _repository ?? throw new ArgumentNullException(nameof(repository));
        paymentService = _paymentService ?? throw new ArgumentNullException(nameof(paymentService));
    }
    public async Task<string> CreateCardAsync(CancellationToken cancellationToken)
    {
        string cardNumber = GenerateRandomCardNumber();
        await repository.Add(new Card() { CardNumber = cardNumber}, cancellationToken);
        var cards = await repository.GetAll();
        return cardNumber;
    }

    private string GenerateRandomCardNumber()
    {
        Random random = new Random();
        var value = random.Next(1000, 9999);
        return string.Concat("48202930293", value);
    }

    public async Task<decimal> GetCardBalanceAsync(string cardNumber)
    {
        var card = (await repository.GetWhere(c => c.CardNumber.Equals(cardNumber))).FirstOrDefault();
        if(card != null)
        {
            return card.Balance;
        }
        return 0;
    }

    public async Task<bool> PayAsync(string cardNumber, decimal amount, CancellationToken cancellationToken)
    {
        var card = (await repository.GetWhere(c => c.CardNumber.Equals(cardNumber))).FirstOrDefault();
        if(card != null)
        {
            var fee = paymentService.CalculatePaymentFee(amount);
            card.Balance -= (amount + fee);
            await repository.Update(card, cancellationToken);
            return true;
        }
        return false;
    }
}