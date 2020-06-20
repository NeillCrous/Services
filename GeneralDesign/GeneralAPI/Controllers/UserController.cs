using GeneralBusiness.BusinessProcesses;
using GeneralDTO;
using Microsoft.AspNetCore.Mvc;

namespace GeneralAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        UserProcess UserPO;

        public UserController()
        {
            UserPO = new UserProcess();
        }

        /// <summary>Insert a new user into the database.</summary>
        /// <param name="dto">Blueprint of the user.</param>
        [Route("Insert")]
        [HttpPost]
        public void Insert([FromBody] UserDTO dto)
        {
            UserPO.Insert(dto);
        }
    }
}
