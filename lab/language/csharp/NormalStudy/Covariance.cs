using System;
using System.Collections;
using System.Collections.Generic;

namespace NormalStudy
{
    public class Test1
    {

    }

    public class Test2 : Test1
    {
        public string Name { get; set; }
    }
    public class Convariance
    {
        public static void Run()
        {
            AddValue<Test2>(x=>Write(x));
        }
        public static void Write<T>(T a) where T:Test1
        {
            Console.WriteLine(a);
        }
        public static void AddValue<T>(Action<Test1> doSomething) 
        {
            var value = new Test2();
            doSomething?.Invoke(value);
        }
    }
}