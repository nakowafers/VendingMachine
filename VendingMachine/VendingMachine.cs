using System;
using System.Collections.Generic;
using System.Dynamic;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<VendingItem> _vendingItems = new List<VendingItem>()
        {
            new VendingItem("candy", 0.10, 100),
            new VendingItem("snack", 0.50, 100),
            new VendingItem("nuts", 0.90, 100),
            new VendingItem("Coke", 0.25, 100),
            new VendingItem("Pepsi", 0.35, 100),
            new VendingItem("Gingerale", 0.45, 100)
        };

        private List<double> _allowedCurrencies = new List<double>()
        {
            0.01,
            0.05,
            0.10,
            0.25,
            1.00,
            2.00
        };

        private double _moneySessionTotal = 0.0;

        public VendingMachine()
        {
            PrintVendingDisplay();

            Run(Console.ReadLine());
        }

        private void Run(string input)
        {
            while (input != "x")
            {
                // Check if input is double then currency
                if (double.TryParse(input, out double value) && IsValidCurrency(value))
                {
                    _moneySessionTotal += value;
                }
                else
                {
                    Console.WriteLine("INVALID CURRENCY");
                }

                PrintVendingDisplay();
                input = Console.ReadLine();
            }
        }

        // return error message here?
        private bool IsValidCurrency(double input) => _allowedCurrencies.Contains(input);


        public void ResetVendingMachine()
        {
            _vendingItems.ForEach(delegate (VendingItem item) { item.Reset(); });
        }

        private void PrintVendingDisplay()
        {
            Console.WriteLine("Nicola's Vending Machine!");

            foreach (var item in _vendingItems)
            {
                string value = $"{item.Name} @ {item.Cost.ToString("C")}, {item.Quantity} Remaining";
                Console.WriteLine(value);
            }
            Console.WriteLine($"Current Money in Macine is: {_moneySessionTotal}");
            Console.WriteLine("Enter money or code to vend.");
        }
    }
}
