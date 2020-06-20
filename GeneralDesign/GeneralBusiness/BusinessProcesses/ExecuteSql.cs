using System.Data;

namespace GeneralBusiness.BusinessProcesses
{
    public class ExecuteSql : BaseBusinessObject
    {
        public DataTable GetTable(string sql)
        {
            return Repo.getTable(sql);
        }
    }
}
