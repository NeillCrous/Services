using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralData.Repository;
using GeneralDTO;

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

        public GeneralData.Model.Entities.ng_Product ng_ProductEntity(ng_ProductDTO dto)
        {
            GeneralData.Model.Entities.ng_Product newEntity = new GeneralData.Model.Entities.ng_Product
            { 
            description = dto.description,
            price = dto.price,
            imageUrl = dto.imageUrl,
            productCode = dto.productCode,
            productId = dto.productId,
            productName = dto.productName,
            releaseDate = dto.releaseDate,
            starRating  = dto.starRating
            };

            return newEntity;
        }

        public void Insert(ng_ProductDTO newEntity)
        {
            Repo.Insert(ng_ProductEntity(newEntity));
            Repo.Save();
        }
    }
}
