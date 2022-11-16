namespace Reports
{
    public class DailyReportGenerator
    {
        private readonly ITransactionSource transactionSource;
        private readonly IDateTimeWrapper timeService;

        public DailyReportGenerator(ITransactionSource transactionSource, IDateTimeWrapper timeService)
        {
            this.transactionSource = transactionSource;
            this.timeService = timeService;
        }

        public void AddTransactions(IReportFile file, DateTime date)
        {
            if (file is null)
            {
                throw new ArgumentNullException("file", "Report file is null");
            }
            if (date > timeService.Today)
            {
                throw new ArgumentException("Invalid date", "date");
            }
            var transactions = transactionSource.GetByDate(date).OrderBy(x => x.accountId);

            foreach (var transaction in transactions)
            {
                file.Add(transaction.ToReportItem());
            }
        }
    }
}