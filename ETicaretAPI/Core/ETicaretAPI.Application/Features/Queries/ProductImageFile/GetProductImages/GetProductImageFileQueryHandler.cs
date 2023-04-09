using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, GetProductImageFileQueryResponse>
    {
        public Task<GetProductImageFileQueryResponse> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
