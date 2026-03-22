using AutoMarket.Domain.Entities;

namespace AutoMarket.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<CarBrand> BrandRepository { get; }
        IRepository<CarAnnouncement> AnnouncementRepository { get; }
        Task SaveAllAsync();
        Task DeleteDataBaseAsync();
        Task CreateDataBaseAsync();
    }
}
