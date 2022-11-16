using FluentAssertions;
using Moq;
using Reports;
using Xunit;

namespace ReportTests
{
    public class DailyReportGeneratorTests
    {
        private readonly Mock<ITransactionSource> transactionSource;
        private readonly Mock<IDateTimeWrapper> timeService;
        private DailyReportGenerator sut;

        public DailyReportGeneratorTests()
        {
            transactionSource = new Mock<ITransactionSource>();
            timeService = new Mock<IDateTimeWrapper>();
            sut = new DailyReportGenerator(transactionSource.Object, timeService.Object);
        }

        [Fact]
        public void AddTransactions_Throws_IfNullFile()
        {
            sut.Invoking(x => x.AddTransactions(null, DateTime.Now))
                .Should().Throw<ArgumentNullException>()
                .WithMessage("Report file is null (Parameter 'file')");
        }

        [Fact]
        public void AddTransactions_Throws_IfFutureDate()
        {
            var file = new Mock<IReportFile>();
            var date = DateTime.Now;
            timeService.Setup(x => x.Today).Returns(date.AddDays(-1));

            sut.Invoking(x => x.AddTransactions(file.Object, date))
                .Should().Throw<ArgumentException>()
                .WithMessage("Invalid date (Parameter 'date')");
        }

        [Fact]
        public void AddTransactions_AddsCorrectNumberTransactions()
        {
            var file = new Mock<IReportFile>();
            var date = DateTime.Now;
            timeService.Setup(x => x.Today).Returns(date);
            transactionSource.Setup(x => x.GetByDate(date)).Returns(new List<Transaction>
            {
                new Transaction("acc5", date.AddHours(-1), 23.09M, "Added interest"),
                new Transaction("acc2", date.AddHours(-3), 2150M, "Payment from account 'acc34'")
            });

            sut.AddTransactions(file.Object, date);
            file.Verify(x => x.Add(It.IsAny<ReportItem>()), Times.Exactly(2));
        }

        [Fact]
        public void AddTransactions_AddsExpectedTransactions()
        {
            var file = new Mock<IReportFile>();
            var date = DateTime.Now;
            timeService.Setup(x => x.Today).Returns(date);
            transactionSource.Setup(x => x.GetByDate(date)).Returns(new List<Transaction>
            {
                new Transaction("acc5", date.AddHours(-1), 23.09M, "Added interest"),
                new Transaction("acc2", date.AddHours(-3), 2150M, "Payment from account 'acc34'")
            });

            sut.AddTransactions(file.Object, date);

            file.Verify(x => x.Add(new ReportItem("acc5", date.AddHours(-1), 23.09M)));
            file.Verify(x => x.Add(new ReportItem("acc2", date.AddHours(-3), 2150M)));
        }

        [Fact]
        public void AddTransactions_AddsCorrectOrder()
        {
            var file = new Mock<IReportFile>();
            var date = DateTime.Now;
            timeService.Setup(x => x.Today).Returns(date);
            transactionSource.Setup(x => x.GetByDate(date)).Returns(new List<Transaction>
            {
                new Transaction("acc5", date.AddHours(-1), 23.09M, "Added interest"),
                new Transaction("acc2", date.AddHours(-3), 2150M, "Payment from account 'acc34'")
            });
            var records = new List<ReportItem>();
            file.Setup(x => x.Add(It.IsAny<ReportItem>())).Callback<ReportItem>(x => records.Add(x));
            var expected = new List<ReportItem>
            {
                new ReportItem("acc5", date.AddHours(-1), 23.09M),
                new ReportItem("acc2", date.AddHours(-3), 2150M)
            };

            sut.AddTransactions(file.Object, date);

            records.Should().BeEquivalentTo(expected);
        }
    }
}