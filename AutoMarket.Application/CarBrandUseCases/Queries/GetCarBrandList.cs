namespace AutoMarket.Application.CarBrandUseCases.Queries
{
    public record BrandListDto(int Id, string Name, string CountryOfOrigin, int YearFounded); // DTO to only show car brands without lists of announcements.
    public record GetCarBrandListQuery() : IRequest<IReadOnlyList<BrandListDto>>;
    public class GetCarBrandListHandler : IRequestHandler<GetCarBrandListQuery, IReadOnlyList<BrandListDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCarBrandListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<BrandListDto>?> Handle(GetCarBrandListQuery request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.BrandRepository.ListAllAsync(cancellationToken);
            return brands.Select(b => new BrandListDto(b.Id, b.Name, b.CountryOfOrigin, b.YearFounded)).ToList();
        }
    }
}