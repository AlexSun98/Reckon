using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Moq;

namespace Reckon.Infrastructure.DependencyResolution.Tests
{
    [TestClass]
    public class DependencyBoostraperTest
    {
        [TestMethod]
        public void BootStrapper_Should_Not_RegisterDependencies_If_Dependencies_are_Registered()
        {
            //Arrange
            var mock = new Mock<DependencyBootstrapper>();
          
            var field = typeof(DependencyBootstrapper).GetField("_dependenciesRegistered", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(mock.Object, true);

            //Act/Assert
            mock.Verify(m => m.RegisterAllDependenciesOnStartup(), Times.Never(), "Bootstrapper should not register dependencies if they have been registered");
        }

        [TestMethod]
        public void BootStrapper_Should_RegisterDependencies_Once()
        {
            //Arrange
            var mock = new Mock<DependencyBootstrapper>();

            var field = typeof(DependencyBootstrapper).GetField("_dependenciesRegistered", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(mock.Object, false);

            mock.Setup(m => m.EnsureDependenciesRegisteredWrapper()).Callback(DependencyBootstrapper.EnsureDependenciesRegistered);

            //Act
            mock.Object.EnsureDependenciesRegisteredWrapper();

            //Assert
            Assert.IsTrue(mock.Object.IsDependenciesRegistered(), "Bootstrapper should register dependencies");
        }
    }
}
