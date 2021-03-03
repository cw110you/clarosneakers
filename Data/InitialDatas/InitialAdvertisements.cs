using System;
using webstore.Models;

namespace webstore.Data
{
    public class InitialAdvertisements
    {
        public Advertisement[] advertisements { get; set; }

        public InitialAdvertisements()
        {
            advertisements = new Advertisement[]
            {
                new Advertisement { AdLink = "#1", ImgSrc = "/images/advertisements/advertisement01.jpg", StartDate = new DateTime(2020, 4, 18), EndDate = new DateTime(2022, 4, 25) },
                new Advertisement { AdLink = "#2", ImgSrc = "/images/advertisements/advertisement02.jpg", StartDate = new DateTime(2020, 4, 19), EndDate = new DateTime(2023, 4, 26) },
                new Advertisement { AdLink = "#3", ImgSrc = "/images/advertisements/advertisement03.jpg", StartDate = new DateTime(2020, 4, 20), EndDate = new DateTime(2024, 4, 27) },
                new Advertisement { AdLink = "#4", ImgSrc = "/images/advertisements/advertisement04.jpg", StartDate = new DateTime(2020, 4, 21), EndDate = new DateTime(2025, 4, 28) }
                //new Advertisement { AdLink = "#", ImgSrc = "", StartDate = new DateTime (2020, 4, 2), EndDate = new DateTime (2020, 4, 2) }
            };
        }
    }
}