using GeneralData.Model.Entities;
using GeneralData.Repository;
using GeneralDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness.BusinessObjects
{
    public class employee : BaseBusinessObject
    {
        public employee() {}
        public employee(IGenericRepository repo)
        {
            Repo = repo;
        }

        public Employee createNewEmployee(EmployeeDTO dto)
        {
            Employee employee = new Employee { EmployeeNo = dto.EmployeeNo, Name = dto.Name, Surname = dto.Surname };
            return employee;
        }

        public void Insert(Employee newEntity)
        {
            Repo.Insert(newEntity);
            Repo.Save();
        }

        public void Update(Employee newEntity)
        {
            Repo.Update(newEntity);
            Repo.Save();
        }

        public IEnumerable<Employee> GetByEmployeeNo(int employeeNo) => Repo.Find<Employee>(x => x.EmployeeNo == employeeNo);

        public IEnumerable<Employee> GetAll() => Repo.Find<Employee>();


    }
}
