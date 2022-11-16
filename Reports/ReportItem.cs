namespace Reports
{
    public record ReportItem(string Id, DateTime time, decimal ammount);
    public record Transaction(string accountId, DateTime time, decimal ammount, string reason)
    {
        public ReportItem ToReportItem()
        {
            return new ReportItem(accountId, time, ammount);
        }
    }
}