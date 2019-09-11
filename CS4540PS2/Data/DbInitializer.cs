using CS4540PS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540PS2.Data {
    public class DbInitializer {
        public static void Initialize(LearningOutcomeDBContext context) {
            context.Database.EnsureCreated();
            if (context.CourseInstance.Any()) {
                return;
            }
            var courses = new CourseInstance[] {
                new CourseInstance { CourseInstanceId=0, Name="Web Software Architecture", Description="Web software, HTML, JavaScript.", Number=4540, Semester="Fall", Year=2019 },
                new CourseInstance { CourseInstanceId=1, Name="Discrete Structures", Description="Proofs, statistics, booleans...", Number=2100, Semester="Fall", Year=2019 },
                new CourseInstance { CourseInstanceId=2, Name="Software Practice I", Description="Students will learn how to program in C#.", Number=3500, Semester="Fall", Year=2019 },
                new CourseInstance { CourseInstanceId=3, Name="Software Practice II", Description="Students will learn how to program in C++.", Number=3505, Semester="Fall", Year=2019 }
            };
            foreach(CourseInstance co in courses) {
                context.CourseInstance.Add(co);
            }
            context.SaveChanges();
            var learningo = new LearningOutcomes[] {
                new LearningOutcomes { Loid=0, CourseInstanceId=0, Name="Learn HTML", Description="Students will learn how to use HTML to create static and dynamic web pages"},
                new LearningOutcomes { Loid=1, CourseInstanceId=0, Name="JavaScript", Description="Students will learn how to use JavaScript in web pages"},
                new LearningOutcomes { Loid=2, CourseInstanceId=0, Name="Publish a Web Server", Description="Students will learn how to publish a web server"},
                new LearningOutcomes { Loid=3, CourseInstanceId=0, Name="Example Learning Outcome", Description="Example learning outcome description. This is where the deparment's description of the desired learning outcome will be displayed."},
                new LearningOutcomes { Loid=4, CourseInstanceId=1, Name="Write proofs", Description="Students are able to write proofs for common problems"},
                new LearningOutcomes { Loid=5, CourseInstanceId=1, Name="Boolean Logic", Description="Students are able to solve boolean logic."},
                new LearningOutcomes { Loid=6, CourseInstanceId=2, Name="Write C# Program", Description="Students are able to write code in C#"}
            };
            foreach(LearningOutcomes lo in learningo) {
                context.LearningOutcomes.Add(lo);
            }
            context.SaveChanges();
            var evals = new EvaluationMetrics[] {
                new EvaluationMetrics { Emid=0, Loid=0, Name="PS1", Description="Students will write static HTML web pages." },
                new EvaluationMetrics { Emid=1, Loid=0, Name="Exam 1", Description="The exam covers HTML concepts such as __ and __." },
                new EvaluationMetrics { Emid=2, Loid=1, Name="Assignment 1", Description="Assignment description ... this is where the professor describes how the assignment/exam fulfills the learning outcome. " +
                "The file for this assignment can be downloaded/viewed from here, as well as the sample files." },
                new EvaluationMetrics { Emid=3, Loid=3, Name="Example Assignment/Exam", Description="Assignment description ... this is where the professor describes how the assignment/exam fulfills the learning outcome. " +
                "The file for this assignment can be downloaded/viewed from here, as well as the sample files." },
                new EvaluationMetrics { Emid=4, Loid=4, Name="Homework 1", Description="Students will write proofs." },
                new EvaluationMetrics { Emid=5, Loid=6, Name="Programming Assignment 1", Description="Students write C# program." }
            };
            foreach(EvaluationMetrics em in evals) {
                context.EvaluationMetrics.Add(em);
            }
            context.SaveChanges();
            var samples = new SampleFiles[] {
                new SampleFiles { Sid=0, Emid=0, FileName="Example_Student_Work.pdf", Score=60 },
                new SampleFiles { Sid=1, Emid=0, FileName="Example_Student_Work.pdf", Score=70 },
                new SampleFiles { Sid=2, Emid=0, FileName="Example_Student_Work.pdf", Score=80 },
                new SampleFiles { Sid=3, Emid=0, FileName="Example_Student_Work.pdf", Score=90 },
                new SampleFiles { Sid=4, Emid=1, FileName="Example_Student_Work.pdf", Score=45 },
                new SampleFiles { Sid=5, Emid=1, FileName="Example_Student_Work.pdf", Score=70 },
                new SampleFiles { Sid=6, Emid=1, FileName="Example_Student_Work.pdf", Score=95 },
                new SampleFiles { Sid=7, Emid=3, FileName="Example_Student_Work.pdf", Score=0 },
                new SampleFiles { Sid=8, Emid=3, FileName="Example_Student_Work.pdf", Score=50 },
                new SampleFiles { Sid=9, Emid=3, FileName="Example_Student_Work.pdf", Score=100 },
                new SampleFiles { Sid=10, Emid=5, FileName="Example_Student_Work.pdf", Score=100 }
            };
            foreach(SampleFiles s in samples) {
                context.SampleFiles.Add(s);
            }
            context.SaveChanges();

        }
    }
}
