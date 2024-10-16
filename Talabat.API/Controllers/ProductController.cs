using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery]ProductSpecsParams specs)
        {
            var products = await _productRepo.GetAllWithSpecAsync(new ProductWithBrandAndTypeSpecs(specs));
            var productDto = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countSpecs = new ProductWithCountSpec(specs);
            var count = await _productRepo.GetCountAsync(countSpecs);
            return Ok(new PaginatedEntities<ProductDto>(specs.PageIndex,specs.PageSize,count,productDto));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetByIdAsynct(int id)
        {

            var product = await _productRepo.GetByIdWithSpecAsync(new ProductWithBrandAndTypeSpecs(id));
            var productDto = _mapper.Map<ProductDto>(product);
            if(product == null) 
                return NotFound(new {Message="Not Found", code=404});
            return Ok(productDto);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands =await _productBrandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllCategories()
        {
            var categories =await _productTypeRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
