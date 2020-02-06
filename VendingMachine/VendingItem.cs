using System;
namespace VendingMachine
{

    /// <summary>
    /// private set; lookup
    /// 
    /// </summary>
    public class VendingItem
    {
        private int _startingQuantity;
        public string Name { get; }
        public double Cost { get; }
        public int Quantity
        {
            get;

            // prevent quantity from be overwritten outside of object
            private set;
        }

        public VendingItem(string name, double cost, int quantity)
        {
            Name = name;
            Cost = cost;
            Quantity = quantity;
            _startingQuantity = quantity;
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
