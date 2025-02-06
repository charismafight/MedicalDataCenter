namespace MDC.Models;

using Microsoft.EntityFrameworkCore;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=MDC.db");
#if DEBUG
        optionsBuilder.UseSeeding(
            (context, _) =>
            {
                context.Set<TestFile>().ExecuteDelete();
                context.Set<Test>().ExecuteDelete();
                context.Set<Patient>().ExecuteDelete();
                context
                    .Set<Patient>()
                    .Add(
                        new Patient
                        {
                            Name = "xiao zhongju",
                            Age = string.Empty,
                            PatientId = "202405220001",
                            Pinyin = string.Empty,
                            PinyinShort = "t",
                            Sex = "男",
                            Tests = new List<Test>()
                            {
                                new ()
                                {
                                    Name = "角膜内皮细胞",
                                    TestId = "202405220001",
                                    Category = "角膜内皮细胞",
                                    // ReportPath = "202405220001.jpg",
                                    ReportTime = new DateTime(2024, 5, 22),
                                    TestTime = new DateTime(2024, 5, 22),
                                    TestFiles = [
new TestFile() {
    FileName = "202405220001.jpg",
    FilePath = "角膜内皮细胞/202405220001.jpg",
    CreateTime = DateTime.Now,
    MD5 = "for test",
    FileSize = 5000,
},
new TestFile() {
    FileName = "202405220002.jpg",
    FilePath = "角膜内皮细胞/202405220002.jpg",
    CreateTime = DateTime.Now,
    MD5 = "for test",
    FileSize = 5000,
},
                                    ],
                                },
                            },
                        }
                    );

                context
                    .Set<Patient>()
                    .Add(
                        new Patient
                        {
                            Name = "liu qixiu",
                            Age = string.Empty,
                            PatientId = "20240522002",
                            Pinyin = string.Empty,
                            PinyinShort = "t",
                            Sex = "男",
                            Tests =
                            [
                                new ()
                                {
                                    Name = "角膜内皮细胞",
                                    TestId = "2024052209",
                                    Category = "角膜内皮细胞",
                                    ReportTime = new DateTime(2024, 5, 22),
                                    TestTime = new DateTime(2024, 5, 22),
                                    TestFiles = [
                                        new TestFile()
                                        {
                                                FileName = "2024052209.jpg",
                                                FilePath = "角膜内皮细胞/2024052209.jpg",
                                                CreateTime = DateTime.Now,
                                                MD5 = "for test",
                                                FileSize = 5000,
                                        },
                                    ],
                                },
                            ],
                        }
                    );
                context.SaveChanges();
            }
        );
#endif
    }

    // list Dbset Below to use
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<TestFile> TestFiles => Set<TestFile>();
}
