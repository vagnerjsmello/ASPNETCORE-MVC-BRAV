using Microsoft.EntityFrameworkCore;

namespace BRAV.Context
{
    public class BRAVContext : DbContext
    {
        public BRAVContext(DbContextOptions<BRAVContext> options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }

    }
}
