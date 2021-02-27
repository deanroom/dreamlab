using System;
using System.Collections.Generic;
using System.IO;
using Jil;

namespace SerializeTest
{
    public class JilTest
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
                    new Employee() {FirstName = "Jerry1", LastName = "Ding1", Department = "It1"},
                    new Me()
                    {
                        FirstName = "Jerry2", LastName = "Ding2", Department = "It2", Age = "200",
                        IntPtrValue = new IntPtr(10)
                    },
                }
            };
            using (var output = new StringWriter())
            {
                JSON.Serialize(me,output);
            }
        }
    }
}