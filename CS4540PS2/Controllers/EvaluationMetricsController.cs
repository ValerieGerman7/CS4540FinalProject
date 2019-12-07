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
/// Author: Valerie German, Pierce Bringhurst
/// Date: 6 Dec 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller the instructor's actions on evaluation metrics. This continues the InstructorController class.
/// </summary>
namespace CS4540PS2.Controllers
{
    [Authorize(Roles = "Instructor")]
    public partial class InstructorController : Controller
    {

        /// <summary>
        /// Creates a new evaluation metric entry for the given course and learning outcome, given a score and file.
        /// Verfies the current user is an instructor for the course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="emId"></param>
        /// <param name="score"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvaluationMetric(int? courseId, int? loid, string name, string description, IFormFile assignmentFile)
        {
            if (courseId == null || loid == null || assignmentFile == null || description == null)
            {
                return Json(new { success = false });
            }
            //Verify instructor
            CourseInstance course = _context.CourseInstance.Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefault();
            if (course == null) return Json(new { success = false });
            Instructors inst = course.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).FirstOrDefault();
            if (inst == null) return Json(new { success = false });

            LearningOutcomes loObj = _context.LearningOutcomes.Include(l => l.CourseInstance)
                .Where(l => loid == l.Loid && l.CourseInstance.Instructors.Contains(inst)).FirstOrDefault();
            if ( loObj == null)
            {
                return Json(new { success = false });
            }

            EvaluationMetrics em = new EvaluationMetrics();
            em.Name = name;
            em.Description = description;
            em.Lo = loObj;


            int? emid = null;
            if (assignmentFile != null)
            {
                string filename = assignmentFile.FileName;
                if (assignmentFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await assignmentFile.CopyToAsync(stream);
                        em.FileName = assignmentFile.FileName;
                        em.ContentType = assignmentFile.ContentType;
                        em.FileContent = stream.ToArray();

                    }

                }
      
                _context.EvaluationMetrics.Add(em);
                _context.SaveChanges();
                emid = em.Emid;
            }
           


            return RedirectToAction("EvaluationMetrics", new {emId = emid });
        }
        /// <summary>
        /// Show page of an evaluation metric
        /// </summary>
        /// <param name="emId"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult EvaluationMetrics(int? emId)
        {
            if (emId == null) return NotFound();
            EvaluationMetrics em = _context.EvaluationMetrics.Include(e => e.Lo).ThenInclude(l => l.CourseInstance).
                Where(e => e.Emid == emId).FirstOrDefault();
            if (em == null) return NotFound();
            return View(em);
        }

        /// <summary>
        /// Retries the file associated with the evaluation metric. Returns NotFound if the file is null, the user has invalid permissions
        /// or the record doesn't exist.
        /// </summary>
        /// <param name="emId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEvaluationMetric(int? emId)
        {
            EvaluationMetrics emObj = _context.EvaluationMetrics.Include(e => e.Lo).ThenInclude(l => l.CourseInstance)
                                            .ThenInclude(c => c.Instructors).ThenInclude(i => i.User)
                                            .Where(e => e.Emid == emId).FirstOrDefault();
            if (emObj == null || !emObj.Lo.CourseInstance.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).Any())
            {
                return NotFound();
            }
            if (emObj.FileContent == null || emObj.ContentType == null || emObj.FileName == null)
            {
                return NotFound();
            }
            return File(emObj.FileContent, emObj.ContentType, emObj.FileName);
        }

        /// <summary>
        /// Delete the evaluation metric. Returns a json object with a success boolean.
        /// </summary>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteEvaluationMetrics(int? emId)
        {
            EvaluationMetrics emObj = _context.EvaluationMetrics.Include(e => e.Lo).ThenInclude(l => l.CourseInstance)
                                            .ThenInclude(c => c.Instructors).ThenInclude(i => i.User)
                                            .Where(e => e.Emid == emId).FirstOrDefault();
            if (emObj == null || !emObj.Lo.CourseInstance.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).Any())
            {
                return Json(new { success = false });
            }
            _context.EvaluationMetrics.Remove(emObj);
            _context.SaveChanges();
            CourseInstance course = emObj.Lo.CourseInstance;
            return Json(new { success = true });
        }

    }
}