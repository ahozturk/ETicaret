﻿using ETicaretAPI.Application.Repositories;
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
        public async void Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 1" , Price = 50, Stock= 10},
                new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 2" , Price = 150, Stock= 20},
                new (){ Id = Guid.NewGuid(), CreatedDate= DateTime.UtcNow, Name ="Product 3" , Price = 200, Stock= 30},
            });
            var a = await _productWriteRepository.SaveChanges();
        }
    }
}
