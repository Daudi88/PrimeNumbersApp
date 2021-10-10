using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PrimeNumbersApp.Tests
{
    [TestClass()]
    public class PrimeNumbersTests
    {
        [TestMethod()]
        [DataRow(0, false)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        [DataRow(3, true)]
        [DataRow(4, false)]
        [DataRow(5, true)]
        [DataRow(-1, false)]
        [DataRow(7, true)]
        [DataRow(9, false)]
        [DataRow(int.MaxValue, false)]
        [DataRow(1500007, true)]
        [DataRow(49, false)]
        [DataRow(25, false)]
        [DataRow(17, true)]
        public void IsPrimeNumberTest(int num, bool expected)
        {
            var actual = PrimeNumbers.IsPrimeNumber(num);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(0, 2)]
        [DataRow(2, 3)]
        [DataRow(5, 7)]
        [DataRow(7, 11)]
        [DataRow(89, 97)]
        [DataRow(103, 107)]
        [DataRow(int.MaxValue, -1)]
        [DataRow(-1, 2)]
        public void NextPrimeNumberTest(int num, int expected)
        {
            var actual = PrimeNumbers.NextPrimeNumber(num);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetLargestNumberTest_ListOfThreeNumbers_ReturnsEleven()
        {
            var list = new List<int> { 2, 11, 5 };
            var actual = PrimeNumbers.GetLargestNumber(list);
            Assert.AreEqual(11, actual);
        }

        [TestMethod()]
        public void GetLargestNumberTest_ListOfFourNumbers_ReturnsFortySeven()
        {
            var list = new List<int> { -2, 11, 47, 5 };
            var actual = PrimeNumbers.GetLargestNumber(list);
            Assert.AreEqual(47, actual);
        }

        [TestMethod()]
        public void GetLargestNumberTest_EmptyList_ReturnsZero()
        {
            var list = new List<int>();
            var actual = PrimeNumbers.GetLargestNumber(list);
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void GetLargestNumberTest_Null_ReturnsZero()
        {
            var actual = PrimeNumbers.GetLargestNumber(null);
            Assert.AreEqual(0, actual);
        }
    }
}