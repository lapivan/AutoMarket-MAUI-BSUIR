namespace AutoMarket.Application.CarBrandUseCases.Commands
{
    public record CreateCarBrandCommand(
        string Name,
        string CountryOfOrigin,
        int YearFounded) : IRequest<int>;

    public class CreateCarBrandCommandHandler : IRequestHandler<CreateCarBrandCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCarBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCarBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new CarBrand(request.Name, request.CountryOfOrigin, request.YearFounded);
            await _unitOfWork.BrandRepository.AddAsync(brand, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return brand.Id;
        }
    }
}