using Autofac;
using Autofac.Extras.Quartz;
using Microsoft.Extensions.Configuration;
using SyncEmployService.Common.Helper;
using SyncEmployService.Job;
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
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + "\\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configFilePath));

            //注入日志
            builder.Register(c => log4net.LogManager.GetLogger(typeof(Object))).As<log4net.ILog>();
            //注入调度服务
            builder.RegisterType<ScheduleService>().InstancePerLifetimeScope();
            builder.RegisterType<JobService>().InstancePerLifetimeScope();
            //程序集注入业务服务
            var IAppServices = Assembly.Load("SyncEmployService");
            var AppServices = Assembly.Load("SyncEmployService");
            //builder.RegisterAssemblyTypes(IAppServices, AppServices)
            //  .Where(t => t.Name.EndsWith("Job"))
            //  .AsSelf();
            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces();
            //配置quartz.net依赖注入
            //builder.RegisterModule(new QuartzAutofacFactoryModule());
            //builder.RegisterModule(new QuartzAutofacJobsModule(Assembly.GetExecutingAssembly()));
        }
    }
}
