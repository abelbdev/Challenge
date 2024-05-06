namespace RapidPay.Models
{
    public class Card
    {
        public Card()
        {
            
        }
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
