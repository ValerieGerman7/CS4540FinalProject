﻿using CS4540PS2.Models;
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


        [HttpPost]
        public async Task<JsonResult> CreateSampleFile(int? courseId, int? emId, int score, IFormFile sample) {
            if(courseId == null || emId == null || sample == null) {
                return Json(new { success = false });
            }
            //Verify instructor
            CourseInstance course = _context.CourseInstance.Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefault();
            if(course == null) return Json(new { success = false });
            Instructors inst = course.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).FirstOrDefault();
            if(inst == null) return Json(new { success = false });

            EvaluationMetrics emObj = _context.EvaluationMetrics.Where(e => e.Emid == emId).FirstOrDefault();
            if (emObj == null) {
                return Json(new { success = false });
            }
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
                    }
                }
            }
            return Json(new { success = true });
        }

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

    }
}
