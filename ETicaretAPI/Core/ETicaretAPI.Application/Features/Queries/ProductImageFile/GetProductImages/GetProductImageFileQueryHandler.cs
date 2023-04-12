using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        async Task<List<GetProductImageFileQueryResponse>> IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>.Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            var response = product?.ProductImageFiles.Select(p => new GetProductImageFileQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.FileName}",
                FileName = p.FileName,
                Id = p.Id
            });

            return response.ToList();
        }
    }
}
