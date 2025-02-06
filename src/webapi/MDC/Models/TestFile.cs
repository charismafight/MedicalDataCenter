namespace MDC.Models;

public class TestFile
{
    public int Id { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    public string MD5 { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public long FileSize { get; set; }
}
