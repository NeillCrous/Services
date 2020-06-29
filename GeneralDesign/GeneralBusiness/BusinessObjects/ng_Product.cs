using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralData.Repository;

namespace GeneralBusiness.BusinessObjects
{
    public class ng_Product: BaseBusinessObject
    {
        public ng_Product() { }
        public ng_Product(IGenericRepository repo)
        {
            Repo = repo;
        }
        public IEnumerable<GeneralData.Model.Entities.ng_Product> GetAll()
        {
            IEnumerable<GeneralData.Model.Entities.ng_Product> ng_Products = Repo.Find<GeneralData.Model.Entities.ng_Product>();
            return ng_Products;
        }

    }
}
