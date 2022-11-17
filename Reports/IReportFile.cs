namespace Reports
{
    public interface IReportFile
    {
        IReadOnlyList<ReportItem> Records { get; }

        void Add(ReportItem record);

        void Remove(ReportItem record);

        void RemoveAll();
    }
}