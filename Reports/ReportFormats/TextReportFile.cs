namespace Reports.ReportFormats
{
    public class TextReportFile : IReportFile
    {
        private List<ReportItem> reportItems;

        public TextReportFile()
        {
            reportItems = new List<ReportItem>();
        }

        public IReadOnlyList<ReportItem> Records => reportItems;

        public void Add(ReportItem record)
        {
            reportItems.Add(record);
        }

        public void AddRange(IEnumerable<ReportItem> records)
        {
            reportItems.AddRange(records);
        }

        public void Remove(ReportItem record)
        {
            if (reportItems.Contains(record))
            {
                reportItems = reportItems.Where(x => x != record).ToList();
            }
        }

        public void RemoveAll()
        {
            reportItems = new List<ReportItem>();
        }
    }
}