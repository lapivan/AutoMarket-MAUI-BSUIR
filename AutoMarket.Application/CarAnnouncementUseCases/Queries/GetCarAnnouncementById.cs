using AutoMarket.Application.CarBrandUseCases.Queries;

namespace AutoMarket.Application.CarAnnouncementUseCases.Queries
{
    public record GetCarAnnouncementByIdQuery(int Id) : IRequest<AnnouncementDto?>;
    public class GetCarAnnouncementByIdHandler : IRequestHandler<GetCarAnnouncementByIdQuery, AnnouncementDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCarAnnouncementByIdHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<AnnouncementDto?> Handle(GetCarAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            var a = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id, cancellationToken);
            if (a == null) return null;
            return new AnnouncementDto(
                a.Id, a.Title, a.Model, a.Price, a.Description,
                a.ManufactureYear, a.CreatedAt, a.UpdatedAt, a.Status, a.CarBrandId);
        }
    }
}