namespace RapidPay.Services.Base
{
    public interface ICardService
    {
        Task<string> CreateCardAsync(CancellationToken cancellationToken);
        Task<bool> PayAsync(string cardNumber, decimal amount, CancellationToken cancellationToken);
        Task<decimal> GetCardBalanceAsync(string cardNumber);
    }
}
