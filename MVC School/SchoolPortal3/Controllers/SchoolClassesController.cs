using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolPortal3.Data;
using SchoolPortal3.Entities;
using SchoolPortal3.Models;

namespace SchoolPortal3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SchoolClassesController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolClassesController(SchoolPortalDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            var classes = await _context.SchoolClasses.ToListAsync();
            //Hämtar första svaret tillbaka som har namnet och Id som matchar varandra. Vill ersätta Id med name som namn
            foreach(var schoolClass in classes)
            {
                schoolClass.ProgramManager = await _userManager.Users.FirstOrDefaultAsync(au => au.Id == schoolClass.ProgramManagerId);
            }


            return View(classes);
        }











        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }




            var studentIds = _context.SchoolClassStudents
               .Where(scs => scs.SchoolClassId == id)
               .Select(scs => scs.StudentId).ToList();
            var classStudents = await FetchUsersIdAsync(studentIds);

            var newModel = new SchoolClassViewModel()
            {
                Id = schoolClass.Id,
                ClassName = schoolClass.ClassName,
                ProgramManager = await _userManager.FindByIdAsync(schoolClass.ProgramManagerId),
                Students = classStudents
            };

            return View(newModel);
        }


        public async Task<IEnumerable<ApplicationUser>> FetchUsersIdAsync( List<string> ids )
        {         
            var users = new List<ApplicationUser>();
            foreach (var id in ids)
            {
                users.Add(await _userManager.FindByIdAsync(id));
            }
            return users;
        }












        // GET: SchoolClasses/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.ProgramManagers = await _userManager.GetUsersInRoleAsync("Program Manager");
            return View();
        }

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,ProgramManagerId,Created")] Entities.SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                schoolClass.Id = Guid.NewGuid();
                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewBag.ProgramManagers = await _userManager.GetUsersInRoleAsync("Program Manager");
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClassName,ProgramManagerId,Created")] SchoolClassViewModel schoolClass)
        {
            
            if (id != schoolClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(Guid id)
        {
            return _context.SchoolClasses.Any(e => e.Id == id);
        }
    }
}
