using System;
using NpgSqlRx.Core;
using Console = System.Console;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new Connection("Server=localhost; Database=myDataBase; User Id=postgres; Password=password; Port=5432; enlist=true; Pooling=false;");
            var operation = new Operation(conn);
            var observable = operation.Read<article>("SELECT * from public.article;");
            var subscription = observable.Subscribe(next =>
            {
                Console.WriteLine(string.Format("{0} {1}", next.cola, next.colb));
            }, () =>
            {
                Console.ReadKey();
            });
        }
    }

    public class article
    {
        public string cola { get; set; }
        public string colb { get; set; }
    }
}
