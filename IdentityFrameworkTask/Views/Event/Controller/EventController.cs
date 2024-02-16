using Microsoft.AspNetCore.Mvc;
using IdentityFrameworkTask.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using IdentityFrameworkTask.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace IdentityFrameworkTask.Views.Event
{
    public class EventController : Controller
    {
        private readonly appDbContext context;

        public EventController(appDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var result = await context.Events.ToListAsync();

            var Model = result.Select(e => new EventVM
            {
                Id = e.Id,
                EventName = e.EventName,
                EventType = e.EventType,
                EventFrequency = e.EventFrequency,
                EventStartDate = e.EventStartDate,
                EventEndDate = e.EventEndDate,
                RegistrationOpenDate = e.RegistrationOpenDate,
                RegistrationCloseDate = e.RegistrationCloseDate,
                Price = e.Price,
                Limit = e.Limit,
                Discount = e.Discount
              
            }).ToList();
            return View("Index", Model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() 
        {
            var model = new EventVM();
            model.EventData = new DataModels.Event();
            model.EventData.EventStartDate = DateTime.Today;
            model.EventData.EventEndDate = DateTime.Today;
            model.EventData.RegistrationOpenDate = DateTime.Today;
            model.EventData.RegistrationCloseDate = DateTime.Today;
            return View("Create", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(EventVM evt)
        {
            if (ModelState.IsValid)
            {
                var model = new DataModels.Event()
                {
                    EventName = evt.EventData.EventName,
                    EventType = evt.EventData.EventType,
                    EventFrequency = evt.EventData.EventFrequency,
                    EventStartDate = evt.EventData.EventStartDate,
                    EventEndDate = evt.EventData.EventEndDate,
                    RegistrationOpenDate = evt.EventData.RegistrationOpenDate,
                    RegistrationCloseDate = evt.EventData.RegistrationCloseDate,
                    Price = evt.EventData.Price,
                    Discount = evt.EventData.Discount,
                    Limit = evt.EventData.Limit
                };
                context.Events.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(evt); }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        { 
            var evt = context.Events.SingleOrDefault(x => x.Id == id);
            var result = new DataModels.Event()
            {
                EventName = evt.EventName,
                EventType = evt.EventType,
                EventFrequency = evt.EventFrequency,
                EventStartDate = evt.EventStartDate,
                EventEndDate = evt.EventEndDate,
                RegistrationOpenDate = evt.EventStartDate,
                RegistrationCloseDate = evt.EventEndDate,
                Price = evt.Price, 
                Limit = evt.Limit
            };

            return View(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(DataModels.Event model)
        {
            var evt = new DataModels.Event()
            {
                Id = model.Id,
                EventName = model.EventName,
                EventType = model.EventType,
                EventFrequency = model.EventFrequency,
                EventStartDate = model.EventStartDate,
                EventEndDate = model.EventEndDate,
                RegistrationOpenDate = model.RegistrationOpenDate,
                RegistrationCloseDate = model.RegistrationCloseDate,
                Price = model.Price,
                Limit = model.Limit
            };
            context.Events.Update(evt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult updateDate(int Id, DateTime newDate)
        {
            var model = context.Events.FirstOrDefault(x => x.Id == Id);

            if(model != null)
            {
                model.EventStartDate = newDate;
                context.SaveChanges();
                return Json(model);
            }

            return Json(null);
        }

        public IActionResult Details(int id)
        {
            var evtData = context.Events.SingleOrDefault(x => x.Id == id);

            if (evtData == null)
            {
                return NotFound();
            }

            return View(evtData);
        }

        public IActionResult Delete(int id)
        {
            var evtData = context.Events.SingleOrDefault(x => x.Id == id);

            if (evtData == null)
            {
                return NotFound();
            }

            return View(evtData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCon(int? id)
        {
            var evt = context.Events.SingleOrDefault(e => e.Id == id);

            if (evt != null)
            {
                context.Events.Remove(evt);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
