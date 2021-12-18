using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var cnn = new SqlConnection(@"Data Source=XUHONGDA\MSSQL;Initial Catalog=Study;User Id=sa;Password=123456");
            //cnn.Open();
            //var result = cnn.Query("Select * from Company");
            ////Console.WriteLine(result.Name);
            //foreach(var item in result)
            //{
            //    Console.WriteLine(item.Name);
            //    Console.WriteLine(item.Id);
            //}
            #region IE
            // Create a Person object for each job applicant.
            //Person applicant1 = new Person("Jones", "099-29-4999");
            //Person applicant2 = new Person("Jones", "199-29-3999");
            //Person applicant3 = new Person("Jones", "299-49-6999");

            //// Add applicants to a List object.
            //List<Person> applicants = new List<Person>();
            //applicants.Add(applicant1);
            //applicants.Add(applicant2);
            //applicants.Add(applicant3);

            //// Create a Person object for the final candidate.
            //Person candidate = new Person("Jones", "199-29-3999");
            //if (applicants.Contains(candidate))
            //    Console.WriteLine("Found {0} (SSN {1}).",
            //                       candidate.LastName, candidate.SSN);
            //else
            //    Console.WriteLine("Applicant {0} not found.", candidate.SSN);

            //// Call the shared inherited Equals(Object, Object) method.
            //// It will in turn call the IEquatable(Of T).Equals implementation.
            //Console.WriteLine("{0}({1}) already on file: {2}.",
            //                  applicant2.LastName,
            //                  applicant2.SSN,
            //                  Person.Equals(applicant2, candidate));
            #endregion
            #region compare
            BoxEqualityComparer boxEqC = new BoxEqualityComparer();

            var boxes = new Dictionary<Box, string>(boxEqC);

            var redBox = new Box(4, 3, 4);
            AddBox(boxes, redBox, "red");

            var blueBox = new Box(4, 3, 4);
            AddBox(boxes, blueBox, "blue");

            var greenBox = new Box(3, 4, 3);
            AddBox(boxes, greenBox, "green");
            Console.WriteLine();

            Console.WriteLine("The dictionary contains {0} Box objects.",
                              boxes.Count);
            #endregion
            Console.WriteLine("Hello World!");
        }
        private static void AddBox(Dictionary<Box, String> dict, Box box, String name)
        {
            try
            {
                dict.Add(box, name);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Unable to add {0}: {1}", box, e.Message);
            }
        }
    }
}
