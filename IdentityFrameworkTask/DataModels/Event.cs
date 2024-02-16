using IdentityFrameworkTask.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityFrameworkTask.DataModels
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EventName { get; set; }
        public Type EventType { get; set; }
        public EventFrequency EventFrequency { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime RegistrationOpenDate { get; set; }
        public DateTime RegistrationCloseDate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Limit { get; set; }

        public ICollection<EventParticipant> EventParticipants { get; set; }

        public enum Type
        {
            Dance = 1,
            Singing = 2,
            Drama = 3,
            Sports = 4,
            Debate = 5
        }

    }

}
