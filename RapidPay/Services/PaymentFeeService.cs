using RapidPay.Services.Base;

namespace RapidPay.Services
{
    public class PaymentFeeService : IPaymentFeeService
    {
        private readonly object lockObject = new object();
        private decimal currentFee;
        private Timer timer;

        public PaymentFeeService()
        {
            currentFee = GetRandomFee();
            timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        private void UpdateFee(object? state)
        {
            lock(lockObject)
            {
                currentFee = GetRandomFee();
                Console.WriteLine($"Fee updated: {currentFee} at {DateTime.Now}");
            }
        }

        private decimal GetRandomFee()
        {
            var random = new Random();
            return (decimal)random.Next(0, 200) / 100;
        }

        public decimal CalculatePaymentFee(decimal amount)
        {
            return GetFee()*amount;
        }

        private decimal GetFee()
        {
            lock (lockObject)
            {
                return currentFee/100;
            }
        }
    }
}
