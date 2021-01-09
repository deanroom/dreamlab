using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;

namespace ProtobufNetTest

{

    [ProtoContract]
    public  class ProtoPerson
    {
        [ProtoMember(3)]
        public string Name { get; set; }

    }

    [ProtoContract]
    public class Student:ProtoPerson
    {
        [ProtoMember(1)]
        public int Age { get; set; }
        [ProtoMember(2)]
        public string Gender { get; set; }
    }

    public class ProtobufNetTest
    {
        public static void Run()
        {
            using var ms = new MemoryStream();
            var student = new Student {Age = 20, Gender = "Male"};
            var buffer = ms.ToArray();
            Serializer.Serialize(ms,student);
            var newStudent = Serializer.Deserialize<Student>(ms);
            Console.WriteLine( JsonConvert.SerializeObject(newStudent));
        }
    }

}