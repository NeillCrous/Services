using GeneralData.Model;
using Microsoft.EntityFrameworkCore;

namespace GeneralData.Repository
{
    public class GeneralRepository : BaseRepository, IGenericRepository
    {
        internal override DbContext Context { get; set; }

        public GeneralRepository(string connectionString) => Context = new GeneralContext(connectionString);

        public GeneralRepository(GeneralContext context) => Context = context;
    }
}