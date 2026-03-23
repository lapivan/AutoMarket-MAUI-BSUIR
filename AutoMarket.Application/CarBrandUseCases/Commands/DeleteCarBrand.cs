namespace AutoMarket.Application.CarBrandUseCases.Commands
{
    public record DeleteCarBrandCommand(int Id) : IRequest<int?>;

    public class DeleteCarBrandCommandHandler : IRequestHandler<DeleteCarBrandCommand, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int?> Handle(DeleteCarBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (brand == null) return null;
            await _unitOfWork.BrandRepository.DeleteAsync(brand, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return request.Id;
        }
    }
}