using RapidPay.Models;
using RapidPay.Persistence;
using RapidPay.Services.Base;

public class CardService : ICardService
{
    private readonly IAsyncRepository<Card> repository;
    public CardService(IAsyncRepository<Card> _repository)
    {
        repository = _repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task<string> CreateCardAsync()
    {
        string cardNumber = GenerateRandomCardNumber();
        await repository.Add(new Card() { CardNumber = cardNumber});
        return cardNumber;
    }

    private string GenerateRandomCardNumber()
    {
        Random random = new Random();
        var value = random.Next(1000, 9999);
        return string.Concat("482029302939", value);
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

    public async Task<bool> PayAsync(string cardNumber, decimal amount)
    {
        var card = (await repository.GetWhere(c => c.CardNumber.Equals(cardNumber))).FirstOrDefault();
        if(card != null)
        {
            card.Balance -= amount;
            await repository.Update(card);
            return true;
        }
        return false;
    }
}