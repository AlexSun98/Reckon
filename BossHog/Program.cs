using Autofac;
using Reckon.Domain;
using Reckon.Infrastructure.DependencyResolution;
using System;

namespace Reckon
{
    public class Program
    {
        public static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            DependencyBootstrapper.EnsureDependenciesRegistered();
            Container = DependencyBootstrapper.Container;

            using (var scope = Container.BeginLifetimeScope())
            {
                try
                {
                    //Test 1
                    var printService = scope.Resolve<IPrintService>();
                    var numbers = printService.SetupNumeric();
                    printService.Print(numbers);
                }
                catch(Exception e)
                {
                    throw e;
                }
                Console.ReadKey();
            }
        }
    }
}
