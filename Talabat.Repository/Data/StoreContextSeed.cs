using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(TalabatDbContext _dbContext)
        {
            #region brands
            var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands.Count() > 0)
            {
                brands = brands.Select(b => new ProductBrand
                {
                    Name = b.Name
                }).ToList();
                if (_dbContext.ProductBrands.Count() == 0)
                {
                    foreach (var brand in brands)
                        await _dbContext.Set<ProductBrand>().AddAsync(brand);

                }
            }
            #endregion
            #region Categories
            var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

            if (types.Count() > 0)
            {

                if (_dbContext.ProductTypes.Count() == 0)
                {
                   await _dbContext.AddRangeAsync(types);
                }
            }
            #endregion
            #region Proudcts
            var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var productsFromJson = JsonSerializer.Deserialize<List<Product>>(productData);

            if (productsFromJson.Count() > 0)
            {
                if (_dbContext.Products.Count() == 0)
                {
                    foreach (var product in productsFromJson)
                      await _dbContext.Set<Product>().AddAsync(product);
                }
            }
            #endregion
            var deliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            if(deliveryMethods.Count() > 0)
            {
                if (_dbContext.DeliveryMethods.Count() == 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        await _dbContext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
