// See https://aka.ms/new-console-template for more information
using Autofac;
using Quartz;
using Quartz.Impl;
using SyncEmployService;
using SyncEmployService.Job;
using Topshelf;

var container = DependencyConfig.GetContainer();

HostFactory.Run(x =>
{
    x.SetDisplayName("恒久-喔趣数据获取服务");
    x.SetServiceName("SyncEmployService");
    x.SetDescription("用于获取喔趣的员工信息并写入恒久系统");

    x.Service<ScheduleService>(setting =>
    {
        setting.ConstructUsing(name => container.Resolve<ScheduleService>());
        setting.WhenStarted(async tc => await tc.Start());
        setting.WhenStopped(async tc => await tc.Stop());
    });
    x.RunAsLocalSystem();
});


