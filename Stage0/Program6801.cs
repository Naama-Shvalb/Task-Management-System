// See https://aka.ms/new-console-template for more information
using System.Transactions;
namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6801();
            Welcome3947();
            Console.ReadKey();
        }

        static partial void Welcome3947();
        private static void Welcome6801()
        {
            Console.Write("Enter user name: ");
            string? user = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", user);
            Console.ReadKey();
        }
    }
}


