using GeneralData.Model.Entities;
using GeneralData.Repository;
using GeneralDTO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace GeneralBusiness.BusinessObjects
{
    public class User : BaseBusinessObject
    {
        public User() { }

        public User(IGenericRepository repo)
        {
            Repo = repo;
        }

        /// <summary>Create a new entity from a DTO.</summary>
        public users CreateNewUser(UserDTO dto)
        {
            users newEntity = new users { Name = dto.Name, Surname = dto.Surname };
            return newEntity;
        }

        /// <summary>Insert an entity into the database.</summary>
        public void Insert(users newEntity)
        {
            Repo.Insert(newEntity);
            Repo.Save();
        }

        /// <summary>Get all the employees.</summary>
        public IEnumerable<users> GetEmployeesFromSP(string sql)
        {
            List<users> users = Repo.ExecuteQuery<users>(sql).ToList();
            /*
            HashSet<users> hs = new HashSet<users>();
            hs.Add(new users() { Name = "", Surname = "" });

            var etes = hs.FirstOrDefault(x => x.Surname == "");

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Where(x => x.Key == "");
            dict.Where(x => x.Value == "");
            dict.Add("", "");

            decimal d = decimal.Parse("");

            decimal.TryParse("", out decimal dd);

            dd = 0;
            */

            return users;
        }
    }
}
