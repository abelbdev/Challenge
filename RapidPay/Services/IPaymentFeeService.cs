namespace RapidPay.Services
{
    public interface IPaymentFeeService
    {
        decimal CalculatePaymentFee(decimal amount);
    }
}
