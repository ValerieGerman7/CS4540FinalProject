using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class SampleFiles
    {
        public int Sid { get; set; }
        public int Score { get; set; }
        public string FileName { get; set; }
        public int Emid { get; set; }

        public virtual EvaluationMetrics Em { get; set; }
    }
}
