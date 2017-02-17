using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLA.Models
{   
    public class PlagiarismData
    {
        public string st { get; set; }
        public string search_type { get; set; }
        public bool error { get; set; }
        public Actual_Results[] actual_results { get; set; }
    }

    public class Actual_Results
    {
        public string title { get; set; }
        public string url { get; set; }
        public string content { get; set; }
        public string full_content { get; set; }
        public string match_phrase { get; set; }
        public int percent { get; set; }
        public string st { get; set; }
    }
}