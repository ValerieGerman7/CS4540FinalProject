using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS4540PS2.Controllers {
    [Authorize(Roles="Admin")]
    public class UserController : Controller {
        private readonly LearningOutcomeDBContext _context;
        private readonly UserContext _userContext;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public UserController(LearningOutcomeDBContext context, UserContext userContext) {
            _context = context;
            _userContext = userContext;
        }
        public IActionResult Index() {
            return View();
        }
    }
}