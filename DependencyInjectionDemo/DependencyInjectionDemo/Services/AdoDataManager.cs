using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public class AdoDataManager : IDataManager
    {
        public string GetMessage(string name)
        {
            return $"Hellow {name} from AdoDataManager";
        }
    }
}
