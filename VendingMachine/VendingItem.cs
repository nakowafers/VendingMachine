using System;
namespace VendingMachine
{

    /// <summary>
    /// private set; lookup
    /// 
    /// </summary>
    public class VendingItem
    {
        private readonly int _startingQuantity;
        public string Name { get; }
        public double Cost { get; }
        public int Quantity { get; private set; }
        public string VendingCode { get; }


        public VendingItem(string name, double cost, int quantity, string vendingCode)
        {
            Name = name;
            Cost = cost;
            Quantity = quantity;
            _startingQuantity = quantity;
            VendingCode = vendingCode;
        }

        public void Dispense()
        {
            // Check if any of item remaining before dispensing
            if (Quantity > 0)
            {
                Quantity--;
            }
            else
            {
                Console.WriteLine("None of that item left, Please choose another!");
            }
        }

        public void Reset()
        {
            Quantity = _startingQuantity;
        }
    }
}
