using GeneralData.Repository;

namespace GeneralBusiness
{
    public abstract class BaseBusinessObject
    {
        IGenericRepository repo;
        public IGenericRepository defaultRepo;
        public static string ConnectionString { get; set; }
        public IGenericRepository Repo { get { if (repo == null) repo = defaultRepo; return repo; } set { repo = value; } }

        public BaseBusinessObject() => defaultRepo = new GeneralRepository(ConnectionString);
        public BaseBusinessObject(IGenericRepository repo) => defaultRepo = repo;
    }
}
