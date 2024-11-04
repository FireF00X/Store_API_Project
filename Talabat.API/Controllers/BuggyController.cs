using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Errors;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly TalabatDbContext _dbContext;

        public BuggyController(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFount")]
        public async Task<ActionResult> NotFoundError() // {{BaseUrl}}/api/Buggy/NotFount
        { 
            var product =await _dbContext.Products.Where(p=>p.Id == 100).FirstOrDefaultAsync();
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        [HttpGet("BadReq/{id}")]
        public ActionResult BadReq([FromRoute]int id) // {{BaseUrl}}/api/Buggy/BadReq/five
        {
            //var product = _dbContext.Products.Find(id);
            //if (product == null)
            //    return NotFound(new ApiResponse(400));
            //return Ok(product);
            return Ok();
        }
        [HttpGet("NullReference")]
        public ActionResult NullReference()// {{BaseUrl}}/api/Buggy/NullReference
        {
                var product = _dbContext.Products.Find(100);
                var productDto = product.ToString();
                return Ok(productDto);     
        }
        [HttpGet("BadRequest")]
        public ActionResult BadRequest()// {{BaseUrl}}/api/Buggy/BadRequest
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}
