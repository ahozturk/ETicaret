using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }   

        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 1" , Price = 50, Stock= 10},
            //    new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 2" , Price = 150, Stock= 20},
            //    new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 3" , Price = 200, Stock= 30},
            //});
            //var a = await _productWriteRepository.SaveChanges();

            Product prod = await _productReadRepository.GetByIdAsync("070d9b6d-52fa-43b4-b82e-cd72a72b9708", false);
            prod.Name = "Product 6";
            await _productWriteRepository.SaveChanges();


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
