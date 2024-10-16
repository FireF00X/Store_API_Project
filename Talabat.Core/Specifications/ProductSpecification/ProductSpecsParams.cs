using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductSpecsParams
    {
        private const int MaxPageSize = 10;

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>MaxPageSize? MaxPageSize : value; }
        }
        public string? Sort {  get; set; }
        public int? BrandId { get; set; }
        public int? CatId { get; set; }
        public int PageIndex { get; set; }
        public string? SearchName { get; set; }
    }
}
