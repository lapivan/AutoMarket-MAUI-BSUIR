using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMarket.Domain.Entities
{
    public class CarBrand : Entity
    {
        private CarBrand() { }
        private List<CarAnnouncement> _announcements = new();
        public string Name { get; private set; } = String.Empty;
        public string CountryOfOrigin { get; private set; } = String.Empty;
        public int YearFounded { get; private set; }
        public IReadOnlyList<CarAnnouncement> Announcements
        {
            get => _announcements.AsReadOnly();
        }
        public CarBrand(string name, string countryOfOrigin, int yearFounded)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
            if (string.IsNullOrWhiteSpace(countryOfOrigin)) throw new ArgumentException("Country of origin cannot be empty", nameof(countryOfOrigin));
            if (yearFounded <= 1885 || yearFounded > DateTime.UtcNow.Year) throw new ArgumentOutOfRangeException(nameof(yearFounded), "Year founded is invalid");

            Name = name;
            CountryOfOrigin = countryOfOrigin;
            YearFounded = yearFounded;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
            Name = name;
        }

        public void ChangeCountryOfOrigin(string countryOfOrigin)
        {
            if (string.IsNullOrWhiteSpace(countryOfOrigin)) throw new ArgumentException("Country of origin cannot be empty", nameof(countryOfOrigin));
            CountryOfOrigin = countryOfOrigin;
        }

        public void ChangeYearFounded(int yearFounded)
        {
            if (yearFounded <= 1885 || yearFounded > DateTime.UtcNow.Year) throw new ArgumentOutOfRangeException(nameof(yearFounded), "Year founded is invalid");
            YearFounded = yearFounded;
        }

        public void AddAnnouncement(CarAnnouncement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

            if (announcement.CarBrandId.HasValue && announcement.CarBrandId.Value != 0 &&
                announcement.CarBrandId.Value != Id)
            {
                throw new InvalidOperationException($"Announcement already belongs to brand {announcement.CarBrandId}");
            }

            _announcements.Add(announcement);
            announcement.AssignToBrand(this);
        }

        public void RemoveAnnouncement(CarAnnouncement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

            if (_announcements.Remove(announcement))
            {
                announcement.AssignToBrand(null);
            }
        }
    }
}