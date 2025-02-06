namespace MDC.Models;

public class Test
{
    public int Id { get; set; }

    public string TestId { get; set; }

    public string Name { get; set; }

    public List<TestFile> TestFiles { get; set; }

    public string Category { get; set; }

    public DateTime? TestTime { get; set; }

    public DateTime? ReportTime { get; set; }
}
