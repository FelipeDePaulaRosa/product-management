using MediatR;

namespace Application.Products.GetProductByCode
{
    public class GetProductByCodeRequest : IRequest<GetProductByCodeResponse>
    {
        public long Code { get; set; }

        public GetProductByCodeRequest(long code)
        {
            Code = code;
        }
    }
}