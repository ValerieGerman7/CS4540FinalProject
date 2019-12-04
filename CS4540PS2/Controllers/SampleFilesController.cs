using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 19 Nov 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller the instructor's actions on sample files. This continues the InstructorController class.
/// </summary>
namespace CS4540PS2.Controllers {
   [Authorize(Roles = "Instructor")]
    public partial class InstructorController : Controller {

        /// <summary>
        /// Creates a new sample file entry for the given course and evaluation metric, given a score and file.
        /// Verfies the current user is an instructor for the course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="emId"></param>
        /// <param name="score"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSampleFile(int? courseId, int? emId, int score, IFormFile sample) {
            if(courseId == null || emId == null || sample == null) {
                return Json(new { success = false });
            }
            //Verify instructor (course must not be archived)
            CourseInstance course = _context.CourseInstance.Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId && c.Status.Status != CourseStatusNames.Archived).FirstOrDefault();
            if(course == null) return Json(new { success = false });
            Instructors inst = course.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).FirstOrDefault();
            if(inst == null) return Json(new { success = false });

            EvaluationMetrics emObj = _context.EvaluationMetrics.Include(e => e.Lo).ThenInclude(l => l.CourseInstance)
                .Where(e => e.Emid == emId && e.Lo.CourseInstance.Instructors.Contains(inst)).FirstOrDefault();
            if (emObj == null) {
                return Json(new { success = false });
            }
            int? sfid = null;
            if (sample != null) {
                string filename = sample.FileName;
                if(sample.Length > 0) {
                    using(var stream = new MemoryStream()) {
                        await sample.CopyToAsync(stream);
                        SampleFiles sf = new SampleFiles() {
                            Score = score,
                            FileName = sample.FileName,
                            ContentType = sample.ContentType,
                            FileContent = stream.ToArray(),
                            Em = emObj
                        };
                        _context.SampleFiles.Add(sf);
                        _context.SaveChanges();
                        sfid = sf.Sid;
                    }
                }
            }
            return RedirectToAction("SampleFile", new { sfId = sfid });
        }

        /// <summary>
        /// Overview of the sample file - displays information about course, learning outcome and
        /// evaluation metric.
        /// </summary>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SampleFile(int? sfId) {
            if (sfId == null) return NotFound();
            SampleFiles sf = _context.SampleFiles.Include(s => s.Em).ThenInclude(e => e.Lo).ThenInclude(l => l.CourseInstance)
                .Where(s => s.Sid == sfId).FirstOrDefault();
            if (sf == null) return NotFound();
            return View(sf);
        }

        /// <summary>
        /// Retries the file associated with the sample file. Returns NotFound if the file is null, the user has invalid permissions
        /// or the record doesn't exist.
        /// </summary>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSampleFile(int? sfId) {
            SampleFiles sfObj = _context.SampleFiles.Include(s => s.Em).ThenInclude(e => e.Lo).ThenInclude(l => l.CourseInstance)
                                            .ThenInclude(c => c.Instructors).ThenInclude(i => i.User)
                                            .Where(s => s.Sid == sfId).FirstOrDefault();
            if (sfObj == null || !sfObj.Em.Lo.CourseInstance.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).Any()) {
                return NotFound();
            }
            if(sfObj.FileContent == null || sfObj.ContentType == null || sfObj.FileName == null) {
                return NotFound();
            }
            return File(sfObj.FileContent, sfObj.ContentType, sfObj.FileName);
        }

        /// <summary>
        /// Delete the sample file. Returns a json object with a success boolean.
        /// </summary>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [HttpPost]  
        public ActionResult DeleteSampleFile(int? sfId) {
            //Get the sample files object, the course must not be archived and the user must be an instructor of the course.
            SampleFiles sfObj = _context.SampleFiles.Include(s => s.Em).ThenInclude(e => e.Lo).ThenInclude(l => l.CourseInstance).ThenInclude(c => c.Status)
                                            .Include(s => s.Em).ThenInclude(e => e.Lo).ThenInclude(l => l.CourseInstance).ThenInclude(c => c.Instructors).ThenInclude(i => i.User)
                                            .Where(s => s.Sid == sfId && s.Em.Lo.CourseInstance.Status.Status != CourseStatusNames.Archived).FirstOrDefault();
            if (sfObj == null || !sfObj.Em.Lo.CourseInstance.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).Any()) {
                return Json(new { success = false });
            }
            _context.SampleFiles.Remove(sfObj);
            _context.SaveChanges();
            CourseInstance course = sfObj.Em.Lo.CourseInstance;
            return Json(new { success = true });
        }

    }
}
