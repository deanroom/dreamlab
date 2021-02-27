using System;

namespace NormalStudy
{
    internal class DynamicTest
    {
        int Test(int a)
        {
            return 0;
        }
        string Test(string a)
        {
            return a; 
            a.GetType();
        }
        void Test()
        {
            dynamic a = 123;
            var result = Test(a);
        }
    }
}