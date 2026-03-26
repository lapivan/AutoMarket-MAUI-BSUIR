using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Application
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();

            var bmw = new CarBrand("BMW", "Германия", 1916);
            var toyota = new CarBrand("Toyota", "Япония", 1937);
            var tesla = new CarBrand("Tesla", "США", 2003);
            var audi = new CarBrand("Audi", "Германия", 1909);
            var mercedes = new CarBrand("Mercedes-Benz", "Германия", 1926);
            var volkswagen = new CarBrand("Volkswagen", "Германия", 1937);

            await unitOfWork.BrandRepository.AddAsync(bmw);
            await unitOfWork.BrandRepository.AddAsync(toyota);
            await unitOfWork.BrandRepository.AddAsync(tesla);
            await unitOfWork.BrandRepository.AddAsync(audi);
            await unitOfWork.BrandRepository.AddAsync(mercedes);
            await unitOfWork.BrandRepository.AddAsync(volkswagen);

            await unitOfWork.SaveAllAsync();

            var bmwAnnouncements = new[]
            {
                new CarAnnouncement("BMW X5 M-Sport", "X5", 85000m, "Отличное состояние, полный привод, 3.0 литра", 2021, bmw.Id),
                new CarAnnouncement("BMW M3 Competition", "M3", 95000m, "Спортивный седан, 510 л.с., разгон до 100 за 3.9 сек", 2022, bmw.Id),
                new CarAnnouncement("BMW X6 M50i", "X6", 110000m, "Спортивный кроссовер, полный привод, 530 л.с.", 2022, bmw.Id),
                new CarAnnouncement("BMW 740Li", "7 Series", 120000m, "Бизнес-класс, кожаный салон, массаж сидений, подогрев всех сидений", 2023, bmw.Id),
                new CarAnnouncement("BMW 530e", "5 Series", 65000m, "Гибрид, экономичный расход, M-пакет, 2.0 литра", 2022, bmw.Id),
                new CarAnnouncement("BMW Z4 Roadster", "Z4", 55000m, "Кабриолет, задний привод, 2.0 литра, 258 л.с.", 2021, bmw.Id),
                new CarAnnouncement("BMW X1 xDrive25e", "X1", 48000m, "Компактный кроссовер, подключаемый гибрид, полный привод", 2023, bmw.Id),
                new CarAnnouncement("BMW i4 M50", "i4", 75000m, "Электрический спортседан, 544 л.с., запас хода 520 км", 2023, bmw.Id)
            };

            var toyotaAnnouncements = new[]
            {
                new CarAnnouncement("Toyota Camry Hybrid", "Camry", 35000m, "Надёжная семейная, гибрид, расход 4.5 л", 2020, toyota.Id),
                new CarAnnouncement("Toyota Land Cruiser 300", "Land Cruiser", 130000m, "Внедорожник премиум-класса, 3.5 л V6, 415 л.с.", 2022, toyota.Id),
                new CarAnnouncement("Toyota RAV4 Adventure", "RAV4", 42000m, "Полный привод, внедорожный пакет, панорамная крыша", 2023, toyota.Id),
                new CarAnnouncement("Toyota Highlander Platinum", "Highlander", 55000m, "7 мест, кожа, подогрев всех сидений, 3.5 л", 2022, toyota.Id),
                new CarAnnouncement("Toyota GR Supra", "Supra", 62000m, "Спорткар, 3.0 литра, 340 л.с., задний привод", 2023, toyota.Id),
                new CarAnnouncement("Toyota Corolla Cross", "Corolla Cross", 28000m, "Компактный кроссовер, экономичный расход, 1.8 л", 2023, toyota.Id),
                new CarAnnouncement("Toyota Prius Prime", "Prius", 32000m, "Гибрид, расход 2.5 л, самый экономичный", 2023, toyota.Id),
                new CarAnnouncement("Toyota 4Runner TRD Pro", "4Runner", 68000m, "Настоящий внедорожник, рама, 4.0 л", 2022, toyota.Id)
            };

            var teslaAnnouncements = new[]
            {
                new CarAnnouncement("Tesla Model 3 Performance", "Model 3", 58000m, "Автопилот, разгон 0-100 за 3.3 сек, 560 км запас хода", 2023, tesla.Id),
                new CarAnnouncement("Tesla Model Y Long Range", "Model Y", 67000m, "Электрокроссовер, запас хода 533 км, полный привод", 2023, tesla.Id),
                new CarAnnouncement("Tesla Model S Plaid", "Model S", 135000m, "Спортивный седан, разгон 0-100 за 2.1 сек, 1020 л.с.", 2022, tesla.Id),
                new CarAnnouncement("Tesla Model X Long Range", "Model X", 115000m, "Электрокроссовер с 'крыльями чайки', 7 мест, полный привод", 2023, tesla.Id),
                new CarAnnouncement("Tesla Cybertruck", "Cybertruck", 80000m, "Электропикап, нерабочий прототип, бронированное стекло", 2024, tesla.Id),
                new CarAnnouncement("Tesla Model 3 Standard Range", "Model 3", 42000m, "Базовый седан, запас хода 491 км, задний привод", 2023, tesla.Id),
                new CarAnnouncement("Tesla Model Y Performance", "Model Y", 72000m, "Спортивная версия, разгон 0-100 за 3.7 сек", 2023, tesla.Id),
                new CarAnnouncement("Tesla Roadster", "Roadster", 200000m, "Спорткар, разгон 0-100 за 1.9 сек, 1000 км запас хода", 2024, tesla.Id)
            };

            var audiAnnouncements = new[]
            {
                new CarAnnouncement("Audi RS6 Avant", "RS6", 145000m, "Универсал-спорткар, 4.0 V8 Biturbo, 600 л.с.", 2022, audi.Id),
                new CarAnnouncement("Audi Q8 55 TFSI", "Q8", 95000m, "Флагманский кроссовер, лазерные фары, 3.0 литра", 2023, audi.Id),
                new CarAnnouncement("Audi e-tron GT", "e-tron GT", 110000m, "Электрический спортседан, 476 л.с., запас хода 488 км", 2023, audi.Id),
                new CarAnnouncement("Audi A8 L", "A8", 125000m, "Бизнес-седан, удлиненная версия, подогрев всех сидений", 2022, audi.Id),
                new CarAnnouncement("Audi Q5 Sportback", "Q5", 58000m, "Купеобразный кроссовер, 2.0 литра, полный привод", 2023, audi.Id),
                new CarAnnouncement("Audi R8 V10 Performance", "R8", 180000m, "Суперкар, 5.2 V10, 620 л.с., полный привод", 2022, audi.Id),
                new CarAnnouncement("Audi A7 Sportback", "A7", 75000m, "Элегантный лифтбек, 3.0 литра, 340 л.с.", 2023, audi.Id),
                new CarAnnouncement("Audi Q4 e-tron", "Q4 e-tron", 55000m, "Компактный электрокроссовер, запас хода 520 км", 2023, audi.Id)
            };

            var mercedesAnnouncements = new[]
            {
                new CarAnnouncement("Mercedes-Benz S500", "S-Class", 125000m, "Флагманский седан, подогрев и массаж всех сидений, 4.0 V8", 2023, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz E63 AMG", "E-Class", 110000m, "Спортседан, 4.0 V8 Biturbo, 612 л.с.", 2022, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz GLS600", "GLS", 140000m, "Роскошный внедорожник, 7 мест, 4.0 V8", 2023, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz C300", "C-Class", 55000m, "Бизнес-седан, 2.0 литра, 258 л.с.", 2023, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz G63 AMG", "G-Class", 210000m, "Легендарный G-Wagen, 4.0 V8 Biturbo, 585 л.с.", 2023, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz EQS 580", "EQS", 130000m, "Электрический седан, запас хода 680 км, 523 л.с.", 2023, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz CLA 45 AMG", "CLA", 62000m, "Компактный спортседан, 2.0 литра, 421 л.с.", 2022, mercedes.Id),
                new CarAnnouncement("Mercedes-Benz EQE SUV", "EQE SUV", 85000m, "Электрический кроссовер, запас хода 590 км", 2023, mercedes.Id)
            };

            var volkswagenAnnouncements = new[]
            {
                new CarAnnouncement("Volkswagen Golf GTI", "Golf", 38000m, "Хот-хэтч, 2.0 литра, 245 л.с., спортивная подвеска", 2022, volkswagen.Id),
                new CarAnnouncement("Volkswagen Tiguan R-Line", "Tiguan", 45000m, "Кроссовер, полный привод, 2.0 литра, 220 л.с.", 2023, volkswagen.Id),
                new CarAnnouncement("Volkswagen Passat CC", "Passat", 35000m, "Бизнес-седан, 2.0 литра, 190 л.с., кожаный салон", 2022, volkswagen.Id),
                new CarAnnouncement("Volkswagen Touareg V6", "Touareg", 65000m, "Большой кроссовер, 3.0 V6, полный привод", 2023, volkswagen.Id),
                new CarAnnouncement("Volkswagen ID.4 Pro", "ID.4", 50000m, "Электрокроссовер, запас хода 520 км", 2023, volkswagen.Id),
                new CarAnnouncement("Volkswagen Arteon", "Arteon", 48000m, "Элегантный лифтбек, 2.0 литра, 280 л.с.", 2022, volkswagen.Id),
                new CarAnnouncement("Volkswagen Multivan", "Multivan", 55000m, "Вместительный минивэн, 7 мест, гибрид", 2023, volkswagen.Id),
                new CarAnnouncement("Volkswagen ID. Buzz", "ID. Buzz", 65000m, "Электрический минивэн в стиле ретро, 7 мест", 2023, volkswagen.Id)
            };

            foreach (var ann in bmwAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            foreach (var ann in toyotaAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            foreach (var ann in teslaAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            foreach (var ann in audiAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            foreach (var ann in mercedesAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            foreach (var ann in volkswagenAnnouncements)
                await unitOfWork.AnnouncementRepository.AddAsync(ann);

            await unitOfWork.SaveAllAsync();

            await SyncData(unitOfWork);
        }

        private static async Task SyncData(IUnitOfWork unitOfWork)
        {
            var brands = await unitOfWork.BrandRepository.ListAllAsync();
            var announcements = await unitOfWork.AnnouncementRepository.ListAllAsync();

            foreach (var ann in announcements)
            {
                var brand = await unitOfWork.BrandRepository.FirstOrDefaultAsync(b => b.Id == ann.CarBrandId);
                if (brand != null)
                {
                    brand.AddAnnouncement(ann);
                    await unitOfWork.BrandRepository.UpdateAsync(brand);
                }
            }

            await unitOfWork.SaveAllAsync();
        }
    }
}