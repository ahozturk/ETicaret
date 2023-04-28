using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
           ETicaretAPI.Domain.Entities.Product p = await _productReadRepository.GetByIdAsync(request.Id);
            p.Name = request.Name;
            p.Stock = request.Stock;
            p.Price = request.Price;
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Product Güncellendi");
            return new();
        }
    }
}
