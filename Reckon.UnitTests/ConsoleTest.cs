using Reckon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reckon.Console.Tests
{
    [TestClass]
    public class ConsoleTest
    {
        [TestMethod]
        public void Print_To_Console_Should_Not_Threw_Any_Exception()
        {
            Assert.ThrowsException<Exception>(() => Program.Main(new string[0]), "Print to console should not threw any ecxeptions");
        }
    }
}
