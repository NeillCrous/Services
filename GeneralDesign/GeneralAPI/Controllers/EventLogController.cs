using GeneralBusiness.BusinessObjects;
using GeneralData.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GeneralAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("v1/[controller]")]
    public class EventLogController: ControllerBase
    {
        EventLog EventLogBO;

        public EventLogController()
        {
            EventLogBO = new EventLog();
        }

        [Route("Get")]
        [HttpGet]
        public IEnumerable<eventLog> Get() => EventLogBO.Get(DateTime.Now.AddDays(-7));
    }
}
