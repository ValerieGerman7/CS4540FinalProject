using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS4540PS2.Models;
using CS4540PS2.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CS4540PS2.Controllers
{
    [Authorize(Roles = "Admin, Chair, Instructor")]
    public class DBItemsController : Controller {
        private readonly LOTDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DBItemsController(LOTDBContext context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Index() {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            return View();
        }

        /// <summary>
        /// Gets all items in the database that the user can see, based on the user supplied
        /// search query
        /// </summary>
        /// <returns></returns>
        public DBItemList Search(string searchString) {
            var viewModel = new DBItemList();

            int x = User.Claims.Where(o => o.Value == "Chair").Count();
            int y = User.Claims.Where(o => o.Value == "Administrator").Count();
            if (x > 0 || y > 0) { 
                viewModel.Courses = _context.CourseInstance
                    .OrderBy(c => c.Number)
                    .AsNoTracking();
                viewModel.EvaluationMetrics = _context.EvaluationMetrics
                .OrderBy(e => e.Name)
                .AsNoTracking();
            }
            else {
                viewModel.Courses = _context.CourseInstance
                    .Where(c => c.Instructors.FirstOrDefault().UserId.ToString() == _userManager.GetUserAsync(User).Result.Id)
                    .OrderBy(c => c.Number)
                    .Include(c => c.LearningOutcomes)
                    .AsNoTracking();
                viewModel.EvaluationMetrics = _context.EvaluationMetrics
                    //.Where(e => e..FirstOrDefault().UserId.ToString() == _userManager.GetUserAsync(User).Result.Id)
                    // this needs to be only for the courses which the user has access, I could just include them from the courses.
                    .OrderBy(e => e.Name)
                    .AsNoTracking();
            }
            
            viewModel.EvaluationMetrics = _context.EvaluationMetrics
                .OrderBy(e => e.Name)
                .AsNoTracking();
            viewModel.Instructors = _context.Instructors
                .OrderBy(i => i.UserId)
                .AsNoTracking();
            viewModel.LearningOutcomes = _context.LearningOutcomes
                .OrderBy(l => l.Name)
                .AsNoTracking();
            return viewModel;
        }               
    }
}
