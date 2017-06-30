using System;
using System.IO;
using NpgSqlRx.Core;
using Console = System.Console;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "Server=localhost; Database=myDataBase; User Id=postgres; Password=password; Port=5432; enlist=true; Pooling=false;";

            "SELECT * from public.article LIMIT 1000000;"
                .QueryToObservable<article>(connStr)
                .Subscribe(next =>
                {
                    Console.WriteLine(string.Format("{0} {1}", next.cola, next.colb));
                }, error =>
                {
                    Console.WriteLine(error.Message);
                    Console.ReadKey();
                }
                    , () =>
                {
                    Console.WriteLine("Complete");
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
