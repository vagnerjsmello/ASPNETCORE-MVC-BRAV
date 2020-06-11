using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRAV.Context;
using BRAV.Models;

namespace BRAV.Controllers
{
    public class StudentController : Controller
    {
        private readonly BRAVContext _context;

        public StudentController(BRAVContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentViewModel.ToListAsync());
        }

        public async Task<IActionResult> GetStudents(string filter)
        {
            
            var studentsModel = await _context.StudentViewModel.ToListAsync();

            if (string.IsNullOrEmpty(filter))
            {
                return PartialView("List", studentsModel);
            }
            else
            {
                return PartialView("List", studentsModel.Where(x => x.FirstName.ToLower().Contains(filter.ToLower()) ||
                                                        x.LastName.ToLower().Contains(filter.ToLower()) ||
                                                        x.Email.ToLower().Contains(filter.ToLower())));
            }
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = await _context.StudentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Birthday")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = await _context.StudentViewModel.FindAsync(id);
            if (studentViewModel == null)
            {
                return NotFound();
            }
            return View(studentViewModel);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Birthday")] StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentViewModelExists(studentViewModel.Id))
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
            return View(studentViewModel);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = await _context.StudentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentViewModel = await _context.StudentViewModel.FindAsync(id);
            _context.StudentViewModel.Remove(studentViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentViewModelExists(int id)
        {
            return _context.StudentViewModel.Any(e => e.Id == id);
        }
    }
}
