using RapidPay.Services.Base;

public class CardService : ICardService
{
    public Task<string> CreateCardAsync()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetCardBalanceAsync(string cardNumber)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PayAsync(string cardNumber, decimal amount)
    {
        throw new NotImplementedException();
    }
}