using Autofac;
using Reckon.Infrastructure.DependencyResolution;


namespace Preface.Console
{
    using Reckon.Domain;
    using System;
    using System.Collections.Generic;

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
                    //Test 2
                    var searchService = scope.Resolve<ISearchService>();

                    Console.WriteLine("enter the text to search");
                    string text = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("enter the subtext - (press Q to quit)");
                        string subText = Console.ReadLine();
                        if (subText.ToLower().Equals("q"))
                            break;
                        var result = searchService.Find(text, subText);
                        Console.WriteLine(result);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
    }
}