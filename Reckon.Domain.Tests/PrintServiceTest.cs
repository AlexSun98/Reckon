using Autofac;
using Reckon.Domain;
using Reckon.Infrastructure.DependencyResolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reckon.Domain.Tests
{
    [TestClass]
    public class PrintServiceTest
    {
        public static IContainer Container { get; set; }

        [TestInitialize]
        public void SetupContainer()
        {
            DependencyBootstrapper.EnsureDependenciesRegistered();
            Container = DependencyBootstrapper.Container;
        }


        [TestMethod]
        public void Printer_Should_Print_100_Strings()
        {
           
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();

                //Act
                var total = printService.SetupNumeric().Count;

                //Assert
                Assert.IsTrue(total == 100, "Needs to print 100 strings");
            }
        }

        [TestMethod]
        public void Printer_Should_Print_Numbers_From_1_To_100()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();

                //Act
                var total = printService.SetupNumeric().Sum();

                //Assert
                Assert.IsTrue(total == 5050, "Needs to print numbers from 1 to 100");
            }
        }


        [TestMethod]
        public void Printer_Should_Throw_An_Exception_If_Total_Number_Of_Items_Is_Not_100()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();
                List<int> numbers = Enumerable.Range(1, 10).ToList();

                //Act/Assert
                Assert.ThrowsException<Exception>(() => printService.Print(numbers), "Printer should throw an exception if total number of printable items is not 100");
            }
        }

        [TestMethod]
        public void NumericConverter_Should_Convert_Multiples_of_3_To_Boss()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();
                //Act
                var result = printService.NumberConverter(6);
                //Assert
                Assert.IsTrue(result.Equals("Boss"), "Converter should return the word ¡°Boss¡± instead of the number for multiples of 3");
            }
        }

        [TestMethod]
        public void NumericConverter_Should_Convert_Multiples_of_5_To_Hog()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();
                //Act
                var result = printService.NumberConverter(10);
                //Assert
                Assert.IsTrue(result.Equals("Hog"), "Converter should return the word ¡°Hog¡± instead of the number for multiples of 5");
            }
        }

        [TestMethod]
        public void NumericConverter_Should_Convert_Multiples_of_5_And_3_To_BossHog()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();
                //Act
                var result = printService.NumberConverter(30);
                //Assert
                Assert.IsTrue(result.Equals("BossHog"), "Converter should return the word ¡°BossHog¡± instead of the number for multiples of 5 or 3");
            }
        }

        [TestMethod]
        public void NumericConverter_Should_Not_Convert_Number_To_Any_BossHog_If_It_Is_Not_Multiples_Of_Any_3_Or_5()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var printService = scope.Resolve<IPrintService>();
                //Act
                var result = printService.NumberConverter(7);
                //Assert
                Assert.IsTrue(result.Equals("7"), "Converter should not convert number to Boos or Hog Or BossHog if its not multiples of 3 or 5");
            }
        }
    }
}
