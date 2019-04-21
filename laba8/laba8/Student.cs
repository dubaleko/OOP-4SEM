using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    class Student
    {
        public string FIO { get; set; }
        public int Age { get; set; }
        public DateTime date { get; set; }
        public string specialization { get; set; }
        public string gender { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }
        public string City { get; set;}
        public string Street { get; set;}
        public string Index { get; set; }
        public int Home { get; set; }
        public int Flat { get; set;}

        public Student(string FIO, int Age, DateTime date, string specialization, string gender, int Course, int Group, 
                       string City , string Street, string Index, int Home, int Flat)
        {
            this.FIO = FIO;
            this.Age = Age;
            this.date = date;
            this.specialization = specialization;
            this.gender = gender;
            this.Course = Course;
            this.Group = Group;
            this.City = City;
            this.Street = Street;
            this.Index = Index;
            this.Home = Home;
            this.Flat = Flat;
        }
        public Student(string FIO, int Age, DateTime date)
        {
            this.FIO = FIO;
            this.Age = Age;
            this.date = date;
        }
        public Student(string FIO, string City, string Street)
        {
            this.FIO = FIO;
            this.City = City;
            this.Street = Street;
        }
        public Student(string FIO, string specialization, int Course, int Group )
        {
            this.FIO = FIO;
            this.specialization = specialization;
            this.Course = Course;
            this.Group = Group;
        }
    }
}
