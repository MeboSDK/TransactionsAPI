namespace TransactionsAPI.Models.TransactionModels
{
    public class CreateTransactionModel
    {
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ReceiverEmail { get; set; }
        public decimal Amount { get; set; }
    }
}
