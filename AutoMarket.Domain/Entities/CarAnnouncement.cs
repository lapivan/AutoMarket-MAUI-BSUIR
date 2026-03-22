using System;
using System.Collections.Generic;
using System.Text;
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
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public int CarBrandId { get; private set; } = 0;
        public AnnouncementStatus Status { get; private set; } = AnnouncementStatus.Active;

        public CarAnnouncement(string title, string model, decimal price, string description, int manufactureYear)
        {
            Title = title;
            Model = model;
            Price = price;
            Description = description;
            ManufactureYear = manufactureYear;
        }
        private CarAnnouncement() { }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return;
            }
            Title = title;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                return;
            }
            Model = model;
            UpdatedAt = DateTime.Now;
        }

        public void ChangePrice(decimal price)
        {
            if (price <= 0)
            {
                return;
            }
            Price = price;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return;
            }
            Description = description;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeManufactureYear(int manufactureYear)
        {
            if (manufactureYear <= 1885 || manufactureYear > DateTime.Now.Year + 1)
            {
                return;
            }
            ManufactureYear = manufactureYear;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeCarBrandId(int carBrandId)
        {
            if (carBrandId <= 0)
            {
                return;
            }
            CarBrandId = carBrandId;
            UpdatedAt = DateTime.Now;
        }

        public void MarkAsSold()
        {
            Status = AnnouncementStatus.Sold;
            UpdatedAt = DateTime.Now;
        }

        public void Archive()
        {
            Status = AnnouncementStatus.Archived;
            UpdatedAt = DateTime.Now;
        }

        public void Activate()
        {
            Status = AnnouncementStatus.Active;
            UpdatedAt = DateTime.Now;
        }
    }
}