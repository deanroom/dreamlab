using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Running;
using Jil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SerializeTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<TypeNamingBenchMark>();
            ProtobufNetTest.Run();
        }
    }
}