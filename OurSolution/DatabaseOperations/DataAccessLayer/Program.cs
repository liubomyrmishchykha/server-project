using System;

namespace DataAccessLayer
{
    class Program
    {

        private static readonly DbDataProvider db = DbDataProvider.Get();
        static void Main()
        {
            Console.WriteLine(DateTime.Now.ToString("d/MM/yyyy"));
        }
    }
}
