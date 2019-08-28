using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public interface IDataManager
    {
        string GetMessage(string name);
    }
}
