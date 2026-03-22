using AutoMarket.Persistense.Data;

namespace AutoMarket.Persistense.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<CarBrand>> _brandRepository;
        private readonly Lazy<IRepository<CarAnnouncement>> _announcementRepository;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _brandRepository = new Lazy<IRepository<CarBrand>>(() => new EfRepository<CarBrand>(context));
            _announcementRepository = new Lazy<IRepository<CarAnnouncement>>(() => new EfRepository<CarAnnouncement>(context));
        }
        public IRepository<CarBrand> BrandRepository => _brandRepository.Value;

        public IRepository<CarAnnouncement> AnnouncementRepository => _announcementRepository.Value;

        public async Task CreateDataBaseAsync() => await _context.Database.EnsureCreatedAsync();
        public async Task DeleteDataBaseAsync() => await _context.Database.EnsureDeletedAsync();
        public async Task SaveAllAsync() => await _context.SaveChangesAsync();
    }
}
