using System;
using System.Collections.Generic;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains the model for the evaluation metrics table in the database.
/// </summary>
namespace CS4540PS2.Models {
    public partial class EvaluationMetrics {
        public EvaluationMetrics() {
            SampleFiles = new HashSet<SampleFiles>();
        }

        public int Emid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Loid { get; set; }

        public virtual LearningOutcomes Lo { get; set; }
        public virtual ICollection<SampleFiles> SampleFiles { get; set; }
    }
}
