using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Author: Valerie German
/// Date: 10 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: Controller for creating HTML with C# example.
/// </summary>
namespace CS4540PS2.Controllers {
    public class StringController : Controller {
        private readonly LearningOutcomeDBContext _context;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public StringController(LearningOutcomeDBContext context) {
            _context = context;
        }
        public IActionResult Index() {
            string webpage = "<div id=\"ContentDiv\">" +
                "<h1> @Model.Department @Model.Number @Model.CourseName </h1>" +
                "< h4 > @Model.CourseDescription < !--Professor name-- ></ h4 >" +
                "< div align = \"right\" >" +
                "< label >< b > Professor View </ b > -</ label >< button onclick = " +
                "\"RedirectToCourseDept('@Model.Department', @Model.Number, '@Model.Semester', @Model.Year);\" > Go To Department View </ button >" +
                "</ div >" +
                "< hr />" +
                "< h2 > Learning Outcomes </ h2 >" +
                "< div class=\"modal fade\" id=\"emModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"evaluationMetricModal\" aria-hidden=\"true\">" +
                "<div class=\"modal-dialog\" role=\"document\">" +
                "<div class=\"modal-content\">" +
                "<div class=\"modal-header\">" +
                "<h5 class=\"modal-title\" id=\"evaluationMetricModal\">Add Evaluation Metric</h5>" +
                "<button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">" +
                "<span aria-hidden=\"true\">&times;</span>" +
                "</button>" +
                "</div>" +
                "<div class=\"modal-body\">" +
                "<form>" +
                "<div class=\"form-group\">" +
                "<label>Title</label>" +
                "<input type = \"email\" class=\"form-control\" placeholder=\"Enter assignment name here\">" +
                "</div>" +
                "<div class=\"form-group\">" +
                "<label>Insert Metric Description</label>" +
                "<textarea class=\"form-control\" rows=\"3\" placeholder=\"Enter description here\"></textarea>" +
                "</div>" +
                "<div class=\"custom-file\">" +
                "<input type = \"file\" class=\"custom-file-input\">" +
                "<label class=\"custom-file-label\" for=\"customFile\">Choose file</label>" +
                "</div>" +
                "</form>" +
                "</div>" +
                "<div class=\"modal-footer\">" +
                "<button type = \"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>" +
                "<button type = \"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">Create</button>" +
                "</div>" +
                "</div>" +
                "</div>" +
                "</div>" +
                "<div class=\"modal fade\" id=\"sModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"sampleFileModal\" aria-hidden=\"true\">" +
                "<div class=\"modal-dialog\" role=\"document\">" +
                "<div class=\"modal-content\">" +
                "<div class=\"modal-header\">" +
                "<h5 class=\"modal-title\" id=\"sampleFileModal\">Add Sample</h5>" +
                "<button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">" +
                "<span aria-hidden=\"true\">&times;</span>" +
                "</button>" +
                "</div>" +
                "<div class=\"modal-body\">" +
                "<form>" +
                "<div class=\"form-group\">" +
                "<label>Insert Score</label>" +
                "<input type = \"number\" class=\"form-control-range\" min=\"0\" max=\"100\">" +
                "</div>" +
                "<div class=\"custom-file\">" +
                "<input type = \"file\" class=\"custom-file-input\">" +
                "<label class=\"custom-file-label\" for=\"customFile\">Choose file</label>" +
                "</div>" +
                "</form>" +
                "</div>" +
                "<div class=\"modal-footer\">" +
                "<button type = \"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>" +
                "<button type = \"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">Create</button>" +
                "</div>" +
                "</div>" +
                "</div>" +
                "</div>" +
                "<form id = \"exampleHomework\" class=\"form-inline\" method=\"get\" action=\"~/files/Example_Homework.pdf\" target=\"_blank\"></form>" +
                "<form id = \"exampleStudentWork\" class=\"form-inline\" method=\"get\" action=\"~/files/Example_Student_Work.pdf\" target=\"_blank\"></form>" +
                "@if(Model.LearningOutcomes.Count == 0) {" +
                "< p >< b > This course has no assigned learning outcomes.</ b ></ p >" +
                "}" +
                "@foreach(LearningOutcomeData lo in Model.LearningOutcomes) {" +
                "< button class=\"btn btn-primary sectionButton text-left\" type=\"button\" data-toggle=\"collapse\"" +
                "data-target=\"#collapseContent@{<text>@Model.LearningOutcomes.IndexOf(lo)</text>}\" aria-expanded=\"false\"" +
                "aria-controls=\"collapseContent@{<text>@Model.LearningOutcomes.IndexOf(lo)</text>}\">" +
                "@lo.LOName" +
                "</button>" +
                "<div class=\"collapse\" id=\"collapseContent@{<text>@Model.LearningOutcomes.IndexOf(lo)</text>}\">" +
                "<div class=\"card card-body\">" +
                "<div class=\"card\">" +
                "<div class=\"card-body\">" +
                "@lo.LODescription" +
                "</div>" +
                "</div>" +
                "<br />" +
                "<h5>" +
                "Evaluation Metrics" +
                "<button type= \"button\" class=\"btn btn-rounded btn-dark btn-sm float-right\" data-toggle=\"modal\" data-target=\"#emModal\">" +
                "<i>+</i>" +
                "</button>" +
                "</h5>" +
                "<hr />" +
                "@if(lo.EvaluationMetrics.Count == 0) {" +
                "< p >< i > This learning outcome has no evaluation metrics - < b > warning </ b > metrics are required, please add at least one.</ i ></ p >" +
                "}" +
                "@foreach(EvaluationMetricData em in lo.EvaluationMetrics) {" +
                "< div class=\"card shadowBox\">" +
                "<div class=\"card-header\">" +
                "<b>@em.Name</b>" +
                "<button class=\"btn btn-rounded btn-dark btn-sm float-right\" onclick=\"document.getElementById('exampleHomework').submit()\">Download Assignment File</button>" +
                "</div>" +
                "<div class=\"card-body\">" +
                "<p class=\"card-text indented\">@em.Description</p>" +
                "<p>" +
                "<label class=\"sampleLabelText\">" +
                "Sample Files: @if(em.Samples.Count == 0) {" +
                "< text >< i > There are no sample files. < b > Upload at least one sample.</ b ></ i ></ text >" +
                "}" +
                "</label>" +
                "@foreach(SamplesData sample in em.Samples) {" +
                "< button value = \"@sample.Score\" class=\"sampleButton\" onclick=\"document.getElementById('exampleStudentWork').submit()\"></button>" +
                "}" +
                "<button type = \"button\" class=\"btn btn-rounded btn-dark btn-sm\" data-toggle=\"modal\" data-target=\"#sModal\">" +
                "<i>+</i>" +
                "</button>" +
                "</p>" +
                "</div>" +
                "</div>" +
                "<br />" +
                "}" +
                "</div>" +
                "</div><!--End Collapse Div-->" +
                "<br /><br />" +
                "}" +
                "<br />" +
                "<hr />" +
                "<br />" +
                "</div>";

            ViewData["Webpage"] = webpage;
            return View();
        }
    }
}