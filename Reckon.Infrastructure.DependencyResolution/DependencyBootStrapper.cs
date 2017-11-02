using Autofac;
using System;

namespace Reckon.Infrastructure.DependencyResolution
{
    public class DependencyBootstrapper
    {
        private static bool _dependenciesRegistered;
        private static readonly object sync = new object();

        public static IContainer Container { get; set; }

        public virtual void RegisterAllDependenciesOnStartup()
        {
            ConfigureAutofac();
        }

        /// <summary>
        /// Perform registrations and build the container - http://docs.autofac.org/en/latest/integration/csl.html
        /// </summary>
        private void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BossHogModule());
            Container = builder.Build();
        }

        public static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (sync)
                {
                    if (!_dependenciesRegistered)
                    {
                        new DependencyBootstrapper().RegisterAllDependenciesOnStartup();
                        _dependenciesRegistered = true;
                    }
                }
            }
        }

        public virtual void EnsureDependenciesRegisteredWrapper()
        {
            EnsureDependenciesRegistered();
        }

        public bool IsDependenciesRegistered()
        {
            return _dependenciesRegistered;
        }
    }
}
