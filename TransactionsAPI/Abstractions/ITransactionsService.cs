using TransactionsAPI.Models.TransactionModels;

namespace TransactionsAPI.Abstractions
{
    public interface ITransactionsService
    {
        Task<IEnumerable<ReadTransactionModel>> GetAllTransactionsAsync();  
    }
}
