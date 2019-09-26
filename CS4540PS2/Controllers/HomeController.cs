using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CS4540PS2.Models;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for home webpages.
/// </summary>
namespace CS4540PS2.Controllers {
    public class HomeController : Controller {
        private readonly LearningOutcomeDBContext _context;
        /// <summary>
        /// Creates home controller with given database context.
        /// </summary>
        /// <param name="context"></param>
        public HomeController(LearningOutcomeDBContext context) {
            _context = context;
        }
        /// <summary>
        /// Default home page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
