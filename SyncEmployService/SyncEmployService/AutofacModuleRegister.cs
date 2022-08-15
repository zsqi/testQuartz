using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService
{
    public class AutofacModuleRegister : Autofac.Module
    {
        //重写Autofac管道Load方法，在这里注册注入
        protected override void Load(ContainerBuilder builder)
        {
            //程序集注入业务服务
            var IAppServices = Assembly.Load("SyncEmployService");
            var AppServices = Assembly.Load("SyncEmployService");
            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces();
        }
    }
}
