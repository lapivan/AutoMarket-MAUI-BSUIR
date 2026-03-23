namespace AutoMarket.Application.CarAnnouncementUseCases.Commands
{
   public record CreateCarAnnouncementCommand(
        string Title,
        string Model,
        decimal Price,
        string Description,
        int ManufactureYear,
        int CarBrandId) : IRequest<int>;

    public class CreateCarAnnouncementCommandHandler : IRequestHandler<CreateCarAnnouncementCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCarAnnouncementCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateCarAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = new CarAnnouncement(
                request.Title,
                request.Model,
                request.Price,
                request.Description,
                request.ManufactureYear,
                request.CarBrandId);
            await _unitOfWork.AnnouncementRepository.AddAsync(announcement, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return announcement.Id;
        }
    }
}