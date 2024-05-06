using RapidPay.Services.Base;

namespace RapidPay.Services
{
    public class PaymentFeeService : IPaymentFeeService
    {
        private readonly object lockObject = new object();
        private decimal randomDecimal;
        private decimal lastFeeAmount;
        private Timer timer;

        public PaymentFeeService()
        {
            randomDecimal = GetRandomDecimal();
            lastFeeAmount = 1; //Start random value
            timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        private void UpdateFee(object? state)
        {
            lock(lockObject)
            {
                randomDecimal = GetRandomDecimal();
                Console.WriteLine($"Fee updated: {randomDecimal} at {DateTime.Now}");
            }
        }

        private decimal GetRandomDecimal()
        {
            var random = new Random();
            return (decimal)random.Next(0, 200) / 100;
        }

        public decimal CalculatePaymentFee(decimal amount)
        {
            lastFeeAmount = GetFee() * amount;
            return lastFeeAmount;
        }

        private decimal GetFee()
        {
            lock (lockObject)
            {
                return randomDecimal * lastFeeAmount;
            }
        }
    }
}
