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
            var ann5 = new CarAnnouncement("BMW X6 M50i", "X6", 110000m, "Спортивный кроссовер, полный привод, 530 л.с.", 2022, 1)
            {
                Id = _nextId++
            };
            var ann6 = new CarAnnouncement("BMW 740Li", "7 Series", 120000m, "Бизнес-класс, кожаный салон, массаж сидений", 2023, 1)
            {
                Id = _nextId++
            };
            var ann7 = new CarAnnouncement("BMW 530e", "5 Series", 65000m, "Гибрид, экономичный расход, M-пакет", 2022, 1)
            {
                Id = _nextId++
            };
            var ann8 = new CarAnnouncement("BMW Z4 Roadster", "Z4", 55000m, "Кабриолет, задний привод, 2.0 литра", 2021, 1)
            {
                Id = _nextId++
            };
            var ann9 = new CarAnnouncement("BMW X1 xDrive25e", "X1", 48000m, "Компактный кроссовер, подключаемый гибрид", 2023, 1)
            {
                Id = _nextId++
            };
            var ann10 = new CarAnnouncement("Toyota Land Cruiser 300", "Land Cruiser", 130000m, "Внедорожник премиум-класса, 3.5 л V6", 2022, 2)
            {
                Id = _nextId++
            };
            var ann11 = new CarAnnouncement("Toyota RAV4 Adventure", "RAV4", 42000m, "Полный привод, внедорожный пакет, панорамная крыша", 2023, 2)
            {
                Id = _nextId++
            };
            var ann12 = new CarAnnouncement("Toyota Highlander Platinum", "Highlander", 55000m, "7 мест, кожа, подогрев всех сидений", 2022, 2)
            {
                Id = _nextId++
            };
            var ann13 = new CarAnnouncement("Toyota GR Supra", "Supra", 62000m, "Спорткар, 3.0 литра, 340 л.с.", 2023, 2)
            {
                Id = _nextId++
            };
            var ann14 = new CarAnnouncement("Toyota Corolla Cross", "Corolla Cross", 28000m, "Компактный кроссовер, экономичный расход", 2023, 2)
            {
                Id = _nextId++
            };
            var ann15 = new CarAnnouncement("Tesla Model Y Long Range", "Model Y", 67000m, "Электрокроссовер, запас хода 533 км, полный привод", 2023, 3)
            {
                Id = _nextId++
            };
            var ann16 = new CarAnnouncement("Tesla Model S Plaid", "Model S", 135000m, "Спортивный седан, разгон 0-100 за 2.1 сек, 1020 л.с.", 2022, 3)
            {
                Id = _nextId++
            };
            var ann17 = new CarAnnouncement("Tesla Model X Long Range", "Model X", 115000m, "Электрокроссовер с 'крыльями чайки', 7 мест", 2023, 3)
            {
                Id = _nextId++
            };
            var ann18 = new CarAnnouncement("Tesla Cybertruck", "Cybertruck", 80000m, "Электропикап, нерабочий прототип", 2024, 3)
            {
                Id = _nextId++
            };
            var ann19 = new CarAnnouncement("Tesla Model 3 Standard Range", "Model 3", 42000m, "Базовый седан, запас хода 491 км", 2023, 3)
            {
                Id = _nextId++
            };

            var ann20 = new CarAnnouncement("Audi RS6 Avant", "RS6", 145000m, "Универсал-спорткар, 4.0 V8 Biturbo, 600 л.с.", 2022, 4)
            {
                Id = _nextId++
            };
            var ann21 = new CarAnnouncement("Audi Q8 55 TFSI", "Q8", 95000m, "Флагманский кроссовер, лазерные фары, 3.0 литра", 2023, 4)
            {
                Id = _nextId++
            };
            var ann22 = new CarAnnouncement("Audi e-tron GT", "e-tron GT", 110000m, "Электрический спортседан, 476 л.с.", 2023, 4)
            {
                Id = _nextId++
            };
            var ann23 = new CarAnnouncement("Audi A8 L", "A8", 125000m, "Бизнес-седан, удлиненная версия, подогрев всех сидений", 2022, 4)
            {
                Id = _nextId++
            };
            var ann24 = new CarAnnouncement("Audi Q5 Sportback", "Q5", 58000m, "Купеобразный кроссовер, 2.0 литра, полный привод", 2023, 4)
            {
                Id = _nextId++
            };
            _announcements.AddRange(new[]
            {
                // BMW (id = 1)
                ann1, ann2, ann5, ann6, ann7, ann8, ann9,
    
                // Toyota (id = 2)
                ann3, ann10, ann11, ann12, ann13, ann14,
    
                // Tesla (id = 3)
                ann4, ann15, ann16, ann17, ann18, ann19,
    
                // Audi (id = 4)
                ann20, ann21, ann22, ann23, ann24
            });
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