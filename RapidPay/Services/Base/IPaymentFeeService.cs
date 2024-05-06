namespace RapidPay.Services.Base
{
    public interface IPaymentFeeService
    {
        decimal CalculatePaymentFee(decimal amount);
    }
}
