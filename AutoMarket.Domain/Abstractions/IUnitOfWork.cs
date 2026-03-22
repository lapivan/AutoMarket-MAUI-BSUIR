using AutoMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
