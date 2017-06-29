using System;
using NpgSqlRx.Core;
using Console = System.Console;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new Connection("Server=localhost; Database=postgres; User Id=postgres; Password=password; Port=5432; enlist=true; Pooling=false;");
            var operation = new Operation(conn);
            var observable = operation.Read<TestClass>("SELECT 'hello' as ColA, 'world' as ColB");
            var subscription = observable.Subscribe(next =>
            {
                Console.WriteLine(string.Format("{0} {1}", next.ColA, next.ColB));
            }, () =>
            {
                Console.ReadKey();
            });
        }
    }

    public class TestClass
    {
        public string ColA { get; set; }
        public string ColB { get; set; }
    }
}
