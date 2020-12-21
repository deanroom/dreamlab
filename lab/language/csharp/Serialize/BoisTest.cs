using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Salar.Bois;

namespace serialize
{
    public class BoisTest
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
                    new Me() {FirstName = "Jerry2", LastName = "Ding2",Department="It2",Age = "200",IntPtrValue=new IntPtr(10)},
                }
            };
            
            //BoisSerializer.Initialize<Me>();
            var boisSerializer = new BoisSerializer();
            byte[] serilize;
            using (var mem = new MemoryStream())
            {
                boisSerializer.Serialize(me, mem);

                serilize= mem.ToArray();
            }
            
            using (var mem = new MemoryStream(serilize))
            {
               var meAgain= boisSerializer.Deserialize<Me>(mem);
            }

        }
    }


}