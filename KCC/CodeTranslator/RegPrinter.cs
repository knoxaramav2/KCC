
/// <summary>
/// For debugging
/// </summary>

using System;

namespace CodeTranslator
{
    public class RegPrinter
    {
        public static void PrintRegUpdate(string scope, string key, string value, string action)
        {
            Console.WriteLine("{0,8} {1,8} {2,15} {3,8}",scope, key, value, action);
        }
    }
}