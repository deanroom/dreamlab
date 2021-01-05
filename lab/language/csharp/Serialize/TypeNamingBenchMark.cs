using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace SerializeTest
{
    public class TypeNamingBenchMark
    {
        
        [Benchmark]
        public  void Run()
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
           
        }
    }

}