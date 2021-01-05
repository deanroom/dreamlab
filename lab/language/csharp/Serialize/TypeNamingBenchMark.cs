using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace serialize
{
    public class TypeNamingTest
    {
        
        [Benchmark]
        public static void Run()
        {
            var me = new Me()
            {
                Age = "37",
                Salary = "000",
                Persons = new List<Person>
                {
                    new Person() {FirstName = "Jerry", LastName = "Ding"},
                    new Employee() {FirstName = "Jerry1", LastName = "Ding1",Department="It1"},
                    new Me() {FirstName = "Jerry2", LastName = "Ding2",Department="It2",Age = "200",IntPtrValue=new IntPtr(10)},
                }
            };
            
            var json = JsonConvert.SerializeObject(me);
            //Console.WriteLine($"Normal Serialize:\n{json}");

            var meAgain = JsonConvert.DeserializeObject<Me>(json);
            //Console.WriteLine($"Normal deserialize:\n{meAgainJson}");

            // var json1 = JsonConvert.SerializeObject(me, Formatting.Indented,
            //     new JsonSerializerSettings
            //     {
            //         TypeNameHandling = TypeNameHandling.All
            //     });
            //
            // Console.WriteLine($"TypeNameHandling serialize:\n{json1}");
            // JObject rss = JObject.Parse(json1);
            // var typeString = rss.First.Values().Aggregate((current, x) =>
            // {
            //     return current.ToString() + x.ToString();
            // });
            // var type = Type.GetType(typeString.ToString());
            // var meAgain1 = JsonConvert.DeserializeObject(json1, type);
            // var meAgainJson1 = JsonConvert.SerializeObject(meAgain1);
            // //Console.WriteLine($"Use the type get from json to deserialize and then normal serialize:\n{meAgainJson1}");
            //
            // var meAgain2 = JsonConvert.DeserializeObject(json1, new JsonSerializerSettings
            // {
            //     TypeNameHandling = TypeNameHandling.All
            // });
            // var meAgainJson2 = JsonConvert.SerializeObject(meAgain2);
            // //Console.WriteLine($"Use type naming handling deserialize and then normal serialize:\n{meAgainJson2}");
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Employee : Person
    {
        public string Department { get; set; }
        public string JobTitle { get; set; }
    }

    public class Me : Employee
    {
        public IntPtr IntPtrValue { get; set; }
        public string Age { get; set; }
        public string Salary { get; set; }
        public IList<Person> Persons { get; set; }
    }
}