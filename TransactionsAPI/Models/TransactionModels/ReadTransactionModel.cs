namespace TransactionsAPI.Models.TransactionModels
{
    public class ReadTransactionModel
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStampUTC { get; set; } 
    }
}
