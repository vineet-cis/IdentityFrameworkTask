using IdentityFrameworkTask.DataModels;
using IdentityFrameworkTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Linq;
using IdentityFrameworkTask.Helpers;
using IdentityFrameworkTask;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace IdentityFrameworkTask.Views.Participant
{ 
    public class ParticipantController : Controller
    {
        private readonly appDbContext context;
        public ParticipantController(appDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            
            List<ParticipantVM> participants = GetParticipants();
            return View("Index", participants);
        }

        public async Task<IActionResult> Create()
        {
         
            ParticipantVM model = new ParticipantVM();

            model.Students = await context.Students.ToListAsync();

            model.Events = await context.Events.ToListAsync();

            model.PartData = new EventParticipant();
            model.PartData.CreateDate = DateTime.Today;
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ParticipantVM part)
        {
            DateTime currentDate = DateTime.Today;
            
                if (part.IsChangeEventType)
                {
                   
                    part.Events = await context.Events.Where(wh => wh.EventType == part.EventData.EventType && wh.RegistrationCloseDate > currentDate).ToListAsync();
                    part.Students = await context.Students.ToListAsync();
                    part.IsChangeEventType = false;
                    return View(part);
                }

            if (ModelState.IsValid)
            {
                var stu = context.Students.FirstOrDefault(s =>  s.Id == part.SelectedStudentId);
                var evt = context.Events.FirstOrDefault(e => e.Id == part.SelectedEventId);
                var model = new EventParticipant()
                {
    
                    CreateDate = part.PartData.CreateDate,
                    EventID = evt.Id,
                    StudentID = stu.Id
                    
                    
                };
                context.EventParticipants.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(part); }
        }

        [HttpGet]
        public IActionResult GetEventNames(DataModels.Event.Type selectedEventType)
        {
            DateTime currentDate = DateTime.Today;
            var eventNames = context.Events
                .Where(e => e.EventType == selectedEventType && e.RegistrationCloseDate > currentDate)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.EventName
                })
                .ToList();

            return Json(eventNames);
        }


        public IActionResult Edit(int id)
        {
            var part = context.EventParticipants.SingleOrDefault(x => x.Id == id);
            var result = new EventParticipant()
            {
                CreateDate = part.CreateDate,
                EventID = part.EventID,
                StudentID = part.StudentID
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(EventParticipant model)
        {
            var part = new EventParticipant()
            {
                CreateDate = model.CreateDate,
                EventID = model.EventID,
                StudentID = model.StudentID
            };

            context.EventParticipants.Update(part);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var part = context.EventParticipants.SingleOrDefault(x => x.Id == id);

            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        public IActionResult Delete(int id)
        {
            var partData = context.EventParticipants.SingleOrDefault(x => x.Id == id);

            if (partData == null)
            {
                return NotFound();
            }

            return View(partData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCon(int id)
        {
            var part = context.EventParticipants.SingleOrDefault(e => e.Id == id);

            if (part != null)
            {
                context.EventParticipants.Remove(part);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public List<ParticipantVM> GetParticipants()
        {
            List<ParticipantVM> participants = context.EventParticipants.Join(
                context.Events,
                ePart => ePart.EventID,
                @event => @event.Id,
                (ePart, @event) => new { ePart, @event }
                )
                .Join(
                context.Students,
                joined => joined.ePart.StudentID,
                stu => stu.Id,
                (joined, stu) => new ParticipantVM
                {
                    Id = joined.ePart.Id,
                    EventName = joined.@event.EventName,
                    Name = $"{stu.FirstName} {stu.LastName}",
                    Age = stu.Age,
                    EventStartDate = joined.@event.EventStartDate,
                    EventEndDate = joined.@event.EventEndDate,
                    RegistrationDate = joined.ePart.CreateDate
                }
                ).ToList();

            return participants;
        }
    }
}
