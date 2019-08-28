using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMvcCore
{
    public class AppConfiguration
    {
        public string Company { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public Address Address { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public int Duration { get; set; }
        public Course Courses { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
    }

    public class Course
    {
        public string Title { get; set; }
        public string NoOfParticipant { get; set; }
    }

    public class Address{
        public string City { get; set; }
        public string BuildingNo { get; set; }
    }
}
