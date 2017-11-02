using Autofac;
using Reckon.Domain;
using Reckon.DomainService;
using Reckon.Infrastructure.DependencyResolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reckon.Domain.Tests
{
    [TestClass]
    public class SearchServiceTest
    {
        public static IContainer Container { get; set; }

        [TestInitialize]
        public void SetupContainer()
        {
            DependencyBootstrapper.EnsureDependenciesRegistered();
            Container = DependencyBootstrapper.Container;
        }


        [TestMethod]
        public void Find_Method_Should_Be_Accessible_From_External()
        {
           
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var mock = new Mock<ISearchService>();
                //Act
                mock.Object.Find(string.Empty, string.Empty);
                //Assert
                mock.Verify(m => m.Find(string.Empty, string.Empty), Times.Once(), "Find method should be accessible from external.");
            }
        }

        [TestMethod]
        public void Find_Method_Should_Return_A_String()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();

                //Act
                var result = searchService.Find(string.Empty, string.Empty);

                //Assert
                Assert.IsTrue(result.GetType() == typeof(string), "Find method should return a string");
            }
        }

        [TestMethod]
        public void Find_Method_Should_Return_No_Output_When_Input_Is_Invalid()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();

                //Act
                var result1 = searchService.Find(string.Empty, string.Empty);
                var result2 = searchService.Find(string.Empty, "");
                var result3 = searchService.Find("123", "");

                //Assert
                Assert.IsTrue(result1.Equals("<No Output>"), "Find method should return no output when input is invalid");
                Assert.IsTrue(result2.Equals("<No Output>"), "Find method should return no output when input is invalid");
                Assert.IsTrue(result3.Equals("<No Output>"), "Find method should return no output when input is invalid");
            }
        }

        [TestMethod]
        public void Find_Method_Should_Accept_Two_Strings_As_Input()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();
                try
                {
                    //Act - If someone changed interface, it should threw exception to warn users
                    var result = searchService.Find(string.Empty, string.Empty);
                }
                catch(Exception e)
                {
                    //Assert
                    Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
                }
            }
        }


        [TestMethod]
        public void String_Extension_Method_Should_Be_Case_Insensitive()
        {
            //Arrange
                char test1 = 'a';
               
                try
                {
                    var result = test1.IsEqualTo('A');
                Assert.IsTrue(result, "String char extention method should be case insensitive.");
                }
                catch (Exception e)
                {
                    Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
                }
            
        }

        [TestMethod]
        public void Find_Method_Should_Follow_Rules()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();
                var textToSearch = "Peter told me (actually he slurrred) that peter the pickle piper piped a pitted pickle before he petered out. Phew!";
                try
                {
                    //Act
                    var test1 = searchService.Find(textToSearch, "Peter");
                    var test2 = searchService.Find(textToSearch, "peter");
                    var test3 = searchService.Find(textToSearch, "Pick");
                    var test4 = searchService.Find(textToSearch, "Pi");
                    var test5 = searchService.Find(textToSearch, "<No Output>");

                    //Assert
                    Assert.IsTrue(test1.Equals("1, 43, 98"), "It should output beginning position of matched subtext.");
                    Assert.IsTrue(test2.Equals(test2), "Match should be case insensitive.");
                    Assert.IsTrue(test3.Equals("53, 81"), "It should allow multiple matches");
                    Assert.IsTrue(test4.Equals("53, 60, 66, 74, 81"), "It should allow multiple matches");
                    Assert.IsTrue(test5.Equals("<No Output>"), "It should output nooutput if no matches");
                }
                catch (Exception e)
                {
                    //Assert
                    Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
                }
            }
        }

        [TestMethod]
        public void Find_Method_Should_Return_1_When_Two_Inputs_Are_Same()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();
                var textToSearch = "P";
                try
                {
                    //Act
                    var test1 = searchService.Find(textToSearch, "p");
                    //Assert
                    Assert.IsTrue(test1.Equals("1"), "It should output 1 when two inputs are same.");
                }
                catch (Exception e)
                {
                    //Assert
                    Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
                }
            }
        }

        [TestMethod]
        public void Find_Method_Should_Not_Throw_Index_Out_Of_Range_Exception()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //Arrange
                var searchService = scope.Resolve<ISearchService>();
                var text1 = "P"; var subtext1 = "peter";
                var text2 = "P"; var subtext2 = "p";
                var text3 = "peter"; var subtext3 = "p";
                try
                {
                    //Act
                    var test1 = searchService.Find(text1, subtext1);
                    var test2 = searchService.Find(text2, subtext2);
                    var test3 = searchService.Find(text3, subtext3);

                    //Assert
                    Assert.IsTrue(test1.Equals("<No Output>"), string.Format("Text: {0}, SubText: {1} - It should return no output", text1, subtext1));
                    Assert.IsTrue(test2.Equals("1"), string.Format("Text: {0}, SubText: {1} - It should return 1", text2, subtext2));
                    Assert.IsTrue(test3.Equals("1"), string.Format("Text: {0}, SubText: {1} - It should return 1", text3, subtext3));
                }
                catch (Exception e)
                {
                    //Assert
                    Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
                }
            }
        }
    }
}
