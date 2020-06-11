using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRAV.Context;
using BRAV.Models;
using AutoMapper;

namespace BRAV.Controllers
{
    public class StudentController : Controller
    {
        private readonly BRAVContext _context;
        private readonly IMapper _mapper;

        public StudentController(BRAVContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        public async Task<IActionResult> GetStudents(string filter)
        {
            try
            {
                //EF Core GetAll Students
                var students = await _context.Students.ToListAsync();

                //AutoMapper Entity to ViewModel
                var studentsViewModel = _mapper.Map<List<StudentViewModel>>(students);

                if (string.IsNullOrEmpty(filter))
                {
                    return PartialView("List", studentsViewModel);
                }
                else
                {
                    return PartialView("List", studentsViewModel.Where(x => x.FirstName.ToLower().Contains(filter.ToLower()) ||
                                                            x.LastName.ToLower().Contains(filter.ToLower()) ||
                                                            x.Email.ToLower().Contains(filter.ToLower())));
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                //EF Core Get Student
                var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

                //AutoMapper Entity to ViewModel
                var studentViewModel = _mapper.Map<StudentViewModel>(student);

                return PartialView("Details", studentViewModel);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public IActionResult Create()
        {
            return PartialView("Create", new StudentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,FirstName,LastName,Email,Birthday")] StudentViewModel studentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //AutoMapper ViewModel to Entity
                    var student = _mapper.Map<Student>(studentViewModel);
                    
                    //EFCore Add Student
                    _context.Add(student);
                    await _context.SaveChangesAsync();

                    return Json(new { data = "Operation performed successfully!", success = true });
                }
                return Json(new { data = "Invalid model state!", success = false });
            }
            catch
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //EF Core Get Student
            var student = await _context.Students.FindAsync(id);
            
            //AutoMapper Entity to ViewModel
            var studentViewModel = _mapper.Map<StudentViewModel>(student);

            return PartialView("Edit", studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Birthday")] StudentViewModel studentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //AutoMapper ViewModel to Entity
                    var student = _mapper.Map<Student>(studentViewModel);

                    //EF Core Update Student
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                return Json(new { data = "Invalid model state!", success = false });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            //EF Core Get Student
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            
            //AutoMapper Entity to ViewModel
            var studentViewModel = _mapper.Map<StudentViewModel>(student);

            return PartialView("Delete", studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteAsync(StudentViewModel studentViewModel)
        {
            try
            {
                if (studentViewModel.Id != 0)
                {
                    //EF Core Get Student
                    var student = await _context.Students.FindAsync(studentViewModel.Id);
                    
                    //EF Core Remove Student
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();

                    return Json(new { data = "Operation performed successfully!", success = true });
                }
                return Json(new { data = "Invalid model state!", success = false });
            }
            catch
            {
                throw;
            }
        }
    }
}
