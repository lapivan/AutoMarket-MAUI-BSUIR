using System.Linq.Expressions;

namespace AutoMarket.Persistence.Repository
{
    public class FakeBrandRepository : IRepository<CarBrand>
    {
        private readonly List<CarBrand> _brands = new();
        private int _nextId = 1;

        public FakeBrandRepository()
        {
            var bmw = new CarBrand("BMW", "Германия", 1916) { Id = _nextId++ };
            var toyota = new CarBrand("Toyota", "Япония", 1937) { Id = _nextId++ };
            var tesla = new CarBrand("Tesla", "США", 2003) { Id = _nextId++ };
            var audi = new CarBrand("Audi", "Германия", 1909) { Id = _nextId++ };

            _brands.AddRange(new[] { bmw, toyota, tesla, audi });
        }

        public async Task<CarBrand?> GetByIdAsync(int id, CancellationToken ct = default,
            params Expression<Func<CarBrand, object>>[]? includes)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(_brands.FirstOrDefault(b => b.Id == id));
        }

        public async Task<CarBrand?> FirstOrDefaultAsync(Expression<Func<CarBrand, bool>> filter, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var compiled = filter.Compile();
            return await Task.FromResult(_brands.FirstOrDefault(compiled));
        }

        public async Task<IReadOnlyList<CarBrand>> ListAllAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(_brands.AsReadOnly());
        }

        public async Task<IReadOnlyList<CarBrand>> ListAsync(Expression<Func<CarBrand, bool>> filter,
            CancellationToken ct = default, params Expression<Func<CarBrand, object>>[]? includes)
        {
            ct.ThrowIfCancellationRequested();
            var compiled = filter.Compile();
            var result = _brands.Where(compiled).ToList();
            return await Task.FromResult(result.AsReadOnly());
        }

        public async Task AddAsync(CarBrand entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (entity.Id == 0)
            {
                entity.Id = _nextId++;
            }
            _brands.Add(entity);
            await Task.CompletedTask;
        }

        public Task UpdateAsync(CarBrand entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var existing = _brands.FirstOrDefault(b => b.Id == entity.Id);
            if (existing != null)
            {
                _brands.Remove(existing);
                _brands.Add(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CarBrand entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var toRemove = _brands.FirstOrDefault(b => b.Id == entity.Id);
            if (toRemove != null)
            {
                _brands.Remove(toRemove);
                foreach (var ann in toRemove.Announcements.ToList())
                {
                    toRemove.RemoveAnnouncement(ann);
                }
            }
            return Task.CompletedTask;
        }
    }

    public class FakeAnnouncementRepository : IRepository<CarAnnouncement>
    {
        private readonly List<CarAnnouncement> _announcements = new();
        private int _nextId = 1001;

        public FakeAnnouncementRepository()
        {
            var ann1 = new CarAnnouncement("BMW X5 M-Sport", "X5", 85000m, "Отличное состояние", 2021, 1)
            {
                Id = _nextId++
            };
            var ann2 = new CarAnnouncement("BMW M3 Competition", "M3", 95000m, "Очень быстрая", 2022, 1)
            {
                Id = _nextId++
            };
            var ann3 = new CarAnnouncement("Toyota Camry Hybrid", "Camry", 35000m, "Надёжная семейная", 2020, 2)
            {
                Id = _nextId++
            };
            var ann4 = new CarAnnouncement("Tesla Model 3 Performance", "Model 3", 58000m, "Автопилот", 2023, 3)
            {
                Id = _nextId++
            };

            _announcements.AddRange(new[] { ann1, ann2, ann3, ann4 });
        }

        public async Task<CarAnnouncement?> GetByIdAsync(int id, CancellationToken ct = default,
            params Expression<Func<CarAnnouncement, object>>[]? includes)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(_announcements.FirstOrDefault(a => a.Id == id));
        }

        public async Task<CarAnnouncement?> FirstOrDefaultAsync(Expression<Func<CarAnnouncement, bool>> filter, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var compiled = filter.Compile();
            return await Task.FromResult(_announcements.FirstOrDefault(compiled));
        }

        public async Task<IReadOnlyList<CarAnnouncement>> ListAllAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(_announcements.AsReadOnly());
        }

        public async Task<IReadOnlyList<CarAnnouncement>> ListAsync(Expression<Func<CarAnnouncement, bool>> filter,
            CancellationToken ct = default, params Expression<Func<CarAnnouncement, object>>[]? includes)
        {
            ct.ThrowIfCancellationRequested();
            var compiled = filter.Compile();
            var result = _announcements.Where(compiled).ToList();
            return await Task.FromResult(result.AsReadOnly());
        }

        public async Task AddAsync(CarAnnouncement entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (entity.Id == 0)
            {
                entity.Id = _nextId++;
            }
            _announcements.Add(entity);

            if (entity.CarBrandId.HasValue && entity.CarBrandId.Value != 0)
            {
            }

            await Task.CompletedTask;
        }

        public Task UpdateAsync(CarAnnouncement entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var existing = _announcements.FirstOrDefault(a => a.Id == entity.Id);
            if (existing != null)
            {
                _announcements.Remove(existing);
                _announcements.Add(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CarAnnouncement entity, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var toRemove = _announcements.FirstOrDefault(a => a.Id == entity.Id);
            if (toRemove != null)
            {
                _announcements.Remove(toRemove);
                if (toRemove.CarBrand != null)
                {
                    toRemove.CarBrand.RemoveAnnouncement(toRemove);
                }
            }
            return Task.CompletedTask;
        }
    }
}