using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralData.Model.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GeneralAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("v1/[controller]")]
    public class ng_ProductController : ControllerBase
    {
        GeneralBusiness.BusinessObjects.ng_Product ng_ProductBO;

        public ng_ProductController()
        {
            ng_ProductBO = new GeneralBusiness.BusinessObjects.ng_Product();
        }

        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<ng_Product> Get() => ng_ProductBO.GetAll();
    }
}
