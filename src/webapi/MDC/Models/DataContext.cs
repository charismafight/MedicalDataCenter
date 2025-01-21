using Microsoft.EntityFrameworkCore;

namespace MDC.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=MDC.db");
#if DEBUG
        optionsBuilder.UseSeeding((context, _) =>
        {
            context.Set<Patient>().Add(new Patient
            {
                Name = "test patient",
                Age = "100岁",
                PatientId = "test001",
                Pinyin = "test pin yin",
                PinyinShort = "t",
                Sex = "男",
                Tests = new List<Test>{
                    new Test{Name="test1",TestId="1",Category="eyetest"},
                    new Test{Name="test2",TestId="2",Category="eyetest"},
                    new Test{Name="test3",TestId="3",Category="eyetest"},
                }
            });
            context.SaveChanges();
        });
#endif
    }

    // list Dbset Below to use
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Patient> Patients => Set<Patient>();
}
