using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    [Serializable]
   public  class Student
    {
        [NonSerialized]
        public static string database = " ";

        public string firstname { get; set; }
        public string secondname { get; set; }
        public string thirdname { get; set; }
        public decimal age { get; set; }
        public string dateofbirthday { get; set; }
        public string specialization { get; set; }
        public string gender { get; set; }
        public string course { get; set; }
        public string group { get; set; }
        public Adress adress { get; set; }

        public Student(string firstname, string secondname, string thirdname, decimal age, string dateofbirthday,
                        string specialization, string gender, string course, string group, Adress adress)
        {
            this.firstname = firstname;
            this.secondname = secondname;
            this.thirdname = thirdname;
            this.age = age;
            this.specialization = specialization;
            this.dateofbirthday = dateofbirthday;
            this.gender = gender;
            this.course = course;
            this.group = group;
            this.adress = adress;
        }
        public Student()
        {

        }
        public string info()
        {
            return (firstname +" "+ secondname+" "+thirdname );
        }
        public string allinfoaboutstudent()
        {
            return ("Фамилия " + firstname + '\n' + "Имя " + secondname + '\n' +
                    "Отчество " + thirdname +'\n' + "Возраст "+ age.ToString() + '\n'+ "Дата Рождения " + dateofbirthday + '\n'+ 
                     "Половая принадлежность "+ gender + '\n' + "Специальность " + specialization + '\n' + "Курс " + course + '\n' +
                      "Группа " +  group + '\n' + "Город " + adress.city + '\n' +  "Индекс " + adress.index + '\n' + "Улица "+ adress.street+ '\n' +
                        "Дом " + adress.house + '\n' + "Квартира " + adress.flat);
        }
    }
}
