using AutoMarket.Domain.Enums;

namespace AutoMarket.Application.CarAnnouncementUseCases.Commands
{
    public record UpdateCarAnnouncementCommand(
        int Id,
        string Title,
        string Model,
        decimal Price,
        string Description,
        int ManufactureYear,
        AnnouncementStatus Status) : IRequest<bool>;

    public class UpdateCarAnnouncementHandler : IRequestHandler<UpdateCarAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCarAnnouncementHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(UpdateCarAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null) return false;

            entity.ChangeTitle(request.Title);
            entity.ChangeModel(request.Model);
            entity.ChangePrice(request.Price);
            entity.ChangeDescription(request.Description);
            entity.ChangeManufactureYear(request.ManufactureYear);

            switch (request.Status)
            {
                case AnnouncementStatus.Active:
                    entity.Activate();
                    break;
                case AnnouncementStatus.Sold:
                    entity.MarkAsSold();
                    break;
                case AnnouncementStatus.Archived:
                    entity.Archive();
                    break;
            }

            await _unitOfWork.SaveAllAsync();
            return true;
        }
    }
}