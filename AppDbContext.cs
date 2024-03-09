using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;"
            + "Database=master;"
            // + "User Id=sa;"
            // + "Password=Ruanps0197;"
            //+ "MultipleActiveResultSet=true;"
            + "Trusted_Connection=True;"
            + "Encrypt=YES;"
            + "TrustServerCertificate=YES");
    }
}