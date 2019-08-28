using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public class EntityDataManager : IDataManager
    {
        public string GetMessage(string name)
        {
            return $"Hellow {name} from EntityDataManager";
        }
    }
}
