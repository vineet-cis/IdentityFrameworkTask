using IdentityFrameworkTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityFrameworkTask.Views.Student
{
    public class StudentController : Controller
    {
        private readonly appDbContext context;

        public StudentController(appDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var result = await context.Students.ToListAsync();
            return View("Index", result);
        }

        public async Task<IActionResult> Search()
        {
            var result = await context.Students.ToListAsync();
            return View("Search", result);
        }

        public IActionResult Create()
        {
            var model = new StudentVM();
            model.StuData = new DataModels.Student();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(StudentVM std)
        {
            if (ModelState.IsValid)
            {
                var model = new DataModels.Student()
                {
                    FirstName = std.StuData.FirstName,
                    LastName = std.StuData.LastName,
                    Address = std.StuData.Address,
                    Phone = std.StuData.Phone.Replace("-", string.Empty),
                    Age = std.StuData.Age
                };
                context.Students.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(std); }
        }

        public IActionResult Edit(int id)
        {
            var sdt = context.Students.SingleOrDefault(x => x.Id == id);
            var result = new DataModels.Student()
            {
                FirstName = sdt.FirstName,
                LastName = sdt.LastName,
                Address = sdt.Address,
                Phone = sdt.Phone,
                Age = sdt.Age
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(DataModels.Student model)
        {
            var std = new DataModels.Student()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Phone = model.Phone,
                Age = model.Age
            };

            context.Students.Update(std);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var stdData = context.Students.SingleOrDefault(x => x.Id == id);

            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }

        public IActionResult Delete(int id)
        {
            var stdData = context.Students.SingleOrDefault(x => x.Id == id);

            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCon(int id)
        {
            var std = context.Students.SingleOrDefault(e => e.Id == id);

            if (std != null)
            {
                context.Students.Remove(std);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
