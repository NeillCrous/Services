using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralBusiness.BusinessObjects;
using GeneralData.Model.Entities;
using Microsoft.AspNetCore.Mvc;


namespace GeneralAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        employee EmployeeBO;

        public EmployeeController()
        {
            EmployeeBO = new employee();
        }

        [Route("GetAllEmployees")]
        [HttpGet]
        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> lstEmps = EmployeeBO.GetAll();
            //*************Just a bit of Test Code
            //Dictionary<int, Employee> dictEmployees = new Dictionary<int, Employee>();
            //foreach (Employee e in lstEmps)
            //    dictEmployees.Add(e.EmployeeNo, e);

            //dictEmployees.TryGetValue(346, out Employee emp);

            //int i = emp.EmployeeNo;

            return lstEmps;
        }

        [RouteAttribute("GetEmployeeByEmployeeNo/{employeeno}")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployeeByEmployeeNo(int EmployeeNo)
        {
            IEnumerable<Employee> lstEmps = EmployeeBO.GetByEmployeeNo(EmployeeNo);

            return lstEmps;
        }
    }
}
