using System;
using System.Collections.Generic;
using System.IO;
using Utf8Json;

namespace serialize
{
    public class Utf8Json
    {
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
                }
            };
            var json = JsonSerializer.Serialize(me);

            //Console.WriteLine(json);

        }
    }
}