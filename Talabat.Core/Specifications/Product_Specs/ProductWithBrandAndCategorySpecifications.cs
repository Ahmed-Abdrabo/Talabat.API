using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecification<Product>
    {
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base(P=>
            (string.IsNullOrEmpty(specParams.Search)||P.Name.ToLower().Contains(specParams.Search))&&
                        (!specParams.BrandId.HasValue||P.BrandId== specParams.BrandId.Value)
                        &&
                        (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value))
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderByAsc(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderByAsc(p => p.Name);
            }

            ApplyPagination((specParams.PageIndex-1)*specParams.PageSize, specParams.PageSize);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
