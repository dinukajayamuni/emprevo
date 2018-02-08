using System;
using Autofac;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = Bootstrapper.Build();
            var calculator = container.Resolve<IPriceCalculator>();
            CalculateTotalPrice(calculator);
        }

        private static void CalculateTotalPrice(IPriceCalculator calculator)
        {
            try
            {
                var entryDateTime = ReadDateTime("Enter Patron’s Entry Date and Time (i.e. 22/03/2018 10:30:00 AM): ");
                var exitDateTime = ReadDateTime("Enter Patron’s Entry Date and Time (i.e. 22/03/2018 08:30:00 PM): ");
                if (entryDateTime > exitDateTime) throw new ArgumentOutOfRangeException(nameof(exitDateTime));
                var price = calculator.Calculate(entryDateTime, exitDateTime);
                Console.WriteLine($"Rate Name: {price.RateName}");
                Console.WriteLine($"Rate Amount: {price.Rate}");
                Console.WriteLine($"Total Price: {price.Total}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static DateTime ReadDateTime(string message)
        {
            Console.WriteLine(message);
            DateTime dateTime;
            if (DateTime.TryParse(Console.ReadLine(), out dateTime)) return dateTime;
            Console.WriteLine("Invalid Date and Time");
            ReadDateTime(message);
            return dateTime;
        }
    }
}