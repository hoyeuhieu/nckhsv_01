using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyVideo
{
    public class PlateReaderResult
    {
        public string processing_time { get; set; }
        public string filename { get; set; }
        public string version { get; set; }
        public string camera_id { get; set; }
        public string timestamp { get; set; }
        public List<Result> results { get; set; }

    }

    public class Result
    {
        public string plate { get; set; }
        public string score { get; set; }
        public Box box { get; set; }
        public List<Candidate> candidates { get; set; }
        public Region region { get; set; }
        public Vehicle vehicle { get; set; }

    }

    public class Box
    {
        public string xmin { get; set; }
        public string ymin { get; set; }
        public string xmax { get; set; }
        public string ymax { get; set; }
    }

    public class Region
    {
        public string code { get; set; }
        public string score { get; set; }
    }

    public class Candidate
    {
        public string score { get; set; }
        public string plate { get; set; }
    }

    public class Vehicle
    {
        public string score { get; set; }
        public string type { get; set; }
        public Box box { get; set; }
    }

}
