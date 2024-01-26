using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task4
{
    internal class Program
    {

        static void Main(string[] args)
        {

            string path1 = "C:\\Users\\MSI\\Desktop\\Обучение\\Task4\\students.dat";
            string path2 = "C:\\Users\\MSI\\Desktop\\Students";


            List<Student> students = ReadValues(path1);
            //foreach (Student studentProp in students)
            //{
            //    Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
            //}
            //Directory.CreateDirectory(path2);

            foreach (Student studentProp in students)
            {

                //var fs = new FileStream(path2, FileMode.Open);
                //using (StreamWriter sw = new StreamWriter(fs))
                //{
                //    sw.WriteLine(path2, "\\" + studentProp.Group + ".txt");
                //}

                File.CreateText(path2 + "\\" + studentProp.Group + ".txt");

            }
            WriteValues(students, path2);
        }
        static List<Student> ReadValues(string path)
        {
            List<Student> result = new List<Student>();

            var fs = new FileStream(path, FileMode.Open);

            using (StreamReader sr = new StreamReader(fs))
            {

                fs.Position = 0;

                BinaryReader br = new BinaryReader(fs);

                while (fs.Position < fs.Length)
                {
                    Student student = new Student();
                    student.Name = br.ReadString();
                    student.Group = br.ReadString();
                    long dt = br.ReadInt64();
                    student.DateOfBirth = DateTime.FromBinary(dt);
                    student.AverageScore = br.ReadDecimal();
                    result.Add(student);
                }
            }
            fs.Close();
            return result;
        }
        static void WriteValues(List<Student> students, string path)
        {
           
            var fs = new FileStream(path, FileMode.Open);
            var fileName=Directory.GetFiles(path);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                
                foreach (Student student in students)
                {
                    foreach (string file in fileName)
                    if (student.Name == file)
                    {
                        sw.Write(student.Group);
                        sw.Write(student.DateOfBirth);
                        sw.Write(student.AverageScore);
                    }
                }
            }
        }
    }
}
