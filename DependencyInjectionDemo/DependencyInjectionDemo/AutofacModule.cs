using Autofac;
using DependencyInjectionDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EntityDataManager>().As<IDataManager>().SingleInstance();
            builder.RegisterType<DemoService>().As<DemoService>();
        }
    }
}
