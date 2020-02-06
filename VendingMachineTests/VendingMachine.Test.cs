using NUnit.Framework;
using VendingMachine;

namespace VendingMachineTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VendingItem_CreatesItem_ResultsExpectedProperties()
        {
            // Arrange
            VendingItem popTarts = new VendingItem("PopTarts", 0.35, 100);

            // Act
            string name = popTarts.Name;
            string cost = popTarts.Cost.ToString();
            int quantity = popTarts.Quantity;
            string result = $"{name} is {cost} and there is a total of {quantity}";

            // Assert
            Assert.That(result, Is.EqualTo("PopTarts is 0.35 and there is a total of 100"));
        }
    }
}