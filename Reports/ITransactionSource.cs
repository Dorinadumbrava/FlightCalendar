using System.Transactions;

namespace Reports
{
    public interface ITransactionSource
    {
        public IEnumerable<Transaction> GetAll();

        public IEnumerable<Transaction> GetByDate(DateTime date);
    }
}