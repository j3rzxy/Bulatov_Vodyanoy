using _223_Bulatov_Vodyanoy;
using _223_Bulatov_Vodyanoy.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static _223_Bulatov_Vodyanoy.Func2;


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
        public void F2Calculate_ValidInput_ReturnsTrueAndCorrectResult()
        {
            double x = 1.0;
            double b = 2.0;
            var funcType = Functions2.FunctionType.Square;

            double expected = Math.Exp(1.0); // e^1
            double epsilon = 1e-7;

            bool isSuccess = Functions2.F2Calculate(x, b, funcType, out double actual, out string selectedName);

            Assert.IsTrue(isSuccess, "Метод должен вернуть true при корректных данных");
            Assert.AreEqual(expected, actual, epsilon, "Результат вычисления не совпадает с ожидаемым");
            Assert.AreEqual("x^2", selectedName, "Имя выбранной функции должно быть 'x^2'");
        }

        [TestMethod]
        public void F3Calculate_ValidInput_ReturnsTrueAndRoundedResult()
        {
            double x = 1.0;
            double b = 1.0;
            double expected = 199.919;
            double epsilon = 0.001;

            bool isSuccess = Functions3.F3Calculate(x, b, out double actual, out string error);

            Assert.IsTrue(isSuccess, "Метод должен вернуть true при корректных данных");
            Assert.IsNull(error, "Сообщение об ошибке должно быть null при успехе");
            Assert.AreEqual(expected, actual, epsilon, "Результат не совпадает с ожидаемым");
        }
        [TestMethod]
        public void F3Calculate_NegativeUnderRoot_ReturnsFalseWithError()
        {
            double x = 2.0;
            double b = -3.0;

            bool isSuccess = Functions3.F3Calculate(x, b, out double actual, out string error);

            Assert.IsFalse(isSuccess, "Метод должен вернуть false при отрицательном подкоренном выражении");
            Assert.IsNotNull(error, "Сообщение об ошибке не должно быть null");
            Assert.IsTrue(error.Contains("отрицательное"), "Сообщение должно указывать на отрицательное значение под корнем");
            Assert.AreEqual(0, actual, "При ошибке результат должен быть 0");
        }

        [TestMethod]
        public void F3Calculate_ZeroUnderRoot_ReturnsCorrectResult()
        {
            double x = 2.0;
            double b = -2.0;
            double expected = 18.0;

            bool isSuccess = Functions3.F3Calculate(x, b, out double actual, out string error);

            Assert.IsTrue(isSuccess);
            Assert.IsNull(error);
            Assert.AreEqual(expected, actual, 0.001);
        }
    }
}