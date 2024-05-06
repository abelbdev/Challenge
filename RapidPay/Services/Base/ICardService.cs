namespace RapidPay.Services.Base
{
    public interface ICardService
    {
        Task<string> CreateCardAsync();
        Task<bool> PayAsync(string cardNumber, decimal amount);
        Task<decimal> GetCardBalanceAsync(string cardNumber);
    }
}
