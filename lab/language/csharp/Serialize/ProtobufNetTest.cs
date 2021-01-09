using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;

namespace SerializeTest

{

    [ProtoContract]
    [ProtoInclude(1,typeof(Student))]
    [ProtoInclude(2,typeof(Teacher))]
    public abstract class ProtoPerson
    {
        [ProtoMember(1)]
        public string Name { get; set; }

    }

    [ProtoContract]
    public class Student: ProtoPerson
    {
        [ProtoMember(2)]
        public int Age { get; set; }
    }

    [ProtoContract]
    public class Teacher : ProtoPerson
    {
        [ProtoMember(3)]
        public string Title { get; set; }
    }

    public class ProtobufNetTest
    {
        public static void Run()
        {
            using (var ms = new MemoryStream())
            {
                var student = new Student();
                student.Age = 20;
                //var buffer = ms.ToArray();
                Serializer.Serialize(ms, student);
                var newPerson=  Serializer.Deserialize<ProtoPerson>(ms);
                Console.WriteLine( JsonConvert.SerializeObject(newPerson));
            }
        }
    }

}