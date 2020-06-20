using GeneralBusiness.BusinessProcesses;
using Microsoft.AspNetCore.Mvc;

namespace GeneralAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class StoredProcController : ControllerBase
    {
        [Route("ExecStoredProc/{sql}")]
        [HttpGet]
        public void ExecStoredProc(string sql)
        {
            ExecuteSql es = new ExecuteSql();
            es.GetTable(sql);
        }

        [Route("Playpin")]
        [HttpGet]
        public void Playpin(string sql)
        {
            ExecuteSql es = new ExecuteSql();
            es.GetTable(sql);
        }

    }
}
