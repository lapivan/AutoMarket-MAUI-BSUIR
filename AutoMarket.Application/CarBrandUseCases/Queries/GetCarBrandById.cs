using AutoMarket.Domain.Enums;

namespace AutoMarket.Application.CarBrandUseCases.Queries
{
    public record AnnouncementDto(
        int Id,
        string Title,
        string Model,
        decimal Price,
        string Description,
        int ManufactureYear,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        AnnouncementStatus Status,
        int? CarBrandId);

    public record BrandDetailsDto(
        int Id,
        string Name,
        string CountryOfOrigin,
        int YearFounded,
        IReadOnlyList<AnnouncementDto> Announcements);
    public record GetCarBrandByIdQuery(int Id) : IRequest<BrandDetailsDto?>;
    public class GetCarBrandByIdHandler : IRequestHandler<GetCarBrandByIdQuery, BrandDetailsDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCarBrandByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BrandDetailsDto?> Handle(GetCarBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                b => b.Announcements);

            if (brand == null) return null;
            return new BrandDetailsDto(
                brand.Id,
                brand.Name,
                brand.CountryOfOrigin,
                brand.YearFounded,
                brand.Announcements.Select(a => new AnnouncementDto(
                    a.Id,
                    a.Title,
                    a.Model,
                    a.Price,
                    a.Description,
                    a.ManufactureYear,
                    a.CreatedAt,
                    a.UpdatedAt,
                    a.Status,
                    a.CarBrandId))
                .ToList()
            );
        }
    }
}