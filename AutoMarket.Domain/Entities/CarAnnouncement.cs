using AutoMarket.Domain.Enums;

namespace AutoMarket.Domain.Entities
{
    public class CarAnnouncement : Entity
    {
        public string Title { get; private set; } = String.Empty;
        public string Model { get; private set; } = String.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = String.Empty;
        public int ManufactureYear { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int? CarBrandId { get; private set; } = 0;
        public CarBrand? CarBrand { get; private set; }
        public AnnouncementStatus Status { get; private set; } = AnnouncementStatus.Active;

        public CarAnnouncement(string title, string model, decimal price, string description,
                       int manufactureYear, int carBrandId)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title required");
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model required");
            if (price <= 0) throw new ArgumentException("Price must be positive");
            if (manufactureYear < 1886 || manufactureYear > DateTime.UtcNow.Year + 1)
                throw new ArgumentOutOfRangeException(nameof(manufactureYear));

            Title = title;
            Model = model;
            Price = price;
            Description = description ?? "";
            ManufactureYear = manufactureYear;
            CarBrandId = carBrandId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
            Status = AnnouncementStatus.Active;
        }
        private CarAnnouncement() { }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty", nameof(model));
            Model = model;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePrice(decimal price)
        {
            if (price <= 0) throw new ArgumentException("Price must be positive", nameof(price));
            Price = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty", nameof(description));
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeManufactureYear(int manufactureYear)
        {
            if (manufactureYear <= 1885 || manufactureYear > DateTime.UtcNow.Year + 1)
                throw new ArgumentOutOfRangeException(nameof(manufactureYear), "Manufacture year is invalid");
            ManufactureYear = manufactureYear;
            UpdatedAt = DateTime.UtcNow;
        }

        protected internal void AssignToBrand(CarBrand? brand)
        {
            CarBrand = brand;
            CarBrandId = brand?.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsSold()
        {
            Status = AnnouncementStatus.Sold;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Archive()
        {
            Status = AnnouncementStatus.Archived;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            Status = AnnouncementStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}