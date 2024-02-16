using IdentityFrameworkTask.DataModels;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityFrameworkTask.ViewModels
{
    public class ParticipantVM
    {
        public int SelectedEventId { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public int SelectedStudentId { get; set; }
        public EventParticipant PartData { get; set; }
        public Event EventData { get; set; }
        public List<Event> Events { get; set; }
        public Student stuData { get; set; }
        public List<Student> Students { get; set; }
        public string Name { get; set; }
        public string EventName { get; set; }
        public string Event { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsChangeEventType { get; set; }


        public List<SelectListItem> EventDDL
        {
            get
            {
                var DDL = new List<SelectListItem>();

                if (Events != null)
                    foreach (var i in Events)
                    {
                        var item = new SelectListItem();
                        item.Value = i.Id.ToString();
                        item.Text = i.EventName;
                        DDL.Add(item);
                    }
                return DDL;
            }
        }

        public List<SelectListItem> StudentDDL
        {
            get
            {
                var DDL = new List<SelectListItem>();

                if (Events != null)
                    foreach (var i in Students)
                    {
                        var item = new SelectListItem();
                        item.Value = i.Id.ToString();
                        item.Text = i.FirstName + " " + i.LastName;
                        DDL.Add(item);
                    }
                return DDL;
            }
        }
    }
}
