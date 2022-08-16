using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService
{
    public static class DependencyConfig
    {
        private static IContainer Container { get; set; }
        public static IContainer GetContainer()
        {
            if (Container == null)
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterModule(new AutofacModuleRegister());                
                Container = builder.Build();
            }
            return Container;
        }
    }
}
