namespace FutureCodr.Data.MockRepository
{
    using FutureCodr.Models;
    using System;

    public class MockRepository
    {
        public Bootcamp mockBootcamp;
        public BootcampLocation MockBootcampLocation;
        public BootcampSession mockBootcampSession;
        public BootcampTechnology mockBootcampTechnology;
        public ContactForm MockContactForm;
        public Link mockLink;
        public Location mockLocation;
        public Review mockReview;
        public Site mockSite;
        public Technology mockTechnology;
        public User mockUser;

        public MockRepository()
        {
            Site site = new Site
            {
                SiteName = "Test",
                SiteLogoURL = "TestUrl"
            };
            mockSite = site;
            BootcampLocation location = new BootcampLocation
            {
                BootcampID = 2,
                LocationID = 1
            };
            MockBootcampLocation = location;
            Location location2 = new Location
            {
                LocationID = 1,
                City = "Houston",
                Country = "USA"
            };
            mockLocation = location2;
            Technology technology = new Technology
            {
                Name = "Javascript"
            };
            mockTechnology = technology;
            BootcampTechnology technology2 = new BootcampTechnology
            {
                BootcampID = 1,
                TechnologyID = 1
            };
            mockBootcampTechnology = technology2;
            ContactForm form = new ContactForm
            {
                ContactFormID = 1,
                Email = "clabanow@aol.com",
                Message = "hello there",
                Name = "Charles"
            };
            MockContactForm = form;
            Bootcamp bootcamp = new Bootcamp
            {
                BootcampID = 1,
                PrimaryTechnologyID = 1,
                Name = "App Academy",
                LocationID = 2,
                Price = 0x2ee0,
                LengthInWeeks = 10,
                Website = "http://www.appacademy.com",
                LogoLink = "http://sample.com"
            };
            mockBootcamp = bootcamp;
            BootcampSession session = new BootcampSession
            {
                BootcampSessionID = 3,
                BootcampID = 1,
                LocationID = 1,
                TechnologyID = 1,
                StartDate = new DateTime(0x7de, 8, 1),
                EndDate = new DateTime(0x7de, 10, 0x16)
            };
            mockBootcampSession = session;
            Link link = new Link
            {
                LinkID = 1,
                URL = "http://www.wsj.com",
                LinkText = "Bootcamps in the WSJ",
                SiteID = 1,
                Date = new DateTime(0x7de, 2, 0x16),
                BootcampID = 1
            };
            mockLink = link;
            Review review = new Review
            {
                ReviewID = 1,
                ReviewText = "It was good",
                IsVerified = false,
                BootcampID = 2,
                UserID = 1
            };
            mockReview = review;
            User user = new User
            {
                UserID = 1,
                Email = "Hello@gmail.com",
                Password = "hello"
            };
            mockUser = user;
        }
    }
}
