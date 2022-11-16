using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports
{
    public interface IReportFile
    {
        IReadOnlyList<ReportItem> Records { get; }

        void Add(ReportItem record);
        void AddRange(IEnumerable<ReportItem> records);
        void Remove(ReportItem record);
        void RemoveAll();
    }
}
