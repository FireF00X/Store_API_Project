using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandAndTypeSpecs : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecs(ProductSpecsParams specs) : base(P =>
        (string.IsNullOrEmpty(specs.SearchName) || P.Name.ToLower().Trim().Contains(specs.SearchName.ToLower().Trim())) &&
            (specs.BrandId.HasValue ? (P.BrandId == specs.BrandId) : true) &&
            (specs.CatId.HasValue ? (P.TypeId == specs.CatId) : true))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "priceAsc":
                        AddOrderByAsc(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderByAsc(P => P.Name);
                        break;
                }
            }

            AddPagination((specs.PageIndex - 1) * specs.PageSize, specs.PageSize);
        }
        public ProductWithBrandAndTypeSpecs(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
