using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;

namespace ConsoleApplication1
{
    [ProtoContract]
    //[ProtoInclude(11,typeof(Student))]
    public abstract  class ProtoPerson
    {
        [ProtoMember(1)] public string Name { get; set; }
    }

    [ProtoContract]
    public class Student : ProtoPerson
    {
        [ProtoMember(2)] public int Age { get; set; }
        [ProtoMember(3)] public string Gender { get; set; }
    }

    public class ProtobufNetTest
    {
        public static void Run()
        {
            var student = new Student {Age = 10, Gender = "Male",Name="Test"};
            var newStudent = ProcessData(student);
            Console.WriteLine(JsonConvert.SerializeObject(newStudent));
        }

        static T ProcessData<T>(T data) where T : ProtoPerson
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                var buffer = ms.ToArray();
                using (var ms2 = new MemoryStream(buffer))
                {
                    var result= Serializer.Deserialize<T>(ms2);
                    Console.WriteLine(result.Name);
                    return result;
                }
            }
        }
    }
}