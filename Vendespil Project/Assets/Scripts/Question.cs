using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Question
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int teamId { get; set; }
        public string question { get; set; }
        public string correctAnwser { get; set; }
        public string wrongAnwser1 { get; set; }
        public string wrongAnwser2 { get; set; }
        public string wrongAnwser3 { get; set; }
    }
}
