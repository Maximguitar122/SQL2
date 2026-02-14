using Microsoft.EntityFrameworkCore;
using System.Linq;

public class Result
{
    public int Kilkist { get; set; }
    public decimal Dohid { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Result> Results => Set<Result>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=Store;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Result>().HasNoKey();
    }
}

class Program
{
    static void Main(string[] args)
    {
        using var db = new AppDbContext();
        db.Database.Migrate();

        var result = db.Results
            .FromSqlRaw("EXEC dbo.Sales")
            
            .First();

        Console.WriteLine($"Kilkist = {result.Kilkist}");
        Console.WriteLine($"Dohid = {result.Dohid}");
        Console.ReadKey();
    }
}