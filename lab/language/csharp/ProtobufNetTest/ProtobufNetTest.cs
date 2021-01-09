using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;

namespace ProtobufNetTest
{
    [Message]
    public abstract class ProtoPerson
    {
        public string Name { get; set; }
    }

    [Message]
    public class Student:ProtoPerson
    {
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    [Message]
    public class Student1 : ProtoPerson
    {
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    [Message]
    public class Student2 : Student
    {
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    [Message]
    public class Teacher : ProtoPerson
    {
        public int Age1 { get; set; }
        public string Gender1 { get; set; }
    }

    [Message]
    public class Teacher1 : Teacher
    {
        public int Age1 { get; set; }
        public string Gender1 { get; set; }
    }
    public class MessageAttribute : Attribute
    {
    }

    public class ProtobufNetTest
    {
        public static Dictionary<Type, IList<Type>> Types { get; set; } = new Dictionary<Type, IList<Type>>();

        public static void Run()
        {
            Console.WriteLine(Assembly.GetCallingAssembly().FullName);
            Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
            Console.WriteLine(Assembly.GetEntryAssembly()?.FullName);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RegisterMessage();
            Console.WriteLine(sw.ElapsedMilliseconds);

            var student = new Student {Age = 10, Gender = "Male",Name = "Test"};
            var newStudent = ProcessData(student);
            Console.WriteLine(JsonConvert.SerializeObject(newStudent));
        }

        private static void RegisterMessage()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetCustomAttribute<MessageAttribute>() != null);
            foreach (var type in types)
            {
                RegisterType(type);
            }
            foreach (var derivedType in Types)
            {
                var number = 1;
                var model = RuntimeTypeModel.Default.Add(derivedType.Key, true);

                foreach (var type in derivedType.Value)
                {
                    model.AddSubType(number++, type);
                }
                var members = derivedType.Key.GetMembers();
                foreach (var memberInfo in members)
                {
                    if (memberInfo.MemberType == MemberTypes.Property)
                    {
                        model.AddField(number++, memberInfo.Name);
                    }
                }
                Console.WriteLine(
                    $"key:{derivedType.Key},Values:{string.Join(",", derivedType.Value?.Select(x => x.Name).ToArray())}");
            }
        }

        private static void RegisterType(Type type)
        {
            if (type.Name != nameof(ProtoPerson))
            {
                var baseType = type.BaseType;
                if (baseType == null)
                    return;
                RegisterType(baseType);
            }

            if (!Types.ContainsKey(type))
            {
                Types.Add(type, new List<Type>());
            }

            if (type.BaseType != null && Types.ContainsKey(type.BaseType))
            {
                Types[type.BaseType].Add(type);
            }
        }

        static T ProcessData<T>(T data)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                var buffer = ms.ToArray();
                using (var ms2 = new MemoryStream(buffer))
                {
                    var result = Serializer.Deserialize<T>(ms2);
                    return result;
                }
            }
        }
    }
}