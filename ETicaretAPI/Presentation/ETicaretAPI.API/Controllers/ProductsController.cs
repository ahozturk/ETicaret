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
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly private ICustomerReadRepository _customerReadRepository;

        public ProductsController
            (
                IProductWriteRepository productWriteRepository,
                IProductReadRepository productReadRepository,
                IOrderWriteRepository orderWriteRepository,
                ICustomerWriteRepository customerWriteRepository,
                ICustomerReadRepository customerReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            Guid id = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new Customer() { Name = "Customer 2", Id = id });

            await _orderWriteRepository.AddAsync(new Order { Address = "Üsküdar", Description = "Açıklama 1", CustomerId=id });
            //await _orderWriteRepository.AddAsync(new Order { Address = "Kakdıköy", Description = "Açıklama 2" });
            await _orderWriteRepository.SaveAsync();
        }
        [HttpGet("{id}")]
        public async Task Get(string id)
        {
            Customer customer = await _customerReadRepository.GetByIdAsync(id);
            customer.Name = "Hakan";
            _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveAsync();

        }
    }
}
