namespace AutoMarket.Application.CarBrandUseCases.Commands
{
    public record UpdateCarBrandCommand(
        int Id,
        string Name,
        string CountryOfOrigin,
        int YearFounded) : IRequest<int?>;

    public class UpdateCarBrandCommandHandler : IRequestHandler<UpdateCarBrandCommand, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int?> Handle(UpdateCarBrandCommand request, CancellationToken cancellationToken)
        {
            var targetBrand = await _unitOfWork.BrandRepository.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (targetBrand == null) return null;
            targetBrand.ChangeName(request.Name);
            targetBrand.ChangeCountryOfOrigin(request.CountryOfOrigin);
            targetBrand.ChangeYearFounded(request.YearFounded);
            await _unitOfWork.BrandRepository.UpdateAsync(targetBrand, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return targetBrand.Id;
        }
    }
}