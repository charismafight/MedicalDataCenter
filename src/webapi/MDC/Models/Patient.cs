namespace MDC.Models;

public class Patient
{
    public int Id { get; set; }

    public string PatientId { get; set; }

    public string Name { get; set; }

    public string Pinyin { get; set; }

    public string PinyinShort { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Sex { get; set; }

    public string Age { get; set; }

    public List<Test> Tests { get; set; }
}
