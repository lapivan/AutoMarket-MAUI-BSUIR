namespace AutoMarket.Persistence.Repository
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly FakeBrandRepository _brandRepository;
        private readonly FakeAnnouncementRepository _announcementRepository;

        public FakeUnitOfWork()
        {
            _brandRepository = new FakeBrandRepository();
            _announcementRepository = new FakeAnnouncementRepository();
        }

        public IRepository<CarBrand> BrandRepository => _brandRepository;

        public IRepository<CarAnnouncement> AnnouncementRepository => _announcementRepository;
        public Task SaveAllAsync()
        {
            return Task.CompletedTask;
        }
        public Task CreateDataBaseAsync()
        {
            return Task.CompletedTask;
        }
        public Task DeleteDataBaseAsync()
        {
            return Task.CompletedTask;
        }
    }
}