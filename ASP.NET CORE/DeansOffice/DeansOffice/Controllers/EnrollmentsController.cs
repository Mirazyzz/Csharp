﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeansOffice.Data;
using DeansOffice.Models;
using DeansOffice.Models.ViewModels;

namespace DeansOffice.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id, string sortOrder)
        {
            ViewData["StudentSort"] = string.IsNullOrEmpty(sortOrder) ? "student_desc" : "";
            ViewData["GradeSort"] = sortOrder == "grade" ? "grade_desc" : "grade";

            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create(int? studentId, int? courseId)
        {
            var courses = _context.Courses.AsNoTracking();
            var students = _context.Students.AsNoTracking();

            if(studentId != null)
            {
                students = students.Where(s => s.StudentId == studentId);

                if(students.Any())
                {
                    ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseCode");
                    ViewData["Students"] = new SelectList(students, "StudentId", "FullName");

                    return View();
                }
                else
                {
                    return NotFound();
                }
            }

            if(courseId != null)
            {
                courses = courses.Where(c => c.CourseId == courseId);

                if (courses.Any())
                {
                    ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseCode");
                    ViewData["Students"] = new SelectList(students, "FullName", "StudentId");
                    
                    return View();
                }
            }

            ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseCode");
            ViewData["Students"] = new SelectList(students, "StudentId", "FullName");

            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,Grade,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.StudentId);
            return View(new EnrollmentViewModel
            {
                CourseId = enrollment.CourseId,
                StudentId = enrollment.StudentId,
                Grade = enrollment.Grade,
                EnrollmentDate = enrollment.EnrollmentDate
            });
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,StudentId,CourseId,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
