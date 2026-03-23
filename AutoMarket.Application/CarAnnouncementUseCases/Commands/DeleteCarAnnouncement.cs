namespace AutoMarket.Application.CarAnnouncementUseCases.Commands
{
    public record DeleteCarAnnouncementCommand(int Id) : IRequest<bool>;
    public class DeleteCarAnnouncementHandler : IRequestHandler<DeleteCarAnnouncementCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCarAnnouncementHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteCarAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.AnnouncementRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null) return false;

            await _unitOfWork.AnnouncementRepository.DeleteAsync(entity, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return true;
        }
    }
}