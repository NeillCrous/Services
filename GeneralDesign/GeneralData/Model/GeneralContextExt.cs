using Microsoft.EntityFrameworkCore;

namespace GeneralData.Model
{
    public partial class GeneralContext : DbContext
    {
        public GeneralContext(string connectionString) : base(GetOptions(connectionString)) { }

        static DbContextOptions GetOptions(string connectionString) => SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }
}
