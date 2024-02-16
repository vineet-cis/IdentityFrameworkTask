using IdentityFrameworkTask.DataModels;
using IdentityFrameworkTask.Helpers;
using System;

namespace IdentityFrameworkTask.ViewModels
{
    public class EventVM
    {
        public Event EventData { get; set; }
        public decimal NetPrice { get; set; }
        public string EventName { get; set; }
        public Event.Type EventType { get; set; }
        public EventFrequency EventFrequency { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Limit { get; set; }
        public decimal Discount { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime RegistrationOpenDate { get; set; }
        public DateTime RegistrationCloseDate { get; set; }
    }
}
