using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Data;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)
        {
            if (storeContext.ProductBrands.Count()==0)
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<IList<ProductBrand>>(brandData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        storeContext.Add(brand);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.ProductCategories.Count() == 0)
            {
                var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<IList<ProductCategory>>(categoryData);
                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        storeContext.Add(category);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.Products.Count() == 0)
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<IList<Product>>(productData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        storeContext.Add(product);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.DeliveryMethods.Count() == 0)
            {
                var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<IList<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        storeContext.Add(deliveryMethod);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }

        }   
    }
}
