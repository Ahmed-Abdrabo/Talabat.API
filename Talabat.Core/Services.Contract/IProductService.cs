using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);
        Task<IReadOnlyList<ProductCategory>> GetProductsCategoriesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync();
        Task<Product?> GetProductAsync(int id);
        Task<int> GetProductCountAsync(ProductSpecParams specParams);
    }
}
