using _223_Bulatov_Vodyanoy;
using _223_Bulatov_Vodyanoy.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void F1Calculate_ValidInput_ReturnsTrueAndCorrectResult()
        {
            double x = 0.0;
            double y = 1.0;
            double z = 1.0;
            double expected = -0.375 * Math.PI; // 5*Atan(0) - 0.25*Acos(0)*(3/1)
            double epsilon = 1e-7;

            bool isSuccess = Functions.F1Calculate(x, y, z, out double actual);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(expected, actual, epsilon);
        }

        [TestMethod]
        public void Page2TestMethod()
        {

        }

        [TestMethod]
        public void Page3TestMethod()
        {

        }
    }
}