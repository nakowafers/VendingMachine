using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingMachine
    {
        // IMPROVEMENT: store in database table?
        // IMPROVEMENT: make db table vending code column unique as constraint
        // IMPROVEMENT: add UI for vending itmes, and for maitenance menu to add items or reset
        // IMPROVEMENT: track transactions in db
        // IMPROVEMENT: add password for supplier reset
        // IMPROVEMENT: add menu for adding or removing vending categories as supplier
        private List<VendingItem> _vendingItems = new List<VendingItem>()
        {
            new VendingItem("candy", 0.10, 100, "A"),
            new VendingItem("snack", 0.50, 100, "B"),
            new VendingItem("nuts", 0.90, 100, "C"),
            new VendingItem("Coke", 0.25, 100, "D"),
            new VendingItem("Pepsi", 0.35, 100, "E"),
            new VendingItem("Gingerale", 0.45, 100, "F"),
            new VendingItem("PopTarts", 1.50, 10, "G")
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

        // current money total in vending machine
        private double _moneySessionTotal = 0.0;

        public VendingMachine()
        {
            Run();
        }

        // All methods private b/c vending machine is self contained as of now
        private void Run()
        {
            string input;

            while (true)
            {
                PrintVendingDisplay();
                input = Console.ReadLine().ToUpper(); // non case sensitive

                // is input a double 
                if (double.TryParse(input, out double result))
                {
                    InsertMoneyUpdateTotal(result);
                }
                // is input an existing vending code
                else if (_vendingItems.Exists(x => x.VendingCode.Equals(input)))
                {
                    Vend(input);
                }
                // check if want refund and if money left
                else if (input == "X")
                {
                    Refund();
                }
                else if (input == "RESET")
                {
                    ResetVendingMachineInventory();
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                }
            }
        }

        private void Vend(string input)
        {
            VendingItem itemToDispense = _vendingItems.Find(x => x.VendingCode.Equals(input));

            // check if enough funds
            if (itemToDispense.Cost > _moneySessionTotal)
            {
                Console.WriteLine("Not enough Funds");
                return;
            }

            // try to dispense
            if (itemToDispense.Dispense())
            {
                // return remaining chance
                double remainingChange = _moneySessionTotal - itemToDispense.Cost;
                _moneySessionTotal = 0;
                Console.WriteLine($"Dispensing {itemToDispense.Name} and Refunding {remainingChange.ToString("C")}");
            }
        }

        private void Refund()
        {
            if (_moneySessionTotal != 0)
            {
                Console.WriteLine($"Refunding {_moneySessionTotal.ToString("C")}");
                _moneySessionTotal = 0;
            }
        }

        private void InsertMoneyUpdateTotal(double input)
        {
            // check if double currency value is in allowed list
            if (_allowedCurrencies.Contains(input))
            {
                _moneySessionTotal += input;
            }
            else
            {
                Console.WriteLine("Currency Not Accepted!");
            }
        }

        private void ResetVendingMachineInventory()
        {
            Console.WriteLine("Resetting Vending Machine");
            _vendingItems.ForEach(delegate (VendingItem item) { item.Reset(); });
        }

        private void PrintVendingDisplay()
        {
            Console.WriteLine("Nicola's Vending Machine!");

            foreach (var item in _vendingItems)
            {
                string value = $"{item.VendingCode}: {item.Name,-10} @ {item.Cost.ToString("C")}, {item.Quantity} Remaining";
                Console.WriteLine(value);
            }
            Console.WriteLine($"Current Money in Machine is: {_moneySessionTotal.ToString("C")}");
            Console.WriteLine("Enter money or code to vend. Enter X to Cancel Transaction.");
        }
    }
}
