using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithCountSpec : BaseSpecifications<Product>
    {
        public ProductWithCountSpec(ProductSpecsParams specs) : base(P =>
        (string.IsNullOrEmpty(specs.SearchName) || P.Name.ToLower().Trim().Contains(specs.SearchName.ToLower().Trim())) &&
        (specs.BrandId.HasValue ? (P.BrandId == specs.BrandId) : true) &&
        (specs.CatId.HasValue ? (P.TypeId == specs.CatId) : true))
        {
        }
    }
}
