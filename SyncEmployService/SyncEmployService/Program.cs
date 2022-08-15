﻿// See https://aka.ms/new-console-template for more information
using Autofac;
using Quartz;
using Quartz.Impl;
using SyncEmployService;
using SyncEmployService.Job;
using Topshelf;

Console.WriteLine("Hello, World!");

ContainerBuilder builder=new ContainerBuilder();
builder.RegisterModule(new AutofacModuleRegister());
builder.Build();

HostFactory.Run(x =>
{
    x.SetDisplayName("SyncEmployService");
    x.SetServiceName("SyncEmployService");
    x.SetDescription("SyncEmployService");

    x.Service<ScheduleService>(setting =>
    {
        setting.ConstructUsing(name => new ScheduleService());
        setting.WhenStarted(async tc => await tc.Start());
        //setting.WhenStarted(tc => tc.Start());
        setting.WhenStopped(async tc => await tc.Stop());
    });
    x.RunAsLocalSystem();
});

//ScheduleService scheduleService = new ScheduleService();
//await scheduleService.Start();
Console.ReadKey();


